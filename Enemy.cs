using Godot;
using System;
using System.Security.Cryptography.X509Certificates;


public partial class Enemy : CharacterBody2D
{
	private int speed = -200;
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	[Signal]
	public delegate void HitBoxCollisionEventHandler();

	Random rnd = new Random();

	private State currentState;

		enum State{
		Go_left,
		Go_Right
	}


	// Called when the node enters the scene tree for the first time.

	public override void _PhysicsProcess(double delta)
	{
		
		Vector2 myVelocity = Velocity;
		myVelocity.Y += Gravity*(float)delta; 

		myVelocity.X = speed;

		


		Velocity = myVelocity;
		MoveAndSlide();
	}
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	

	public void OnAreaShapeEntered(Rid areaRid, Area2D area, int areaShapeIndex, int localShapeIndex)
	{
		if(area.Name == "Area2DRight" || area.Name == "Area2DLeft" || area.Name == "Invisible")
		{
			speed = speed * -1;
		}
		
	}


	public void OnBodyEntered(Node2D body){

			

			if(body.Name == "Player"){
				speed = speed;
			}else{
				speed = speed * -1;
			}

		}


	public void OnHurtBoxAreaEntered(Area2D area){
	

	}

	public void OnPlayerEnteredRightSide(Area2D area){

		if(speed == 200 && area.Name == "PlayerArea"){
			speed = speed * -1;
		
		}
	}

	public void OnPlayerEnteredLeftSide(Area2D area){
		if(area.Name == "PlayerArea" && speed == -200){
			speed = speed * -1;

		}
	}




	public void OnHitBoxAreaEntered(Area2D area){
		

		if(area.Name == "PlayerArea"){
		
			//GD.Print("Enemy got stumped on");
			QueueFree();
		}
	

	}
}
