using Com.Astral.WFC.Rendering;
using Com.Astral.WFC.Utils;
using Godot;
using System.Collections.Generic;

namespace Com.Astral.WFC._2D
{
	/// <summary>
	/// Main class that calls the <see cref="WaveFunctionCollapse2D"/> algorithm.
	/// </summary>
	public partial class Main2D : Node2D, IMain<Vector2I>
	{
		protected const float CELL_SIZE = 60f;

		[Export] protected Key generateKey = Key.Enter;
		[Export] protected bool limitBounds = false;
		[Export] protected float timeBetweenIterations = 0.5f;
		[ExportGroup("Size")]
		[Export] protected uint sizeX = 10u;
		[Export] protected uint sizeY = 10u;
		protected double timer = 0d;
		protected Cell2D[,] cells;

		public override void _Ready()
		{
			Position = new Vector2((1f - sizeX) / 2f, (1f - sizeY) / 2f) * CELL_SIZE;
			SetProcess(false);

			GD.Randomize();
			Data2D.Load();
			Init();
			Generate();
		}

		public override void _Process(double pDelta)
		{
			timer += pDelta;

			if (timer >= timeBetweenIterations)
			{
				timer -= timeBetweenIterations;

				if (!WaveFunctionCollapse2D.IsCollapsed())
				{
					Render(WaveFunctionCollapse2D.Iterate());
				}
				else
				{
					SetProcess(false);
				}
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
			// Don't restart generation if processing.
			if (IsProcessing())
				return;

			Reset();
			WaveFunctionCollapse2D.Init(sizeX, sizeY, limitBounds);
			timer = 0d;
			SetProcess(true);
		}

		public void Render(List<Vector2I> pCellsToRender)
		{
			foreach (Vector2I coordinates in pCellsToRender)
			{
				cells[coordinates.X, coordinates.Y].Render(WaveFunctionCollapse2D.Patterns[coordinates.X, coordinates.Y]);
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