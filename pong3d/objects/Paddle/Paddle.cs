using Godot;
using System;

public partial class Paddle : CharacterBody3D
{

    public const float Speed = 5.0f;
    private Vector3 _originalPos;

    [Export]
    public string UpKeyCode;
    [Export]
    public string DownKeyCode;
    [Export]
    public string LeftKeyCode;
    [Export]
    public string RightKeyCode;

    public override void _Ready()
    {
        base._Ready();
        _originalPos = Position;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        var up = Convert.ToInt32(Input.IsActionPressed(UpKeyCode)) * (Transform.Basis.Y);
        var down = Convert.ToInt32(Input.IsActionPressed(DownKeyCode)) * (Transform.Basis.Y*-1);
        var left = Convert.ToInt32(Input.IsActionPressed(LeftKeyCode)) * (Transform.Basis.Z*-1);
        var right = Convert.ToInt32(Input.IsActionPressed(RightKeyCode)) * (Transform.Basis.Z);

        var direction = up + down + left + right;
        Position += (direction * Speed * (float)delta);

        Position = new(_originalPos.X, Position.Y, Position.Z);
    }
}
