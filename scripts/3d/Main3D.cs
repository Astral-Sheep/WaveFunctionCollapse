using Com.Astral.WFC.Rendering;
using Com.Astral.WFC.Utils;
using Godot;
using System.Collections.Generic;

namespace Com.Astral.WFC._3D
{
	/// <summary>
	/// Main class that calls the <see cref="WaveFunctionCollapse3D"/> algorithm.
	/// </summary>
	public partial class Main3D : Node3D, IMain<Vector3I>
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
			Position = new Vector3((1f - sizeX) / 2f, (1f - sizeY) / 2f, (1f - sizeZ) / 2f) * CELL_SIZE;

			GD.Randomize();
			Data3D.Load();
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
					Render(WaveFunctionCollapse3D.Iterate());
				}
				else
				{
					generating = false;
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
			// Don't restart generation if generating.
			if (generating)
				return;

			Reset();
			WaveFunctionCollapse3D.Init(sizeX, sizeY, sizeZ, limitBounds);
			timer = 0f;
			generating = true;
		}

		public void Render(List<Vector3I> pCellsToRender)
		{
			foreach (Vector3I coordinates in pCellsToRender)
			{
				cells[coordinates.X, coordinates.Y, coordinates.Z].Render(WaveFunctionCollapse3D.Patterns[coordinates.X, coordinates.Y, coordinates.Z]);
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