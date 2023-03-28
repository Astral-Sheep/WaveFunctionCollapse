namespace Com.Astral.WFC.Utils
{
	/// <summary>
	/// Interface used to render patterns.
	/// </summary>
	/// <typeparam name="TPattern">The type of the pattern to render.</typeparam>
	/// <typeparam name="TVector">Needs to be here because <see cref="IPattern{T}"/> needs a type parameter.</typeparam>
	public interface ICell<TPattern, TVector> 
		where TPattern : IPattern<TVector>
		where TVector : IVectorI
	{
		/// <summary>
		/// Method called to render the given pattern.
		/// </summary>
		/// <param name="pPattern">The pattern to render.</param>
		public void Render(TPattern pPattern);
		/// <summary>
		/// Method called to remove the renderer (it doesn't destroy the cell, just the renderer).
		/// </summary>
		public void Reset();
	}
}