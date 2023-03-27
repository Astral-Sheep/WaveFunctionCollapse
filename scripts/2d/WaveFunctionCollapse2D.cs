using Com.Astral.WFC._3D;
using Com.Astral.WFC.Utils;
using Godot;
using System.Collections.Generic;
using ArrayI = Godot.Collections.Array<int>;

namespace Com.Astral.WFC._2D
{
	public static class WaveFunctionCollapse2D
	{
		public static Pattern2D[,] Patterns => _patterns.Clone() as Pattern2D[,];
		private static Pattern2D[,] _patterns;

		public static void Init(uint pSizeX, uint pSizeY, bool pInitConstrain = false)
		{
			_patterns = new Pattern2D[pSizeX, pSizeY];
			Pattern2D lPattern;

			for (int x = 0; x < pSizeX; x++)
			{
				for (int y = 0; y < pSizeY; y++)
				{
					lPattern = new Pattern2D(Data2D.Patterns, new Vector2I(x, y));
					_patterns[x, y] = lPattern;

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
				}
			}
		}

		public static bool IsCollapsed()
		{
			for (int x = 0; x < _patterns.GetLength(0); x++)
			{
				for (int y = 0; y < _patterns.GetLength(1); y++)
				{
					if (_patterns[x, y].Entropy > 0)
						return false;
				}
			}

			return true;
		}

		public static void Iterate()
		{
			Vector2I lCoordinates = GetMinEntropyCoordinates();
			_patterns[lCoordinates.X, lCoordinates.Y].Collapse();
			Propagate(lCoordinates);
		}

		private static Vector2I GetMinEntropyCoordinates()
		{
			int lMinEntropy = int.MaxValue;
			List<Vector2I> lCoordinates = new List<Vector2I>();
			Pattern2D lPattern;

			for (int x = 0; x < _patterns.GetLength(0); x++)
			{
				for (int y = 0; y < _patterns.GetLength(1); y++)
				{
					lPattern = _patterns[x, y];

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

			if (lCoordinates.Count > 1)
			{
				return lCoordinates[Mathf.FloorToInt(GD.Randf() * lCoordinates.Count)];
			}

			return lCoordinates[0];
		}

		private static void Propagate(Vector2I pCoordinates)
		{
			Vector2I lCurrentCoords;
			Vector2I lNeighborCoords;
			ArrayI lNeighborPossibilities;
			ArrayI lPossibleNeighbors;

			Stack<Vector2I> lCoords = new Stack<Vector2I>();
			lCoords.Push(pCoordinates);

			while (lCoords.Count > 0)
			{
				lCurrentCoords = lCoords.Pop();

				foreach (Vector2I dir in GetValidNeighbors(lCurrentCoords))
				{
					lNeighborCoords = lCurrentCoords + dir;
					lNeighborPossibilities = _patterns[lNeighborCoords.X, lNeighborCoords.Y].Possibilities;
					lPossibleNeighbors = _patterns[lCurrentCoords.X, lCurrentCoords.Y].GetPossibleNeighbors(dir);

					if (lNeighborPossibilities.Count <= 1)
						continue;

					if (_patterns[lNeighborCoords.X, lNeighborCoords.Y].Constrain(dir, lPossibleNeighbors))
					{
						if (!lCoords.Contains(lNeighborCoords))
						{
							lCoords.Push(lNeighborCoords);
						}
					}
				}
			}
		}

		private static List<Vector2I> GetValidNeighbors(Vector2I pCoordinates)
		{
			List<Vector2I> lNeighbors = new List<Vector2I>();

			if (pCoordinates.X > 0)
			{
				lNeighbors.Add(Vector2I.Left);
			}

			if (pCoordinates.X < _patterns.GetLength(0) - 1)
			{
				lNeighbors.Add(Vector2I.Right);
			}

			if (pCoordinates.Y > 0)
			{
				lNeighbors.Add(Vector2I.Up);
			}

			if (pCoordinates.Y < _patterns.GetLength(1) - 1)
			{
				lNeighbors.Add(Vector2I.Down);
			}

			return lNeighbors;
		}
	}
}
