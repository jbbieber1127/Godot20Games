using Godot;
using static System.Formats.Asn1.AsnWriter;

public partial class Game : Node
{

	[Export]
	public NodePath BallPath { get ; set; }
    [Export]
    public NodePath Label1Path { get; set; }
    [Export]
    public NodePath Label2Path { get; set; }


    private Ball _ball;
	private Label _label1;
	private Label _label2;


	private bool reverseDirection = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		_ball = GetNode<Ball>(BallPath);
        _label1 = GetNode<Label>(Label1Path);
        _label2 = GetNode<Label>(Label2Path);

        StartPoint();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_ball.Position.X > 10)
		{
			// then paddle 2 won
			StartPoint();
            _label2.Text = (int.Parse(_label2.Text) + 1).ToString();
        }
		else if (_ball.Position.X < -10)
		{
			// paddle 1 won
			StartPoint();
			_label1.Text = (int.Parse(_label1.Text) + 1).ToString();
		}
	}

	private void StartPoint()
	{
		_ball.Position = new Vector3(0, 5, 0);

		float min_x = 2f;
		float max_x = 4f;
		float min_y = 1f;
        float max_y = 3f;
        float min_z = 1f;
		float max_z = 3f;
		var velocity = new Vector3((float)GD.RandRange(min_x, max_x), (float)GD.RandRange(min_y, max_y), (float)GD.RandRange(min_z, max_z));
		if (reverseDirection)
		{
			velocity *= -1;
		}
		_ball.Velocity = velocity;

		reverseDirection = !reverseDirection;
    }
}
