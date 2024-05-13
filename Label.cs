using Godot;
using System;

public partial class Label : Godot.Label
{

	public void OnAddCoin(int coinAmount){
		//GD.Print(coinAmount);
		Text = "Coins: " + coinAmount;
	
		

	}


}
