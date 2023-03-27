using Com.Astral.WFC.Utils;
using Godot;
using Godot.Collections;

namespace Com.Astral.WFC._3D
{
	public static class Data3D
	{
		private const int DIMENSION = 3;
		private const string PATTERNS_PATH = "res://ressources/3d/patterns.json";
		private const string NEIGHBORS_PATH = "res://ressources/3d/neighbors.json";

		public static Array<int> Patterns => _patternIndices.Duplicate();
		private static Array<int> _patternIndices;
		private static Dictionary<int, Dictionary<Axis, int>> _patterns;
		private static Dictionary<int, Array<int>> _neighbors;

		public static void Load()
		{
			_patterns = DataLoader.LoadPatterns(DIMENSION, PATTERNS_PATH);
			_neighbors = DataLoader.LoadNeighbors(NEIGHBORS_PATH);

			_patternIndices = new Array<int>();

			foreach (int key in _patterns.Keys)
			{
				_patternIndices.Add(key);
			}
		}

		public static Dictionary<Axis, Array<int>> GetNeighbors(int pPattern)
		{
			Dictionary<Axis, Array<int>> lNeighbors = new Dictionary<Axis, Array<int>>();
			Axis lAxis;

			for (int i = 0; i < 6; i++)
			{
				lAxis = DataLoader.GetAxisFromVector(new Vector3I(i < 2 ? i * 2 - 1 : 0, (i < 2 || i > 3) ? 0 : i * 2 - 5, i > 3 ? i * 2 - 9 : 0));
				lNeighbors.Add(lAxis, _neighbors[_patterns[pPattern][lAxis]]);
			}

			return lNeighbors;
		}

		public static Array<int> GetNeighborsOnAxis(int pPattern, Axis pAxis)
		{
			return _neighbors[_patterns[pPattern][pAxis]];
		}

		public static int GetStateOnAxis(int pPattern, Axis pAxis)
		{
			return _patterns[pPattern][pAxis];
		}
	}
}
