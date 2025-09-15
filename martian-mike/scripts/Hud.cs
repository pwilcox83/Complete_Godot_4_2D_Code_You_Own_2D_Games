using Godot;

namespace MartianMike.scripts;

public partial class Hud : Control
{
    private Label _timeLabel;
    
    public override void _Ready()
    {
        _timeLabel = GetNode<Label>("TimeLabel");
        SetTimeOnTimeLabel(9999);
    }
    
    public void SetTimeOnTimeLabel(int time)
    {
        _timeLabel.Text = $"TIME: {time}";
    }
}
