using Godot;
using System;

public partial class Death : RichTextLabel
{
	// Called when the node enters the scene tree for the first time.
	public override void _Process(double delta){
		if(Input.IsActionJustPressed("Respawn")){
			GD.Print("Respawn");
			var tree = GetTree();
			tree.ReloadCurrentScene();
			tree.Paused = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
		public void OnPlayerDied(){
		GD.Print("I died");
		Show();
	}
}
