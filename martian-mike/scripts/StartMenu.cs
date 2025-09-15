using Godot;

namespace MartianMike.scripts;

public partial class StartMenu : Node2D
{
    [Export] 
    public PackedScene FirstLevel;

    private Button _startButton;
    private Button _quitButton;

    public override void _Ready()
    {
        _startButton = GetNode<Button>("Control/Start");
        _startButton.Pressed += StartGame;
        
        _quitButton = GetNode<Button>("Control/Quit");
        _quitButton.Pressed += QuitGame;

    }

    private void StartGame()
    {
        GetTree().ChangeSceneToPacked(FirstLevel);
    }

    private void QuitGame()
    {
        GetTree().Quit();
    }
}
