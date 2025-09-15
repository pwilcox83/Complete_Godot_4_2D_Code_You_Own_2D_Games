using AlienAttack.scripts;
using Godot;

public partial class EnemySpawner : Node2D
{
    private Timer _timer;
    private Timer _timerPath;
    private PackedScene _enemyScene;
    private PackedScene _pathEnemyScene;
    private Node2D _spawnPoints;
    
    [Signal]
    public delegate void OnEnemySpawnedEventHandler(Enemy enemyInstance);
    [Signal]
    public delegate void OnPathEnemySpawnedEventHandler(PathEnemy pathEnemyInstance);
    public override void _Ready()
    {
        _enemyScene = GD.Load<PackedScene>("res://scenes/Enemy.tscn");
        _pathEnemyScene = GD.Load<PackedScene>("res://scenes/path_enemy.tscn");
        _spawnPoints = GetNode<Node2D>("SpawnPositions");
        
        _timer = GetNode<Timer>("Timer");
        _timerPath = GetNode<Timer>("Timer_Path");
        _timer.Timeout += TimeOutEnemyAction;
        _timerPath.Timeout += TimeOutEnemyPathAction;
    }

    private void TimeOutEnemyAction()
    {
        SpawnEnemy();
    }
    
    private void TimeOutEnemyPathAction()
    {
        SpawnPathEnemy();
    }

    private void SpawnEnemy()
    {
        var randomMarker =_spawnPoints.GetChildren().PickRandom() as Marker2D;
        var enemyInstance = _enemyScene.Instantiate<Enemy>(); 
        EmitSignal(SignalName.OnEnemySpawned, enemyInstance);
        enemyInstance.GlobalPosition = new Vector2( randomMarker.GlobalPosition.X , randomMarker.GlobalPosition.Y);
    }

    private void SpawnPathEnemy()
    {
        var pathEnemyInstance = _pathEnemyScene.Instantiate<PathEnemy>();
        EmitSignal(SignalName.OnPathEnemySpawned, pathEnemyInstance);
    }
}
