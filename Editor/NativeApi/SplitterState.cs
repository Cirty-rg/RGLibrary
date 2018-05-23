/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System;
using System.Linq;
using System.Reflection;
using RGSoft.Reflections;
using UnityEngine;

namespace RGSoft.NativeApi
{
	[Serializable]
	public class SplitterState
	{
		private const int DefaultSplitSize = 6;

		[SerializeField]
		private int id;
		[SerializeField]
		private int splitterInitialOffset;
		[SerializeField]
		private int currentActiveSplitter = -1;
		[SerializeField]
		private int[] realSizes;
		[SerializeField]
		private float[] relativeSizes;
		[SerializeField]
		private int[] minSizes;
		[SerializeField]
		private int[] maxSizes;
		[SerializeField]
		private int lastTotalSize = 0;
		[SerializeField]
		private int splitSize;
		[SerializeField]
		private float xOffset;

		private object state;

		public int Id
		{
			get { return id; }

			set { id = value; }
		}

		public int SplitterInitialOffset
		{
			get { return splitterInitialOffset; }

			set { splitterInitialOffset = value; }
		}

		public int CurrentActiveSplitter
		{
			get { return currentActiveSplitter; }

			set { currentActiveSplitter = value; }
		}

		public int[] RealSizes
		{
			get { return realSizes; }

			set { realSizes = value; }
		}

		public float[] RelativeSizes
		{
			get { return relativeSizes; }

			set { relativeSizes = value; }
		}

		public int[] MinSizes
		{
			get { return minSizes; }

			set { minSizes = value; }
		}

		public int[] MaxSizes
		{
			get { return maxSizes; }

			set { maxSizes = value; }
		}

		public int LastTotalSize
		{
			get { return lastTotalSize; }

			set { lastTotalSize = value; }
		}

		public int SplitSize
		{
			get { return splitSize; }

			set { splitSize = value; }
		}

		public float XOffset
		{
			get { return xOffset; }

			set { xOffset = value; }
		}

		public object State
		{
			get {
				if (state == null)
					CreateState();
				return state;
			}
		}

		public SplitterState(params float[] relativeSizes)
		{
			Init(relativeSizes, null, null, DefaultSplitSize);
		}

		public SplitterState(int[] realSizes, int[] minSizes, int[] maxSizes)
		{
			this.realSizes = realSizes;
			this.minSizes = minSizes ?? new int[realSizes.Length];
			this.maxSizes = maxSizes ?? new int[realSizes.Length];
			relativeSizes = new float[realSizes.Length];
			splitSize = DefaultSplitSize;
			this.RealToRelativeSizes();
		}

		public SplitterState(float[] relativeSizes, int[] minSizes, int[] maxSizes)
		{
			Init(relativeSizes, minSizes, maxSizes, 0);
		}

		public SplitterState(float[] relativeSizes, int[] minSizes, int[] maxSizes, int splitSize)
		{
			Init(relativeSizes, minSizes, maxSizes, splitSize);
		}

		private void Init(float[] relative, int[] min, int[] max, int split)
		{
			relativeSizes = relative;
			minSizes = min ?? new int[relativeSizes.Length];
			maxSizes = max ?? new int[relativeSizes.Length];
			splitSize = split > 0 ? split : DefaultSplitSize;
			NormalizeRelativeSizes();
		}


		public void NormalizeRelativeSizes()
		{
			var total = 1f;
			var num2 = relativeSizes.Sum();
			for (var i = 0; i < relativeSizes.Length; i++)
			{
				relativeSizes[i] = relativeSizes[i] / num2;
				total -= relativeSizes[i];
			}
			relativeSizes[relativeSizes.Length - 1] += total;
		}

		public void RealToRelativeSizes()
		{
			var num = 1f;
			var num2 = realSizes.Sum();
			for (var i = 0; i < realSizes.Length; i++)
			{
				relativeSizes[i] = (float)realSizes[i] / num2;
				num -= relativeSizes[i];
			}
			if (relativeSizes.Length > 0)
			{
				relativeSizes[relativeSizes.Length - 1] += num;
			}
		}

		private void CreateState()
		{
			state = Activator.CreateInstance(SplitterStateType, relativeSizes, minSizes, maxSizes, splitSize);
			//IdField.SetValue(state, id);
			//SplitterInitialOffsetField.SetValue(state, splitterInitialOffset);
			//CurrentActiveSplitterField.SetValue(state, currentActiveSplitter);
			//RealSizesField.SetValue(state, realSizes);
			//RelativeSizesField.SetValue(state, relativeSizes);
			//MinSizesField.SetValue(state, minSizes);
			//MaxSizesField.SetValue(state, maxSizes);
			//LastTotalSizeField.SetValue(state, lastTotalSize);
			//SplitSizeField.SetValue(state, splitSize);
			//XOffsetField.SetValue(state, xOffset);
		}

		public void Apply()
		{
			id = (int)IdField.GetValue(state);
			splitterInitialOffset = (int)SplitterInitialOffsetField.GetValue(state);
			currentActiveSplitter = (int)CurrentActiveSplitterField.GetValue(state);
			realSizes = (int[])RealSizesField.GetValue(state);
			relativeSizes = (float[])RelativeSizesField.GetValue(state);
			minSizes = (int[])MinSizesField.GetValue(state);
			maxSizes = (int[])MaxSizesField.GetValue(state);
			lastTotalSize = (int)LastTotalSizeField.GetValue(state);
			splitSize = (int)SplitSizeField.GetValue(state);
			xOffset = (float)XOffsetField.GetValue(state);
		}

		private static readonly Type SplitterStateType;
		private static readonly FieldInfo IdField;
		private static readonly FieldInfo SplitterInitialOffsetField;
		private static readonly FieldInfo CurrentActiveSplitterField;
		private static readonly FieldInfo RealSizesField;
		private static readonly FieldInfo RelativeSizesField;
		private static readonly FieldInfo MinSizesField;
		private static readonly FieldInfo MaxSizesField;
		private static readonly FieldInfo LastTotalSizeField;
		private static readonly FieldInfo SplitSizeField;
		private static readonly FieldInfo XOffsetField;


		static SplitterState()
		{
			SplitterStateType = TypeLoader.Load("UnityEditor", "UnityEditor.SplitterState");
			const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
			IdField = SplitterStateType.GetField("ID", flags);
			SplitterInitialOffsetField = SplitterStateType.GetField("splitterInitialOffset", flags);
			CurrentActiveSplitterField = SplitterStateType.GetField("currentActiveSplitter", flags);
			RealSizesField = SplitterStateType.GetField("realSizes", flags);
			RelativeSizesField = SplitterStateType.GetField("relativeSizes", flags);
			MinSizesField = SplitterStateType.GetField("minSizes", flags);
			MaxSizesField = SplitterStateType.GetField("maxSizes", flags);
			LastTotalSizeField = SplitterStateType.GetField("lastTotalSize", flags);
			SplitSizeField = SplitterStateType.GetField("splitSize", flags);
			XOffsetField = SplitterStateType.GetField("xOffset", flags);
		}
	}
}