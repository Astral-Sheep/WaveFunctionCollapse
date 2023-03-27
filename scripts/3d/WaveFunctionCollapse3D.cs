using Com.Astral.WFC.Utils;
using Godot;
using System.Collections.Generic;
using ArrayI = Godot.Collections.Array<int>;

namespace Com.Astral.WFC._3D
{
	public static class WaveFunctionCollapse3D
	{
		public static Pattern3D[,,] Patterns => _patterns.Clone() as Pattern3D[,,];
		private static Pattern3D[,,] _patterns;

		public static void Init(uint pSizeX, uint pSizeY, uint pSizeZ, bool pInitConstrain = false)
		{
			_patterns = new Pattern3D[pSizeX, pSizeY, pSizeZ];
			Pattern3D lPattern;

			for (int x = 0; x < pSizeX; x++)
			{
				for (int y = 0; y < pSizeY; y++)
				{
					for (int z = 0; z < pSizeZ; z++)
					{
						lPattern = new Pattern3D(Data3D.Patterns, new Vector3I(x, y, z));
						_patterns[x, y, z] = lPattern;

						if (!pInitConstrain)
							continue;

						if (x == 0)
						{
							lPattern.InitConstrain(Axis.PosX);
						}
						else if (x == pSizeX - 1)
						{
							lPattern.InitConstrain(Axis.NegX);
						}

						if (y == 0)
						{
							lPattern.InitConstrain(Axis.PosY);
						}
						else if (y == pSizeY - 1)
						{
							lPattern.InitConstrain(Axis.NegY);
						}

						if (z == 0)
						{
							lPattern.InitConstrain(Axis.PosZ);
						}
						else if (z == pSizeZ - 1)
						{
							lPattern.InitConstrain(Axis.NegZ);
						}
					}
				}
			}
		}

		public static bool IsCollapsed()
		{
			for (int x = 0; x < _patterns.GetLength(0); x++)
			{
				for (int y = 0; y < _patterns.GetLength(1); y++)
				{
					for (int z = 0; z < _patterns.GetLength(2); z++)
					{
						if (_patterns[x, y, z].Entropy > 0)
							return false;
					}
				}
			}

			return true;
		}

		public static void Iterate()
		{
			Vector3I lCoordinates = GetMinEntropyCoordinates();
			_patterns[lCoordinates.X, lCoordinates.Y, lCoordinates.Z].Collapse();
			Propagate(lCoordinates);
		}

		private static Vector3I GetMinEntropyCoordinates()
		{
			int lMinEntropy = int.MaxValue;
			List<Vector3I> lCoordinates = new List<Vector3I>();
			Pattern3D lPattern;

			for (int x = 0; x < _patterns.GetLength(0); x++)
			{
				for (int y = 0; y < _patterns.GetLength(1); y++)
				{
					for (int z = 0; z < _patterns.GetLength(2); z++)
					{
						lPattern = _patterns[x, y, z];

						if (lPattern.Entropy > 0 && lPattern.Entropy <= lMinEntropy)
						{
							if (lPattern.Entropy < lMinEntropy)
							{
								lMinEntropy = lPattern.Entropy;
								lCoordinates.Clear();
							}

							lCoordinates.Add(lPattern.Coordinates);
						}
					}
				}
			}

			if (lCoordinates.Count > 1)
			{
				return lCoordinates[Mathf.FloorToInt(GD.Randf() * lCoordinates.Count)];
			}

			return lCoordinates[0];
		}

		private static void Propagate(Vector3I pCoordinates)
		{
			Vector3I lCurrentCoords;
			Vector3I lNeighborCoords;
			ArrayI lNeighborPossibilities;
			ArrayI lPossibleNeighbors;

			Stack<Vector3I> lCoords = new Stack<Vector3I>();
			lCoords.Push(pCoordinates);

			while (lCoords.Count > 0)
			{
				lCurrentCoords = lCoords.Pop();

				foreach (Vector3I dir in GetValidNeighbors(lCurrentCoords))
				{
					lNeighborCoords = lCurrentCoords + dir;
					lNeighborPossibilities = _patterns[lNeighborCoords.X, lNeighborCoords.Y, lNeighborCoords.Z].Possibilities;
					lPossibleNeighbors = _patterns[lCurrentCoords.X, lCurrentCoords.Y, lCurrentCoords.Z].GetPossibleNeighbors(dir);

					if (lNeighborPossibilities.Count <= 1)
						continue;

					if (_patterns[lNeighborCoords.X, lNeighborCoords.Y, lNeighborCoords.Z].Constrain(dir, lPossibleNeighbors))
					{
						if (!lCoords.Contains(lNeighborCoords))
						{
							lCoords.Push(lNeighborCoords);
						}
					}
				}
			}
		}

		private static List<Vector3I> GetValidNeighbors(Vector3I pCoordinates)
		{
			List<Vector3I> lNeighbors = new List<Vector3I>();

			if (pCoordinates.X > 0)
			{
				lNeighbors.Add(Vector3I.Left);
			}

			if (pCoordinates.X < _patterns.GetLength(0) - 1)
			{
				lNeighbors.Add(Vector3I.Right);
			}

			if (pCoordinates.Y > 0)
			{
				lNeighbors.Add(Vector3I.Down);
			}

			if (pCoordinates.Y < _patterns.GetLength(1) - 1)
			{
				lNeighbors.Add(Vector3I.Up);
			}

			if (pCoordinates.Z > 0)
			{
				lNeighbors.Add(Vector3I.Forward);
			}

			if (pCoordinates.Z < _patterns.GetLength(2) - 1)
			{
				lNeighbors.Add(Vector3I.Back);
			}

			return lNeighbors;
		}
	}
}
