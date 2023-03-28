using System.Collections.Generic;

namespace Com.Astral.WFC.Utils
{
	/// <summary>
	/// Interface used on scripts that call the wave function collapse algorithm.
	/// </summary>
	public interface IMain<T> where T : IVectorI
	{
		/// <summary>
		/// Initialize the cells array.
		/// </summary>
		public void Init();
		/// <summary>
		/// Start the wave function collapse algorithm.
		/// </summary>
		public void Generate();
		/// <summary>
		/// Render the given patterns.
		/// </summary>
		public void Render(List<T> pCellsToRender);
		/// <summary>
		/// Reset all the cells.
		/// </summary>
		public void Reset();
	}
}