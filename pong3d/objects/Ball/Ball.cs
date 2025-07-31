using Godot;

public partial class Ball : RigidBody3D
{
	public Vector3 Velocity { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        Vector3 velocity = Velocity;

        var collisionInfo = MoveAndCollide(velocity * (float)delta);
        var mult = 1.05f;
        if (collisionInfo != null)
		{
            velocity = velocity.Bounce(collisionInfo.GetNormal()) * mult;
		}

		Velocity = velocity;
    }
}
