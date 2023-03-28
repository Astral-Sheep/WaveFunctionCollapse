using Godot;

namespace Com.Astral.WFC._2D
{
	public partial class InteractiveCamera2D : Camera2D
	{
		[Export] protected Key resetKey = Key.Backspace;
		[Export] protected float zoomSpeed = 1f;

		public override void _Process(double pDelta)
		{
			if (Input.IsMouseButtonPressed(MouseButton.Right))
			{
				Position -= Input.GetLastMouseVelocity() / Zoom * (float)pDelta;
			}
		}

		public override void _UnhandledInput(InputEvent pEvent)
		{
			if (pEvent is InputEventMouseButton pMouseEvent)
			{
				if (pMouseEvent.ButtonIndex == MouseButton.WheelDown && Zoom.X > zoomSpeed)
				{
					Zoom -= Vector2.One * zoomSpeed * Zoom.X;
				}
				else if (pMouseEvent.ButtonIndex == MouseButton.WheelUp)
				{
					Zoom += Vector2.One * zoomSpeed * Zoom.X;
				}
			}

			base._UnhandledInput(pEvent);
		}

		public override void _UnhandledKeyInput(InputEvent pEvent)
		{
			if (pEvent is InputEventKey pKeyEvent && pKeyEvent.Keycode == resetKey && pKeyEvent.Pressed)
			{
				Position = DisplayServer.WindowGetSize() / 2;
			}

			base._UnhandledKeyInput(pEvent);
		}
	}
}