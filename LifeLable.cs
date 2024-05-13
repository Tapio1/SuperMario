using Godot;
using System;

public partial class LifeLable : Label
{
		public void OnAddLife(int currentHP){
		
		Text = "Lives: " + currentHP;
		

	}


}
