using System;
using Godot;

namespace MartianMike.scripts;

public partial class Start : StaticBody2D
{
    private Marker2D _spawnPosition;
    
    public override void _Ready()
    {
        _spawnPosition = GetNode<Marker2D>("SpawnPosition");
    }

    public Marker2D GetSpawnPosition()
    {
        return _spawnPosition ?? throw new InvalidOperationException("SpawnPosition is not initialized yet. Access after _Ready.");
    }
}
