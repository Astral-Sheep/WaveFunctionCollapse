namespace Com.Astral.WFC.Utils
{
	/// <summary>
	/// Interface used on scripts that call the wave function collapse algorithm.
	/// </summary>
	public interface IMain
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
		/// Render the collapsed patterns.
		/// </summary>
		public void Render();
		/// <summary>
		/// Reset all the cells.
		/// </summary>
		public void Reset();
	}
}