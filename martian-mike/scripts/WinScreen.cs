using System;
using Godot;

namespace MartianMike.scripts;

public partial class WinScreen : Control
{
	private Button _button;
	
	public override void _Ready()
	{
		_button = GetNode<Button>("Button");
		_button.Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/start_menu.tscn");
	}
}
