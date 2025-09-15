using Godot;
using System;

public partial class GameOverScreen : Panel
{
    private Button _retryButton;
    private Label _scoreText;

    public override void _Ready()
    {
        _scoreText = GetNode<Label>("Score");
        _retryButton = GetNode<Button>("Retry");
        _retryButton.Pressed += ButtonPressed;
    }

    public void SetScoreText(int score)
    {
        _scoreText.Text = $"SCORE: {score}";
    }

    private void ButtonPressed()
    {
        GetTree().ReloadCurrentScene();
    }
}
