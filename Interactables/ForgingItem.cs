using Godot;
using System;

public partial class ForgingItem : RigidBody3D
{
	private MeshInstance3D mesh;  // Reference to the mesh
	private StandardMaterial3D material;  // Material to modify
	private bool Clicked = false;
	private bool mouseHover;
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
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _PhysicsProcess(double delta){
		
	}
	
	private void Selected(){
		//GD.Print("Block Selected");
		material.AlbedoColor = new Color(0.0f, 1.0f, 0.0f);
		Clicked = true;
		Highlighted(mouseHover);
	}
	
	private void AltSelected(){
		//GD.Print("Block Alt-Selected");
		material.AlbedoColor = new Color(0.0f, 0.0f, 1.0f);
		Clicked = true;
		Highlighted(mouseHover);
	}
	
	private void UnClick(){
		material.AlbedoColor = new Color(1.0f, 1.0f, 1.0f);
		Clicked = false;
		Highlighted(mouseHover);
	}
	
	private void Highlighted(bool mouseHover){
		this.mouseHover = mouseHover;
		if(mouseHover && !Clicked){
			material.EmissionEnabled = true;
			material.Emission = new Color(1.0f, 1.0f, 0.0f);
		} else {
			material.EmissionEnabled = false;
		}
	}
	
}
