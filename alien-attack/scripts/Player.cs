using Godot;

namespace AlienAttack.scripts;

public partial class Player : CharacterBody2D
{
    public int Force { get; set; } = 100;
    
    [Signal]
    public delegate void LivesDepletedEventHandler();
    
    private PackedScene _rocketScene;
    private Node _rocketContainer;
    private AudioStreamPlayer _missileFire;
    
    public override void _Ready()
    {
        _rocketScene = GD.Load<PackedScene>("res://scenes/rocket.tscn");
        _rocketContainer = GetNode<Node>( "RocketContainer");
        _missileFire = GetNode<AudioStreamPlayer>("MissileFire");
    }
    public override void _PhysicsProcess(double delta)
    {
        var directionVector = new Vector2(0, 0);
        var direction = GetDirectionVector();
        Velocity = direction * Force;
        var newPos = ClampPosition();
        Position = newPos;
        MoveAndSlide();
    }

    private Vector2 GetDirectionVector()
    {
        var vertical = 0;
        var horizontal = 0;

        vertical = Input.IsActionPressed("move_up") || Input.IsActionPressed("move_down")
            ? Input.IsActionPressed("move_down") ? 1 : -1
            : 0;

        horizontal = Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right")
            ? Input.IsActionPressed("move_right") ? 1 : -1
            : 0;

        return new Vector2(horizontal, vertical);
    }

    private Vector2 ClampPosition()
    {
        var viewBox = GetViewportRect().Size;
        var newPos = Position + Velocity * (float)GetPhysicsProcessDeltaTime(); ;
        newPos.X = Mathf.Clamp(newPos.X, 0, viewBox.X);
        newPos.Y = Mathf.Clamp(newPos.Y, 0, 720);
        return newPos;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("player_fire"))
        {
            var rocketInstance = _rocketScene.Instantiate<Rocket>();
            rocketInstance.Position = Position;
            rocketInstance.Position = Position + new Vector2(85, 0); // shift up 10 pixels
            _rocketContainer.AddChild(rocketInstance);
            _missileFire.Play();
        }
    }
    public void TakeDamage()
    {
        EmitSignal(SignalName.LivesDepleted);
    }

    public void DestroyPlayer()
    {
        QueueFree();
    }
}
