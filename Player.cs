using Godot;
using System;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.

	[Signal]
	public delegate void LoseHPEventHandler(int currentHP);
	
	[Signal]
	public delegate void AddCoinEventHandler(int coinAmount);

	[Signal]
	public delegate void AddLifeEventHandler();

	[Signal]
	public delegate void ImDeadEventHandler();

	[Signal]
	public delegate void IWinEventHandler();

	[Signal]
	public delegate void SpawnLevelEventHandler();

	[Export]
	public float up = - 600;

	public float down = 650;

	public float left = - 300;

	public float right = 300;

	public int currentHP = 1;

	public int coinAmount = 0;

	public int lifeAdd = 0;

	public int jump;

	public float fast;

	public int justDie;

	public int acceleration;

	public int plus = 50;

	private Godot.Timer speedTimer;
	

	//public int Speed {get; set;} = 350;

	

	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	public override void _Ready(){
		speedTimer = GetNode<Timer>("PlayerTimer");
		speedTimer.Start();

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 myVelocity = Velocity;

		//myVelocity.Y += Gravity*(double)delta; 
		myVelocity.Y += Gravity*(float)delta;

		
		if(Input.IsActionJustPressed("MoveUp") && IsOnFloor())
		{
			myVelocity.Y = up;				
				
		}

		if(Input.IsActionJustPressed("Movedown")){
			myVelocity.Y = down;
			//myVelocity.Y = Gravity*(float)delta;
		}

		if(Input.IsActionPressed("MoveLeft"))
		{

			if(Input.IsActionPressed("Sprint")){
					fast = -70;
				} 
				else{
					fast = 0;
				}
			myVelocity.X = left + fast;

		}	


		else if(Input.IsActionPressed("MoveRight"))
		{
				
		
			if(Input.IsActionPressed("Sprint")){
					fast = 70;
				} 
				else{
					fast = 0;
				}
				myVelocity.X = right + fast;

				
		}

		else{
			myVelocity.X = 0;
		}

		if (jump == 0)
		{
			myVelocity.Y = -300;
			jump = 1;
		}
		if (jump == 2){
			myVelocity.Y = - 600;
			jump = 1;
		}
	  

		/*
		myVelocity = Velocity;

		var myDirection = Input.GetVector("MoveLeft","MoveRight","MoveUp", "MoveDown");

		if(myDirection != Vector2.Zero){
		myVelocity = myDirection * Speed;
		}
		else{
			myVelocity = myDirection * 0;
		}

		*/

		Velocity = myVelocity;
		MoveAndSlide();

	}

		
		public void OnPlayerSpeed(){
			
		}

	public void OnCollision(Rid areaRid, Area2D area, int areaShapeIndex, int localShapeIndex)
	{
		//GD.Print(area.Name);

		if(area.Name == "PlaceHolder1"){
			GD.Print("Level Spawn");

			EmitSignal(SignalName.SpawnLevel);
			

		}
		
		if(area.Name == "Hurtbox"){
			//GD.Print("Player got killed");
			currentHP--;
			EmitSignal(SignalName.AddLife, currentHP);
			jump = 0;

			if(currentHP <= 0){
				EmitSignal(SignalName.ImDead);
			}
			
		}
		
		
		if(area.Name == "KillArea"){
			int k = 0;	
			while(k < 1){
				currentHP--;
				EmitSignal(SignalName.AddLife, currentHP);
				k++;
				if(currentHP <= 0){
					GetTree().Paused = true;
				}
			}
		}

		if(currentHP <= 0){
			GetTree().Paused = true;
		}

	}

	/*
		public void OnCollision(Rid areaRid, Area2D area, int areaShapeIndex, int localShapeIndex)
	{   
		
		if(area.Name == "HitBox"){

			GD.Print("Jump");
		}
	}
   */

	

	
/*
	public void OnSideCollision(Rid bodyRid, Node2D area, int bodyShapeIndex, int localShapeIndex){

		if(area.Name == "HitBox"){
		
			//GD.Print("dö");
		}

		//area.GetParent().QueueFree();

	}
*/
	public void OnCoinEntered(Area2D area){
		if(area.Name == "CoinArea"){
			coinAmount++;
			EmitSignal(SignalName.AddCoin, coinAmount);
			lifeAdd++;

			speedTimer.Start(3);
			//GD.Print("wait");
		}
		if(area.Name == "BigCoinArea"){
			coinAmount = coinAmount + 10;
			lifeAdd = lifeAdd + 10;
			EmitSignal(SignalName.AddCoin, coinAmount);
			
		}
		if(area.Name == "1upArea"){
			//currentHP++;
			coinAmount = coinAmount + 5;
			lifeAdd = lifeAdd + 5;
			EmitSignal(SignalName.AddCoin, coinAmount);
		}

		if(lifeAdd > 20 || lifeAdd == 20){
			lifeAdd = 0;
			currentHP++;
			//GD.Print("1UP");
			EmitSignal(SignalName.AddLife, currentHP);
		}
		if (area.Name == "GoalArea"){
			GetTree().Paused = true;
			EmitSignal(SignalName.IWin);
		}
		if(area.Name == "HitBox"){
			jump = 0;
			if(Input.IsActionPressed("MoveUp")){
				jump = 2;
			}
		}

		if(area.Name == "OffLimitsArea"){
			currentHP = 0;
			EmitSignal(SignalName.AddLife, currentHP);
			//GD.Print("OffLimits");
			if(currentHP <= 0){
				EmitSignal(SignalName.ImDead);
			}
		}


		

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		
	}
}

