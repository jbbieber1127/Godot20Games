using Godot;

public partial class Ball : CharacterBody2D
{
	[Export]
	NodePath score1Path;
	[Export]
	NodePath score2Path;
	Label score1;
	Label score2;

	private float MaxVelocity = 750;
	
	public override void _Ready()
	{
		base._Ready();
		ResetVelocity();
		score1 = GetNode<Label>(score1Path);
		score2 = GetNode<Label>(score2Path);
	}

	public void ResetVelocity()
	{
		Velocity = new()
		{
			X = 300f,
			Y = 0f
		};
	}

	public void ResetPosition()
	{
		Position = new()
		{
			X = 600,
			Y = 350,
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		var collisionInfo = MoveAndCollide(velocity * (float)delta);
		if (collisionInfo != null)
		{
			var paddleSize = 116f;
			var go = collisionInfo.GetCollider();
			var mult = 1f;
			if (go is Paddle)
			{
				mult = 1.05f;
				var paddle = (Paddle)go;
				var paddleCenter = paddle.GlobalPosition.Y + (paddleSize / 2);
				var addYVel = ((paddleCenter - GlobalPosition.Y) / (paddleSize / 2)) ;
				velocity.Y = addYVel * -100 * Mathf.Abs(Velocity.X/150);
			}
			velocity = velocity.Bounce(collisionInfo.GetNormal())*mult;
			Velocity = velocity;
			if (Velocity.Y > MaxVelocity)
			{
				Velocity = new Vector2(Velocity.X, MaxVelocity);
			}
		}

		if (Position.X < 0) 
		{
			score2.Text = (int.Parse(score2.Text) + 1).ToString();
			ResetPosition();
			ResetVelocity();
		}
		else if (Position.X > 1148)
		{
			score1.Text = (int.Parse(score1.Text) + 1).ToString();
			ResetPosition();
			ResetVelocity();
		}
	}
}
