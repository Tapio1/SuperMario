using Godot;
using System;



//Level LevelOne;

public partial class Main : Node2D
{
	
	Node2D mainMenu;

	Node2D Level;

	public int showOnOff = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mainMenu = GetNode<Node2D>("MainMenu");
		

		Input.MouseMode = Input.MouseModeEnum.Hidden;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("OpenMenu")){
			showOnOff = showOnOff * -1;

			if(showOnOff == -1){
				GetNode<Button>("UI/QuitButton").Visible = true;
				Input.MouseMode = Input.MouseModeEnum.Visible;
			}

			if (showOnOff == 1){
			GetNode<Button>("UI/QuitButton").Visible = false;
			Input.MouseMode = Input.MouseModeEnum.Hidden;
			}
		
		}
	}

	public void OnQuitButtonPressed(){
		GetTree().Quit();
	}

	public void OnSpawnLevel(){
		mainMenu.QueueFree();
		GD.Print("1-1");

		var nextLevel = GD.Load<PackedScene>("res://level_1_1.tscn").Instantiate<Node2D>();
		AddChild(nextLevel);
	}
}
