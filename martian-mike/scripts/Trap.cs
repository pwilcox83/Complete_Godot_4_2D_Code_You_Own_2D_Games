using Godot;

namespace MartianMike.scripts;

public partial class Trap : Node2D
{
    [Signal]
    public delegate void PlayerTouchedTrapEventHandler();
    
    private Area2D _area2d;
    
    public override void _Ready()
    {
        _area2d = GetNode<Area2D>("Area2D");
        _area2d.BodyEntered += TrapOnEntered;
    }

    private void TrapOnEntered(Node2D body)
    {
        if (body is not Player player) return;
        
        EmitSignal(SignalName.PlayerTouchedTrap);
    }
}
