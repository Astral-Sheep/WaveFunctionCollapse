using Godot;
using Godot.Collections;
using System;

namespace Com.Astral.WFC.Utils
{
	public static class DataLoader
	{
		public static Dictionary<int, Dictionary<Axis, int>> LoadPatterns(uint pDimension, string pPath)
		{
			Dictionary<int, Dictionary<Axis, int>> lPatterns;

			if (ResourceLoader.Exists(pPath))
			{
				lPatterns = new Dictionary<int, Dictionary<Axis, int>>();

				Dictionary<string, Dictionary<string, int>> lData = ResourceLoader
					.Load<Json>(pPath).Data
					.As<Dictionary<string, Dictionary<string, int>>>();

				Dictionary<Axis, int> lAxis;

				foreach (string key in lData.Keys)
				{
					lAxis = new Dictionary<Axis, int>();

					foreach (string subKey in lData[key].Keys)
					{
						lAxis.Add(Enum.Parse<Axis>(subKey), lData[key][subKey]);
					}

					lPatterns.Add(int.Parse(key), lAxis);
				}
			}
			else
			{
				lPatterns = new Dictionary<int, Dictionary<Axis, int>>();
				Dictionary<Axis, int> lNeighbors;
				int lAxisCount = (int)pDimension * 2;

				for (int i = 0; i < (1 << lAxisCount); i++)
				{
					lNeighbors = new Dictionary<Axis, int>();

					for (int j = 0; j < pDimension; j++)
					{
						lNeighbors.Add((Axis)(1 << j), (i & (1 << (2 * j + 1))) > 0 ? 1 : 0);
						lNeighbors.Add((Axis)~(1 << j), (i & (1 << (2 * j))) > 0 ? 1 : 0);
					}

					lPatterns.Add(i, lNeighbors);
				}

				Dictionary<int, Dictionary<string, int>> lSave = new Dictionary<int, Dictionary<string, int>>();
				Dictionary<string, int> lAxis;

				foreach (int key in lPatterns.Keys)
				{
					lAxis = new Dictionary<string, int>();

					foreach (Axis subKey in lPatterns[key].Keys)
					{
						lAxis.Add(subKey.ToString(), lPatterns[key][subKey]);
					}

					lSave.Add(key, lAxis);
				}

				FileAccess lFile = FileAccess.Open(pPath, FileAccess.ModeFlags.Write);
				lFile.StoreString(Json.Stringify(lSave, "\t"));
				lFile.Close();
			}

			return lPatterns;
		}

		public static Dictionary<int, Array<int>> LoadNeighbors(string pPath)
		{
			if (!ResourceLoader.Exists(pPath))
				throw new NullReferenceException($"No file found at {pPath}");

			Dictionary<int, Array<int>> lNeighbors = new Dictionary<int, Array<int>>();

			Dictionary<string, Array<int>> lData = ResourceLoader
				.Load<Json>(pPath).Data
				.As<Dictionary<string, Array<int>>>();

			foreach (string key in lData.Keys)
			{
				lNeighbors.Add(int.Parse(key), lData[key]);
			}

			return lNeighbors;
		}

		public static Axis GetAxisFromVector(IVectorI pVector)
		{
			for (int i = 0; i <= pVector.Size; i++)
			{
				if (pVector[i] != 0)
				{
					return (Axis)(pVector[i] < 0 ? ~(1 << i) : (1 << i));
				}
			}

			return Axis.None;
		}
	}
}
