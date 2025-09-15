using Godot;

namespace MartianMike.scripts;

public partial class JumpPad : Area2D
{
    [Export] public int Force = 300;
    
    private AnimatedSprite2D _animatedSprite;
    
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        BodyEntered += PlayAnimation;
    }

    private void PlayAnimation(Node2D body)
    {
        if (body is not Player player) return;
        
        _animatedSprite.Play("jump");
        player.Jump(Force);
    }
}
