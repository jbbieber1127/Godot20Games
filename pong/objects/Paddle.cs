using Godot;

public partial class Paddle : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	[Export]
	public string UpKeyCode;
	[Export]
	public string DownKeyCode;
	[Export]
	public string LeftKeyCode;
	[Export]
	public string RightKeyCode;

	private Vector2 _originalPos;

    public override void _Ready()
    {
		base._Ready();
		_originalPos = Position;
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity += GetDeceleration() * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed(UpKeyCode))
		{
			velocity.Y = JumpVelocity;
		}

		if (Input.IsActionJustPressed(DownKeyCode))
		{
			velocity.Y = -JumpVelocity;
		}

		if (Position.Y < -50 || Position.Y > 598)
		{
			velocity = new Vector2(velocity.X, -velocity.Y);
		}
		Velocity = velocity;
		MoveAndSlide();
		Position = new(_originalPos.X, Position.Y);
	}

	public Vector2 GetDeceleration()
	{
		Vector2 velocity = Velocity;

		var l2 = velocity.LengthSquared();
		if (l2 > 300 * 300)
		{
			return -velocity * 4f;
		}
		else if (l2 > 250 * 250)
		{
			return -velocity * 3f;
		}
		else if (l2 > 200 * 200)
		{
			return -velocity * 2f;
		}
		else if (l2 > 150 * 150)
		{
			return -velocity * 1f;
		}
		else if (l2 > 100 * 100)
		{
			return -velocity * 0.5f;
		}
		else if (l2 > 50 * 50)
		{
			return -velocity * 2f;
		}
		else
		{
			return -velocity;
		}
	}
}
