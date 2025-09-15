using Godot;

namespace MartianMike.scripts;

public partial class Goal : Area2D
{
    private AnimatedSprite2D _animatedSprite;
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public void PlayAnimation()
    {
        _animatedSprite.Play("default");   
    }
}
