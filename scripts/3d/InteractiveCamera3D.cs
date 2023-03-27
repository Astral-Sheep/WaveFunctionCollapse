using Godot;

namespace Com.Astral.WFC._3D
{
	public partial class InteractiveCamera3D : Camera3D
	{
		protected const string FORWARD_ACTION = "forward";
		protected const string BACKWARD_ACTION = "back";
		protected const string UP_ACTION = "up";
		protected const string DOWN_ACTION = "down";
		protected const string LEFT_ACTION = "left";
		protected const string RIGHT_ACTION = "right";

		[ExportGroup("Movement")]
		[Export] protected float movementSpeed = 5f;
		[ExportSubgroup("Rotation")]
		[Export] protected float xAxisSpeed = 0.01f;
		[Export] protected float yAxisSpeed = 0.01f;

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			float lHorizontal = Input.GetActionStrength(RIGHT_ACTION) - Input.GetActionStrength(LEFT_ACTION);
			float lForward = Input.GetActionStrength(FORWARD_ACTION) - Input.GetActionStrength(BACKWARD_ACTION);

			Vector3 lVelocity = new Vector3(
				Mathf.Cos(Rotation.Y) * lHorizontal + Mathf.Cos(Rotation.Y + Mathf.Pi / 2f) * lForward,
				Input.GetActionStrength(UP_ACTION) - Input.GetActionStrength(DOWN_ACTION),
				-Mathf.Sin(Rotation.Y) * lHorizontal - Mathf.Sin(Rotation.Y + Mathf.Pi / 2f) * lForward
			) * movementSpeed;

			Position += lVelocity * lDelta;
		}

		public override void _UnhandledInput(InputEvent pEvent)
		{
			if (Input.IsMouseButtonPressed(MouseButton.Right) && pEvent is InputEventMouseMotion pMotionEvent)
			{
				Rotation += new Vector3(
					Mathf.DegToRad(-pMotionEvent.Velocity.Y * yAxisSpeed),
					Mathf.DegToRad(-pMotionEvent.Velocity.X * xAxisSpeed),
					0f
				);
			}

			base._UnhandledInput(pEvent);
		}
	}
}
