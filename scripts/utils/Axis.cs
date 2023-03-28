namespace Com.Astral.WFC.Utils
{
	/// <summary>
	/// The axis in 3 dimensions. (more dimensions can be added with a limit of 8 dimensions)
	/// </summary>
	public enum Axis : sbyte
	{
		None = 0,
		NegX = ~1,
		PosX = 1,
		NegY = ~2,
		PosY = 2,
		NegZ = ~4,
		PosZ = 4
	}
}