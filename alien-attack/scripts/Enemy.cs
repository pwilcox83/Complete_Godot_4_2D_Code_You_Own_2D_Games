using Godot;

namespace AlienAttack.scripts;

public partial class Enemy : Area2D
{
    [Export]
    public int Speed = 200;
    private int _points = 100;
    [Signal]
    public delegate void EnemyDestroyedEventHandler(int points);
        
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += new Vector2(-Speed * (float)delta, 0);
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is not Player player) return;
        player.TakeDamage();
        CallDeferred(nameof(Destroyed));
    }
    
    public void Destroyed()
    {
        CallDeferred(nameof(EmitDestroyedSignal));
        QueueFree();
    }
    
    public void EmitDestroyedSignal()
    {
        EmitSignal(SignalName.EnemyDestroyed, _points);
    }
}
