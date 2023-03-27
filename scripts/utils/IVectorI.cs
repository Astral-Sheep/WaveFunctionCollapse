namespace Com.Astral.WFC.Utils
{
	public interface IVectorI
	{
		public int Size { get; }
		public int this[int index] { get; set; }

		public float Length();
		public int LengthSquared();
	}
}
