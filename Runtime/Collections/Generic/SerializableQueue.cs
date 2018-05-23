/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RGSoft.Collections.Generic
{

	/// <summary>
	/// Serializable quque.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class SerializableQueue<T> : ICollection, IReadOnlyCollection<T>
	{

		private const int MinimumGrow = 4;
		private const int GrowFactor = 200;
		private const int DefaultCapacity = 4;
		private static readonly T[] EmptyArray = new T[0];

		[SerializeField]
		private T[] array;
		[SerializeField]
		private int head;
		[SerializeField]
		private int tail;
		[SerializeField]
		private int size;
		[SerializeField]
		private int version;

		[NonSerialized]
		private object syncRoot;


		public SerializableQueue()
		{
			array = EmptyArray;
		}

		public SerializableQueue(int capacity)
		{
			capacity = Math.Max(capacity, 0);
			array = new T[capacity];
			head = 0;
			tail = 0;
			size = 0;
		}

		public SerializableQueue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			array = new T[DefaultCapacity];
			size = 0;
			version = 0;

			using (var en = collection.GetEnumerator())
			{
				while (en.MoveNext())
				{
					Enqueue(en.Current);
				}
			}
		}

		public int Count => size;

		public bool IsSynchronized => false;

		public object SyncRoot
		{
			get {
				if (syncRoot == null)
				{
					System.Threading.Interlocked.CompareExchange(ref syncRoot, new object(), null);
				}
				return syncRoot;
			}
		}

		public void Clear()
		{
			if (head < tail)
				Array.Clear(array, head, size);
			else
			{
				Array.Clear(array, head, array.Length - head);
				Array.Clear(array, 0, tail);
			}

			head = 0;
			tail = 0;
			size = 0;
			version++;
		}

		public void CopyTo(T[] destinationArray, int offset)
		{
			if (destinationArray == null)
			{
				throw new ArgumentNullException(nameof(destinationArray));
			}

			if (offset < 0 || offset > destinationArray.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(offset));
			}

			var length = destinationArray.Length;
			var copyLength = length - offset;
			if (copyLength < size)
			{
				throw new ArgumentException(
					"Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}

			var numToCopy = copyLength < size ? copyLength : size;
			if (numToCopy == 0) return;

			var firstPart = array.Length - head < numToCopy ? array.Length - head : numToCopy;
			Array.Copy(array, head, destinationArray, offset, firstPart);
			numToCopy -= firstPart;
			if (numToCopy > 0)
			{
				Array.Copy(array, 0, destinationArray, offset + array.Length - head, numToCopy);
			}
		}

		void ICollection.CopyTo(Array destinationArray, int offset)
		{
			if (destinationArray == null)
			{
				throw new ArgumentNullException(nameof(destinationArray));
			}

			if (destinationArray.Rank != 1)
			{
				throw new ArgumentException();
			}

			if (destinationArray.GetLowerBound(0) != 0)
			{
				throw new ArgumentException();
			}

			var length = destinationArray.Length;

			if (offset < 0 || offset > length)
			{
				throw new ArgumentOutOfRangeException(nameof(offset));
			}


			if (length - offset < size)
			{
				throw new ArgumentException();
			}

			int numToCopy = length - offset < size ? length - offset : size;
			if (numToCopy == 0) return;

			try
			{
				int firstPart = (array.Length - head < numToCopy) ? array.Length - head : numToCopy;
				Array.Copy(array, head, destinationArray, offset, firstPart);
				numToCopy -= firstPart;

				if (numToCopy > 0)
				{
					Array.Copy(array, 0, destinationArray, offset + array.Length - head, numToCopy);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException();
			}
		}

		public void Enqueue(T item)
		{
			if (size == array.Length)
			{
				var newCapacity = (int)((long)array.Length * GrowFactor / 100);
				if (newCapacity < array.Length + MinimumGrow)
				{
					newCapacity = array.Length + MinimumGrow;
				}
				SetCapacity(newCapacity);
			}

			array[tail] = item;
			tail = (tail + 1) % array.Length;
			size++;
			version++;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}

		public T Dequeue()
		{
			if (size == 0)
				throw new InvalidOperationException();

			var removed = array[head];
			array[head] = default(T);
			head = (head + 1) % array.Length;
			size--;
			version++;
			return removed;
		}

		public T Peek()
		{
			if (size == 0)
				throw new InvalidOperationException();

			return array[head];
		}

		public bool Contains(T item)
		{
			var index = head;
			var count = size;

			var c = EqualityComparer<T>.Default;
			while (count-- > 0)
			{
				if (item == null)
				{
					if (array[index] == null)
						return true;
				}
				else if (array[index] != null && c.Equals(array[index], item))
				{
					return true;
				}
				index = (index + 1) % array.Length;
			}

			return false;
		}

		internal T GetElement(int i)
		{
			return array[(head + i) % array.Length];
		}

		public T[] ToArray()
		{
			T[] arr = new T[size];
			if (size == 0)
				return arr;

			if (head < tail)
			{
				Array.Copy(array, head, arr, 0, size);
			}
			else
			{
				Array.Copy(array, head, arr, 0, array.Length - head);
				Array.Copy(array, 0, arr, array.Length - head, tail);
			}

			return arr;
		}

		private void SetCapacity(int capacity)
		{
			var newArray = new T[capacity];
			if (size > 0)
			{
				if (head < tail)
				{
					Array.Copy(array, head, newArray, 0, size);
				}
				else
				{
					Array.Copy(array, head, newArray, 0, array.Length - head);
					Array.Copy(array, 0, newArray, array.Length - head, tail);
				}
			}

			array = newArray;
			head = 0;
			tail = (size == capacity) ? 0 : size;
			version++;
		}

		public void TrimExcess()
		{
			var threshold = (int)(array.Length * 0.9);
			if (size < threshold)
			{
				SetCapacity(size);
			}
		}

		public struct Enumerator : IEnumerator<T>, IEnumerator
		{
			private SerializableQueue<T> queue;
			private int index;   // -1 = not started, -2 = ended/disposed
			private int version;
			private T currentElement;

			internal Enumerator(SerializableQueue<T> q)
			{
				queue = q;
				version = queue.version;
				index = -1;
				currentElement = default(T);
			}


			public void Dispose()
			{
				index = -2;
				currentElement = default(T);
			}

			public bool MoveNext()
			{
				if (version != queue.version)
					throw new InvalidOperationException();

				if (index == -2)
					return false;

				index++;

				if (index == queue.size)
				{
					index = -2;
					currentElement = default(T);
					return false;
				}

				currentElement = queue.GetElement(index);
				return true;
			}

			public T Current
			{
				get {
					if (index < 0)
					{
						throw new InvalidOperationException();
					}
					return currentElement;
				}
			}

			object IEnumerator.Current
			{
				get {
					if (index < 0)
					{
						throw new InvalidOperationException();
					}
					return currentElement;
				}
			}

			void IEnumerator.Reset()
			{
				if (version != queue.version) throw new InvalidOperationException();
				index = -1;
				currentElement = default(T);
			}
		}
	}
}

