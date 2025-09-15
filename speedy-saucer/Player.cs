using Godot;

namespace SpeedySaucer;

public partial class Player : RigidBody2D
{
    [Export]
    public int Force = 500;

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("move_up"))
        {
            ApplyForce(new Vector2( 0f, -Force));    
        }
        if (Input.IsActionPressed("move_down"))
        {
            ApplyForce(new Vector2(0,Force));    
        }
        if (Input.IsActionPressed("move_left"))
        {
            ApplyForce(new Vector2(-Force,0));    
        }
        if(Input.IsActionPressed("move_right"))
        {
            ApplyForce(new Vector2(Force,0));    
        }
        
    }
}
