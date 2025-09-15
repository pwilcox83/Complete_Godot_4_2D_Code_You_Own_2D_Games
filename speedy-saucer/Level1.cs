using Godot;

namespace SpeedySaucer;

public partial class Level1 : Node2D
{
    private bool _isReloading = false;
    public override void _Ready()
    {
        GetNode<Area2D>("Maze").BodyExited += OnMazeBodyExited;
        GetNode<Area2D>("Maze").BodyEntered += OnMazeBodyShapeEntered;
    }
    private void OnMazeBodyExited(Node2D body)
    {
        GD.Print($"Exited: {body.Name}");
        if (_isReloading) return; // prevent multiple reloads
        {
            _isReloading = true;
        }
        CallDeferred(nameof(ReloadScene));
    }
    private void OnMazeBodyShapeEntered(Node2D body)
    {
        GD.Print($"Entered: {body.Name}");
    }

    private void ReloadScene()
    {
        var tree = GetTree();
        if (tree == null)
        {
            return;
        }
        GetTree().ReloadCurrentScene();
        _isReloading = false; // reset
    }
}
