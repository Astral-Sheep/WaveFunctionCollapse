using Com.Astral.WFC.Utils;
using Godot;
using System.Collections.Generic;
using ArrayI = Godot.Collections.Array<int>;

namespace Com.Astral.WFC._2D
{
	/// <summary>
	/// Static class containing the 2 dimensional wave function collapse algorithm.
	/// </summary>
	public static class WaveFunctionCollapse2D
	{
		/// <summary>
		/// The 2 dimensional array containing all patterns.
		/// </summary>
		public static Pattern2D[,] Patterns => _patterns.Clone() as Pattern2D[,];
		private static Pattern2D[,] _patterns;

		/// <summary>
		/// Initialize the algorimth with the given size.
		/// </summary>
		/// <param name="pSizeX">The size on the x axis.</param>
		/// <param name="pSizeY">The size on the y axis.</param>
		/// <param name="pInitConstrain">Whether or not we apply a constrain before starting.</param>
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

					// Go to next iteration if no constrain.
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

		/// <summary>
		/// Return whether or not all patterns are collapsed.
		/// </summary>
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

		/// <summary>
		/// Collapse a cell and propagate its state.
		/// </summary>
		public static void Iterate()
		{
			Vector2I lCoordinates = GetMinEntropyCoordinates();
			_patterns[lCoordinates.X, lCoordinates.Y].Collapse();
			Propagate(lCoordinates);
		}

		/// <summary>
		/// Return the coordinates of the pattern with the minimum entropy.
		/// If there are several patterns with the same entropy, it takes a random pattern among
		/// the ones with the minimum entropy.
		/// </summary>
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

		/// <summary>
		/// Propagate the state of the cell at the given coordinates.
		/// </summary>
		private static void Propagate(Vector2I pCoordinates)
		{
			Vector2I lCurrentCoords;
			Vector2I lNeighborCoords;
			ArrayI lNeighborPossibilities;
			ArrayI lPossibleNeighbors;

			Stack<Vector2I> lCoords = new Stack<Vector2I>();
			lCoords.Push(pCoordinates);

			// Loop while there is a constrained pattern in the stack.
			while (lCoords.Count > 0)
			{
				lCurrentCoords = lCoords.Pop();

				// Get the valid neighbors directions.
				foreach (Vector2I dir in GetValidNeighbors(lCurrentCoords))
				{
					lNeighborCoords = lCurrentCoords + dir;
					lNeighborPossibilities = _patterns[lNeighborCoords.X, lNeighborCoords.Y].Possibilities;
					lPossibleNeighbors = _patterns[lCurrentCoords.X, lCurrentCoords.Y].GetPossibleNeighbors(dir);

					// Go to next iteration if neighbor is already collapsed.
					if (lNeighborPossibilities.Count <= 1)
						continue;

					// Constrain neighbor with remaining possibilities.
					if (_patterns[lNeighborCoords.X, lNeighborCoords.Y].Constrain(dir, lPossibleNeighbors))
					{
						// Add to stack if not already in it.
						if (!lCoords.Contains(lNeighborCoords))
						{
							lCoords.Push(lNeighborCoords);
						}
					}
				}
			}
		}

		/// <summary>
		/// Return the valid directions to get neighbors at the given coordinates.
		/// </summary>
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
