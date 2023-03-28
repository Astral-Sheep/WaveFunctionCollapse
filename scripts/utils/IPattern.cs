using Godot.Collections;

namespace Com.Astral.WFC.Utils
{
	/// <summary>
	/// Interface used to implement n-dimensional patterns.
	/// </summary>
	/// <typeparam name="T">The n-dimensional vector that defines the dimension of the pattern.</typeparam>
	public interface IPattern<T> where T : IVectorI
	{
		/// <summary>
		/// The entropy of the pattern. The closer it is to 0, the closer it is to be collapsed.
		/// </summary>
		public int Entropy { get; }
		/// <summary>
		/// The array containing all the possible states of the pattern.
		/// </summary>
		public Array<int> Possibilities { get; }
		/// <summary>
		/// The normalized coordinates of the pattern.
		/// </summary>
		public T Coordinates { get; }

		/// <summary>
		/// Set a unique state choosen randomly between the remaining possibilities.
		/// The entropy is 0 when the pattern is collapsed.
		/// </summary>
		public void Collapse();
		/// <summary>
		/// Removes all the possibilities which don't correspond to the given parameters.
		/// </summary>
		/// <param name="pDirection">The direction of the constrain (relative to the pattern).</param>
		/// <param name="pPossibilites">The possible neighbors on the axis.</param>
		/// <returns>Returns a boolean telling whether or not the pattern was constrained.</returns>
		public bool Constrain(T pDirection, Array<int> pPossibilites);
		/// <summary>
		/// Return the possible neighbors in the given direction.
		/// </summary>
		/// <param name="pDirection">The direction of the neighbors.</param>
		public Array<int> GetPossibleNeighbors(T pDirection);
		/// <summary>
		/// Return the state of the pattern.
		/// </summary>
		/// <returns>The state of the pattern. -1 if it's undefined.</returns>
		public int GetState();
	}
}
