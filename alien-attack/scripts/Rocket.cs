using Godot;

namespace AlienAttack.scripts;

public partial class Rocket : Area2D
{
    [Export]
    public int Speed = 150;
    private VisibleOnScreenNotifier2D _visibleOnScreenNotifier;
    private Area2D _rocketArea;
   
    public override void _Ready()
    {
        _visibleOnScreenNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
        _visibleOnScreenNotifier.ScreenExited += ScreenExited;
        AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        QueueFree();
        if (area is not Enemy enemy) return;
        enemy.Destroyed();


    }

    private void ScreenExited()
    {
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += new Vector2(Speed * (float)delta, 0);
    }
}
