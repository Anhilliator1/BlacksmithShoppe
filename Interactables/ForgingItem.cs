using Godot;
using System;

[Signal]
public delegate void GetForgingScoreEventHandler();

public partial class ForgingItem : RigidBody3D
{
	private MeshInstance3D mesh;  // Reference to the mesh
	
	private StandardMaterial3D material;  // Material to modify
	
	private bool Clicked = false;
	
	private bool mouseHover;
	
	[Export]
	public int ForgeScore;
	
	[Signal]
	public delegate void ScoreUpdateEventHandler(int Score);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");

		// Ensure it has a material override, or create a new one
		if (mesh.MaterialOverride == null)
		{
			material = new StandardMaterial3D();
			mesh.MaterialOverride = material;
		}
		else
		{
			material = (StandardMaterial3D)mesh.MaterialOverride;
		}
		material.EmissionEnabled = false;
		
		ForgeScore = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _PhysicsProcess(double delta){
		
	}
	
	public void Selected(){
		//GD.Print("Block Selected");
		material.AlbedoColor = new Color(0.0f, 1.0f, 0.0f);
		Clicked = true;
		ForgeScore += 5;
		EmitSignal(SignalName.ScoreUpdate, ForgeScore);
		Highlighted(mouseHover);
	}
	
	public void AltSelected(){
		//GD.Print("Block Alt-Selected");
		material.AlbedoColor = new Color(0.0f, 0.0f, 1.0f);
		Clicked = true;
		ForgeScore -= 5;
		EmitSignal(SignalName.ScoreUpdate, ForgeScore);
		Highlighted(mouseHover);
	}
	
	public void UnClick(){
		material.AlbedoColor = new Color(1.0f, 1.0f, 1.0f);
		Clicked = false;
		Highlighted(mouseHover);
	}
	
	public void Highlighted(bool mouseHover){
		this.mouseHover = mouseHover;
		if(mouseHover && !Clicked){
			material.EmissionEnabled = true;
			material.Emission = new Color(1.0f, 1.0f, 0.0f);
		} else {
			material.EmissionEnabled = false;
		}
	}
	
}
