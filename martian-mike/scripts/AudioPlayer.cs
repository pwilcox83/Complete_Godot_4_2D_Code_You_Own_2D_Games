using System.Threading.Tasks;
using Godot;

namespace MartianMike.scripts;

public partial class AudioPlayer : Node
{
    private AudioStream _hurt;
    private AudioStream _jump;

    public override void _Ready()
    {
        _hurt = GD.Load<AudioStream>("res://assets/audio/hurt.wav");
        _jump = GD.Load<AudioStream>("res://assets/audio/jump.wav");
    }
    public async Task PlaySfx(string sfxName)
    {
        var asp = new AudioStreamPlayer();
        switch (sfxName)
        {
            case "hurt":
                asp.Stream = _hurt;
                break;
            case "jump":
                asp.Stream = _jump;
                break;
            default:
                GD.Print("sfx not found");
                break;
        }

        if (asp.Stream == null) return;
        
        AddChild(asp);
        asp.Play();
        //asp.Finished += asp.QueueFree;
        await ToSignal(asp, AudioStreamPlayer.SignalName.Finished);
        asp.QueueFree();


    }
}
