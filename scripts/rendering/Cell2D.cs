using Com.Astral.WFC._2D;
using Com.Astral.WFC.Utils;
using Godot;
using System.Collections.Generic;
using Vector2I = Com.Astral.WFC._2D.Vector2I;

namespace Com.Astral.WFC.Rendering
{
	/// <summary>
	/// A <see cref="Pattern2D"/> renderer.
	/// </summary>
	public partial class Cell2D : Node2D, ICell<Pattern2D, Vector2I>
	{
		protected const string TEXTURE_PATH = "res://assets/sprites/cell_part.png";
		protected const float TEXTURE_SIZE = 20f;
		protected List<Sprite2D> sprites;

		public void Render(Pattern2D pPattern)
		{
			// Return if already renderer or given an undefined pattern.
			if (sprites != null || pPattern.Entropy > 0)
				return;

			sprites = new List<Sprite2D>();
			int lState = pPattern.GetState();

			for (sbyte i = 0; i < 2; i++)
			{
				// Render positive axis.
				if (Data2D.GetStateOnAxis(lState, (Axis)(1 << i)) > 0)
				{
					CreateSprite(new Vector2(i == 0 ? 1 : 0, i == 1 ? 1 : 0));
				}

				// Render negative axis.
				if (Data2D.GetStateOnAxis(lState, (Axis)~(1 << i)) > 0)
				{
					CreateSprite(new Vector2(i == 0 ? -1 : 0, i == 1 ? -1 : 0));
				}
			}

			// Render mid if at least 1 side is rendered.
			if (sprites.Count > 0)
			{
				CreateSprite(Vector2.Zero);
			}
		}

		public void Reset()
		{
			// Return if not rendered.
			if (sprites == null)
				return;

			for (int i = sprites.Count - 1; i >= 0; i--)
			{
				sprites[i].QueueFree();
				sprites.RemoveAt(i);
			}

			sprites = null;
		}

		/// <summary>
		/// Instantiate, initialize and store a sprite.
		/// </summary>
		protected void CreateSprite(Vector2 pPosition)
		{
			Sprite2D lSprite = new Sprite2D
			{
				Texture = GD.Load<Texture2D>(TEXTURE_PATH)
			};

			AddChild(lSprite);
			lSprite.Position = pPosition * TEXTURE_SIZE;
			sprites.Add(lSprite);
		}
	}
}