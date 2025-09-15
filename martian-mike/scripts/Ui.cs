using Godot;

namespace MartianMike.scripts;

public partial class Ui : CanvasLayer
{
    private WinScreen _winScreen;

    public override void _Ready()
    {
        _winScreen = GetNode<WinScreen>("WinScreen");
    }
    
    public void ShowWinScreen(bool visible)
    {
        _winScreen.Visible = visible;
    }
}
