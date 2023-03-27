namespace Com.Astral.WFC.Utils
{
	public interface ICell<TPattern, TVector> 
		where TPattern : IPattern<TVector>
		where TVector : IVectorI
	{
		public void Render(TPattern pPattern);
		public void Reset();
	}
}
