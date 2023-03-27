using Godot.Collections;

namespace Com.Astral.WFC.Utils
{
	public interface IPattern<T> where T : IVectorI
	{
		public int Entropy { get; }
		public Array<int> Possibilities { get; }
		public T Coordinates { get; }

		public void Collapse();
		public bool Constrain(T pDirection, Array<int> pPossibilites);
		public Array<int> GetPossibleNeighbors(T pDirection);
		public int GetState();
	}
}
