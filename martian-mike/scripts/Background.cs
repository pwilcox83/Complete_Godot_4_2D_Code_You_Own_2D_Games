using Godot;

namespace MartianMike.scripts;

public partial class Background : ParallaxBackground
{
    [Export]
    public int ScrollSpeed = 20;
    [Export]
    public CompressedTexture2D BackgroundTexture;
    private Sprite2D _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("ParallaxLayer/Sprite2D");
        if (BackgroundTexture is null)
        {
            BackgroundTexture = (CompressedTexture2D)GD.Load("res://assets/textures/bg/Blue.png");    
        }
        _sprite.Texture = BackgroundTexture;
    }

    public override void _Process(double delta)
    {
        var regionRect = _sprite.RegionRect;
        regionRect.Position += (new Vector2(ScrollSpeed * (float)delta, ScrollSpeed * (float)delta));
        if (regionRect.Position >= new Vector2(64, 64))
        {
            regionRect.Position = new Vector2();
        }
        _sprite.RegionRect = regionRect;
    }
}
