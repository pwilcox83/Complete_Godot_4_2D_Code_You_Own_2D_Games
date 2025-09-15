using Godot;

namespace AlienAttack.scripts;

public partial class PathEnemy : Path2D
{
    
    private PathFollow2D _pathFollow;
    public Enemy Enemy;

    public override void _Ready()
    {
        _pathFollow = GetNode<PathFollow2D>("PathFollow2D");
        Enemy = GetNode<Enemy>("PathFollow2D/Enemy");

        _pathFollow.SetProgressRatio(1);
    }

    public override void _Process(double delta)
    {
        _pathFollow.ProgressRatio -= 0.25f * (float)delta;
        if (_pathFollow.ProgressRatio <= 0.1)
        {
            QueueFree();
        }
    }
}
