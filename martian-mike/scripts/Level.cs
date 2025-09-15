using Godot;
using Godot.Collections;
using Timer = Godot.Timer;

namespace MartianMike.scripts;

public partial class Level : Node2D
{
    [Export]
    public PackedScene NextLevelScene;
    [Export] 
    public int LevelTime = 10;
    [Export]
    public bool IsFinalLevel = false;

    private int _timeLeft = 0;
    private bool _levelComplete = false;

    private Ui _ui;
    private Hud _hud;
    private Player _player;
    private Start _playerStartPosition;
    private DeathZone _deathZone;
    private Goal _goal;
    private Timer _timer  = new();
    private Array<Node> _traps;

    public override void _Ready()
    {
        SetupPlayer();
        SetupDeathZone();
        SetupTraps();
        SetupGoal();
        SetupTimer();
        SetupUi();
    }
    
    private void SetupPlayer()
    {
        _player = GetTree().GetFirstNodeInGroup("Player") as Player;
        _playerStartPosition = GetNode<Start>("Start");
        if (_player is not null)
        {
            ResetPlayerPosition();
        }
    }
    
    private void ResetPlayerPosition()
    {
        _timeLeft = LevelTime;
        _hud?.SetTimeOnTimeLabel(_timeLeft);
        _player.SetGlobalPosition(_playerStartPosition.GetSpawnPosition().GlobalPosition);
        _player.ResetPlayer();
    }
    
    private void SetupDeathZone()
    {
        _deathZone = GetNode<DeathZone>("DeathZone");
        _deathZone.BodyEntered += DeathZoneEntered;
    }
    
    private void DeathZoneEntered(Node2D body)
    {
        _player.Hurt();
        ResetPlayerPosition();
    }
    
    private void SetupTraps()
    {
        _traps = GetTree().GetNodesInGroup("traps");
        foreach (var trap in _traps)
        {
            if (trap is Trap trapInstance)
            {
                trapInstance.PlayerTouchedTrap += PlayerTouchedTrap;
            }
        }
    }

    private void PlayerTouchedTrap()
    {
        _player.Hurt();
        ResetPlayerPosition();
    }

    private void SetupGoal()
    {
        _goal = GetNode<Goal>("Goal");
        _goal.BodyEntered += PlayerEnteredGoal;
    }
    
    private void LoadNextLevel()
    {
        if (IsFinalLevel) _ui.ShowWinScreen(true);
        
        if(NextLevelScene is null) return;
        
        GetTree().ChangeSceneToPacked(NextLevelScene);
    }

    private void PlayerEnteredGoal(Node2D body)
    {
        if (body is not Player player) return;
        _levelComplete = true;
        player.PlayerActive = false;
        _goal.PlayAnimation();
        GetTree().CreateTimer(1).Timeout += LoadNextLevel;
    }

    private void SetupTimer()
    {
        _timeLeft = LevelTime;
        _timer.Name = "LevelTimer";
        _timer.SetWaitTime(1);
        _timer.Timeout += Timeout;
        AddChild(_timer);
        _timer.Start();
    }
    
    private void Timeout()
    {
        if(_levelComplete) return;
        
        _timeLeft -= 1;
        _hud.SetTimeOnTimeLabel(_timeLeft);
       
        if (_timeLeft >= 0 ) return;
       
        _player.Hurt();
        ResetPlayerPosition();
        _timeLeft = LevelTime;
    }
    
    private void SetupUi()
    {
        _hud = GetNode<Hud>("UI/HUD");
        _ui = GetNode<Ui>("UI");
        _hud.SetTimeOnTimeLabel(_timeLeft);
    }
   
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("quit"))
        {
            GetTree().Quit();
        }
        else if (Input.IsActionJustPressed("reset"))
        {
            GetTree().ReloadCurrentScene();
        }
    }
}
