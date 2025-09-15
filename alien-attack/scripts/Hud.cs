using Godot;

namespace AlienAttack.scripts;

public partial class Hud : Control
{
    private int _score;
    private int _lives;
    private Label _scoreLabel;
    private Label _livesCountLabel;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("ScoreLabel");
        _livesCountLabel = GetNode<Label>("LivesCountLabel");
    }
    
    public void SetScoreLabel(int value)
    {
        _score = value;
        _scoreLabel.Text = $"Score: {_score}";
    }
    
    public void SetLivesLabel(int value)
    {
        _lives = value;
        _livesCountLabel.Text = $"{value}";
    }
}
