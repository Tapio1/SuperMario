using Godot;
using System;

public partial class Block : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnPlayerEntered(Area2D area){
		if(area.Name == "PlayerArea"){
			QueueFree();
		}
	}
}
