namespace Com.Astral.WFC.Utils
{
	/// <summary>
	/// Interface used to implement vectors of int.
	/// </summary>
	public interface IVectorI
	{
		/// <summary>
		/// The size of the vector.
		/// </summary>
		public int Size { get; }
		/// <summary>
		/// The coordinate at the given index (0 for x, 1 for y...).
		/// </summary>
		public int this[int index] { get; set; }
	}
}
