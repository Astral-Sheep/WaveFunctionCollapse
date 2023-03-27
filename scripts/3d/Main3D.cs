using Com.Astral.WFC.Rendering;
using Com.Astral.WFC.Utils;
using Godot;

namespace Com.Astral.WFC._3D
{
	public partial class Main3D : Node3D, IMain
	{
		protected const float CELL_SIZE = 3f;

		[Export] protected Key generateKey = Key.Enter;
		[Export] protected bool limitBounds = false;
		[Export] protected float timeBetweenIterations = 0.5f;
		[ExportGroup("Size")]
		[Export] protected uint sizeX = 10u;
		[Export] protected uint sizeY = 10u;
		[Export] protected uint sizeZ = 10u;
		protected bool generating = false;
		protected double timer = 0f;
		protected Cell3D[,,] cells;

		public override void _Ready()
		{
			GD.Randomize();
			Data3D.Load();
			WaveFunctionCollapse3D.Init(sizeX, sizeY, sizeZ, limitBounds);
			Init();
			Generate();
		}

		public override void _Process(double pDelta)
		{
			if (!generating)
				return;

			timer += pDelta;

			if (timer >= timeBetweenIterations)
			{
				timer -= timeBetweenIterations;

				if (!WaveFunctionCollapse3D.IsCollapsed())
				{
					WaveFunctionCollapse3D.Iterate();
				}
				else
				{
					generating = false;
				}

				Render();
			}
		}

		public override void _UnhandledKeyInput(InputEvent pEvent)
		{
			if (pEvent is InputEventKey pKeyEvent && pKeyEvent.Keycode == generateKey && pKeyEvent.Pressed)
			{
				Generate();
			}

			base._UnhandledKeyInput(pEvent);
		}

		public void Init()
		{
			if (cells != null)
				return;

			cells = new Cell3D[sizeX, sizeY, sizeZ];
			Cell3D lCell;

			for (int x = 0; x < sizeX; x++)
			{
				for (int y = 0; y < sizeY; y++)
				{
					for (int z = 0; z < sizeZ; z++)
					{
						lCell = new Cell3D();
						AddChild(lCell);
						lCell.Position = new Vector3(x, y, z) * CELL_SIZE;
						cells[x, y, z] = lCell;
					}
				}
			}
		}

		public void Generate()
		{
			if (generating)
				return;

			Reset();
			WaveFunctionCollapse3D.Init(sizeX, sizeY, sizeZ);
			timer = 0f;
			generating = true;
		}

		public void Render()
		{
			for (int x = 0; x < sizeX; x++)
			{
				for (int y = 0; y < sizeY; y++)
				{
					for (int z = 0; z < sizeZ; z++)
					{
						cells[x, y, z].Render(WaveFunctionCollapse3D.Patterns[x, y, z]);
					}
				}
			}
		}

		public void Reset()
		{
			for (int x = 0; x < sizeX; x++)
			{
				for (int y = 0; y < sizeY; y++)
				{
					for (int z = 0; z < sizeZ; z++)
					{
						cells[x, y, z].Reset();
					}
				}
			}
		}
	}
}