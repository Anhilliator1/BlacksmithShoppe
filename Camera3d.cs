using Godot;
using System;

public partial class Camera3d : Camera3D
{
	[Export]
	public bool mouseOn = false;
	public bool mouseClicked = false;
	
	[Signal]
	public delegate void UnclickEventHandler();
	
	[Signal]
	public delegate void SelectEventHandler();
	
	[Signal]
	public delegate void AltSelectEventHandler();
	
	[Signal]
	public delegate void MouseHoverEventHandler(bool mouseHover);
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		
	}
	
	
	public void _MouseOn(){
		//GD.Print("Mouse On");
		mouseOn = true;
		EmitSignal(SignalName.MouseHover, mouseOn);
	}
	
	public void _MouseOff(){
		//GD.Print("Mouse Off");
		mouseOn = false;
		EmitSignal(SignalName.MouseHover, mouseOn);
	}
	
	public override void _Input(InputEvent @event){
		if (Input.IsActionJustPressed("mouseClick") && mouseOn && !mouseClicked){
			//GD.Print("Block Clicked");
			EmitSignal(SignalName.Select);
		} else if (Input.IsActionJustPressed("altClick") && mouseOn && !mouseClicked){
			//GD.Print("Block Alt-Clicked");
			EmitSignal(SignalName.AltSelect);
		} else {
			EmitSignal(SignalName.Unclick);
		}
	}
	
	
}
