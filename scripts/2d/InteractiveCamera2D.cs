using Godot;

namespace Com.Astral.WFC._2D
{
	public partial class InteractiveCamera2D : Camera2D
	{
		protected const string UP_ACTION = "forward";
		protected const string DOWN_ACTION = "back";
		protected const string LEFT_ACTION = "left";
		protected const string RIGHT_ACTION = "right";

		[Export] protected Key resetKey = Key.Backspace;
		[Export] protected float movementSpeed = 100f;

		public override void _Process(double pDelta)
		{
			Vector2 lVelocity = new Vector2(
				Input.GetActionStrength(RIGHT_ACTION) - Input.GetActionStrength(LEFT_ACTION),
				Input.GetActionStrength(DOWN_ACTION) - Input.GetActionStrength(UP_ACTION)
			).Normalized() * movementSpeed;

			Position += lVelocity * (float)pDelta;
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