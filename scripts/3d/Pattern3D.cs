using Com.Astral.WFC.Utils;
using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace Com.Astral.WFC._3D
{
	/// <summary>
	/// Representation of a 3 dimensional pattern.
	/// </summary>
	public struct Pattern3D : IPattern<Vector3I>
	{
		public int Entropy => _possibilities.Count - 1;
		public Array<int> Possibilities => _possibilities;
		public Vector3I Coordinates => _coordinates;

		private Array<int> _possibilities;
		private Vector3I _coordinates;

		public Pattern3D(Array<int> pPossibilities, Vector3I pCoordinates)
		{
			_possibilities = pPossibilities;
			_coordinates = pCoordinates;
		}

		public void Collapse()
		{
			// Return if already collapsed.
			if (_possibilities.Count <= 1)
				return;

			// Choose random state.
			_possibilities = new Array<int>() { _possibilities[Mathf.FloorToInt(GD.Randf() * _possibilities.Count)] };
		}

		public bool Constrain(Vector3I pDirection, Array<int> pPossibleNeighbors)
		{
			// Can't be constrained if already collapsed.
			if (Entropy == 0)
				return false;

			int lLength = _possibilities.Count;
			Axis lAxis = DataLoader.GetAxisFromVector(-pDirection);

			for (int i = Possibilities.Count - 1; i >= 0; i--)
			{
				if (!pPossibleNeighbors.Contains(Data3D.GetStateOnAxis(_possibilities[i], lAxis)))
				{
					_possibilities.RemoveAt(i);
				}
			}

			return lLength > Possibilities.Count;
		}

		public Array<int> GetPossibleNeighbors(Vector3I pDirection)
		{
			Axis lAxis = DataLoader.GetAxisFromVector(pDirection);
			Array<int> lPossibleNeighbors = new Array<int>();

			foreach (int possibility in Possibilities)
			{
				foreach (int neighbor in Data3D.GetNeighborsOnAxis(possibility, lAxis))
				{
					if (!lPossibleNeighbors.Contains(neighbor))
					{
						lPossibleNeighbors.Add(neighbor);
					}
				}
			}

			return lPossibleNeighbors;
		}

		public int GetState()
		{
			// Return -1 if not collapsed.
			if (_possibilities.Count > 1)
				return -1;

			return _possibilities[0];
		}

		/// <summary>
		/// Set an initial constrain on the given <see cref="Axis"/>, as if there were an empty cell on it.
		/// </summary>
		public void InitConstrain(Axis pAxis)
		{
			List<int> lPossibleNeighbors = new List<int>();

			// Need this to use method Contains, because godot array seems to be broken just in this method.
			foreach (int possibility in Data3D.GetNeighborsOnAxis(0, pAxis))
			{
				lPossibleNeighbors.Add(possibility);
			}

			for (int i = Possibilities.Count - 1; i >= 0; i--)
			{
				if (!lPossibleNeighbors.Contains(Possibilities[i]))
				{
					Possibilities.RemoveAt(i);
				}
			}
		}
	}
}
