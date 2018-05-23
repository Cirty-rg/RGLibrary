/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;
using RGSoft.Collections.Generic;

namespace RGSoft.Debuggers
{
	[System.Serializable]
	public class LogCache : SerializableQueue<ConsoleEntry>
	{

		public ConsoleEntry GetOrNew()
		{
			if (Count > 0)
				return Dequeue();
			else
				return new ConsoleEntry(); ;
			//return Count > 0 ? Dequeue() : new ConsoleEntry();
		}

		public void AddRange(List<ConsoleEntry> entries)
		{
			entries.ForEach(Enqueue);
		}
	}

}

