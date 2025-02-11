using Godot;
using System;

public partial class Main : Node
{
	private int forgingScore = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		forgingScore = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void GetForgingScore(){
		forgingScore++;
	}
	
}
