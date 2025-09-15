using Godot;

namespace AlienAttack.scripts;

public partial class Game : Node2D
{
    [Export] 
    public int Lives = 3;
    
    private int _score = 0;
    
    private Player _player;
    private EnemySpawner _enemySpawner;
    private Hud _hud;
    private Area2D _deathZone;
    private CanvasLayer _ui;
    private AudioStreamPlayer _enemyHit;
    private AudioStreamPlayer _playerHit;
    
    public override void _Ready()
    {
        _player = GetNode<Player>("Player");
        _player.LivesDepleted += ManageLives;
        _deathZone = GetNode<Area2D>("DeathZone");
        _deathZone.AreaEntered += DestroyObject;
        _enemySpawner = GetNode<EnemySpawner>("EnemySpawner");
        _enemySpawner.OnEnemySpawned += SpawnedEnemy;
        _enemySpawner.OnPathEnemySpawned += SpawnedPathEnemy;
        _hud = GetNode<Hud>("UI/Hud");
        _hud.SetScoreLabel(0);
        _hud.SetLivesLabel(3);
        _ui = GetNode<CanvasLayer>("UI");
        _enemyHit = GetNode<AudioStreamPlayer>("EnemyHitSound");
        _playerHit = GetNode<AudioStreamPlayer>("PlayerHitSound");
    }

    private void SpawnedPathEnemy(PathEnemy pathEnemyInstance)
    {
        AddChild(pathEnemyInstance);
        pathEnemyInstance.Enemy.EnemyDestroyed += EnemyDestroyed;   
    }

    private void SpawnedEnemy(Enemy enemyInstance)
    { 
        enemyInstance.EnemyDestroyed += EnemyDestroyed;
        AddChild(enemyInstance);
    }

    private void EnemyDestroyed(int points)
    {
        _score =  _score + points;
        _hud.SetScoreLabel(_score);
        _enemyHit.Play();
    }
   
    private void ManageLives()
    { 
        _playerHit.Play();
        if (Lives != 0)
        {
            _hud.SetLivesLabel(--Lives);
            return;
        }
        
        _player.DestroyPlayer();

        GetTree().CreateTimer(1).Timeout += DisplayGameOver;
        
    }

    private void DisplayGameOver()
    {
        var gameOverScreenScene = GD.Load<PackedScene>("res://Scenes/game_over_screen.tscn");
        var gameOverScreen =  gameOverScreenScene.Instantiate<GameOverScreen>();
        _ui.AddChild(gameOverScreen);
        gameOverScreen.SetScoreText(_score);
    }

    private void DestroyObject(Area2D area)
    {
        var enemy = area as Enemy;
        enemy.QueueFree();
    }
}
