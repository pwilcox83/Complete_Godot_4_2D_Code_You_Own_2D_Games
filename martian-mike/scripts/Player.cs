using System.Threading.Tasks;
using Godot;

namespace MartianMike.scripts;

public partial class Player : CharacterBody2D
{
    [Export]
    public int Gravity = 400; 
    [Export]
    public int TerminalSpeed =1500;
    [Export]
    public int Speed = 100;
    [Export]
    public int JumpForce = 250;
    
    private float _direction = 250;
    public bool PlayerActive = true;
    private AnimatedSprite2D _playerAnimatedSprite;
    private static AudioPlayer _audioPlayer;
    
    public override void _Ready()
    {
        _playerAnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _audioPlayer = GetNode<AudioPlayer>("/root/AudioPlayer");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!PlayerActive) return;
        
        if (!IsOnFloor())
        {
            Velocity = Velocity with { Y = Velocity.Y + Gravity * (float)delta };
            if (Velocity.Y > TerminalSpeed)
            {
                Velocity = Velocity with { Y = 500 };

            }
        }

        if (IsOnFloor() && Input.IsActionJustPressed("jump"))
        {
            Jump(JumpForce);
        }

        _direction = Input.GetAxis("move_left", "move_right");

        if (_direction != 0)
        {
            _playerAnimatedSprite.FlipH = _direction < 0;
        }

        Velocity = Velocity with { X = _direction * Speed };

        MoveAndSlide();

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        if (IsOnFloor())
        {
            _playerAnimatedSprite.Play(_direction == 0 ? "idle" : "run");
        }
        else
        {
            _playerAnimatedSprite.Play(Velocity.Y < 0 ? "jump" : "fall");
        }
    }

    public void Jump(int force)
    {
        _audioPlayer.PlaySfx("jump");
        Velocity = Velocity with { Y = -force };
    }

    public void ResetPlayer()
    {
        _playerAnimatedSprite.Play("idle");
        Velocity = Vector2.Zero;
        _playerAnimatedSprite.FlipH = false;
    }

    public void Hurt()
    {
        _audioPlayer.PlaySfx("hurt");
    }
}
