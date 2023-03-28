using Com.Astral.WFC.Rendering;
using Com.Astral.WFC.Utils;
using Godot;

namespace Com.Astral.WFC._2D
{
	/// <summary>
	/// Main class that calls the <see cref="WaveFunctionCollapse2D"/> algorithm.
	/// </summary>
	public partial class Main2D : Node2D, IMain
	{
		protected const float CELL_SIZE = 60f;

		[Export] protected Key generateKey = Key.Enter;
		[Export] protected bool limitBounds = false;
		[Export] protected float timeBetweenIterations = 0.5f;
		[ExportGroup("Size")]
		[Export] protected uint sizeX = 10u;
		[Export] protected uint sizeY = 10u;
		protected bool generating = false;
		protected double timer = 0d;
		protected Cell2D[,] cells;

		public override void _Ready()
		{
			GD.Randomize();
			Data2D.Load();
			WaveFunctionCollapse2D.Init(sizeX, sizeY, limitBounds);
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

				if (!WaveFunctionCollapse2D.IsCollapsed())
				{
					WaveFunctionCollapse2D.Iterate();
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
			// Return if already initialized.
			if (cells != null)
				return;

			cells = new Cell2D[sizeX, sizeY];
			Cell2D lCell;

			for (int x = 0; x < sizeX; x++)
			{
				for (int y = 0; y < sizeY; y++)
				{
					lCell = new Cell2D();
					AddChild(lCell);
					lCell.Position = new Vector2(x, y) * CELL_SIZE;
					cells[x, y] = lCell;
				}
			}
		}

		public void Generate()
		{
			// Don't restart generation if generating.
			if (generating)
				return;

			Reset();
			WaveFunctionCollapse2D.Init(sizeX, sizeY, limitBounds);
			timer = 0d;
			generating = true;
		}

		public void Render()
		{
			for (int x = 0; x < sizeX; x++)
			{
				for (int y = 0; y < sizeY; y++)
				{
					cells[x, y].Render(WaveFunctionCollapse2D.Patterns[x, y]);
				}
			}
		}

		public void Reset()
		{
			for (int x = 0; x < sizeX; x++)
			{
				for (int y = 0; y < sizeY; y++)
				{
					cells[x, y].Reset();
				}
			}
		}
	}
}