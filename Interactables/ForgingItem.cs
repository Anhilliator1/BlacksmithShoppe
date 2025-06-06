using Godot;
using System;

public partial class ForgingItem : RigidBody3D
{
	[Signal]
	public delegate void ScoreUpdateEventHandler(int score);
	
	private MeshInstance3D mesh;  // Reference to the mesh
	private CollisionShape3D collider;
	private StandardMaterial3D material;  // Material to modify
	
	[Export]
	public int ForgeScore { get; set; } = 0;  // Each section's score

	private bool clicked = false;
	private bool mouseHover = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
		collider = GetNode<CollisionShape3D>("CollisionShape3D");

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

		// Add this ForgingItem to the "ForgingSections" group for global tracking
	}

	// Called when selected (normal click)
	public void Selected()
	{
		// Highlight selected section and increase score
		material.AlbedoColor = new Color(0.0f, 1.0f, 0.0f);  // Change to green
		clicked = true;
		ForgeScore += 5;  // Increase score for this section
		EmitSignal(SignalName.ScoreUpdate, ForgeScore); // Emit the updated score

		// Deactivate glow and highlight
		Highlighted(mouseHover);
	}

	// Called when alternate selected (alt-click)
	public void AltSelected()
	{
		// Highlight alternate selection and decrease score
		material.AlbedoColor = new Color(0.0f, 0.0f, 1.0f);  // Change to blue
		clicked = true;
		ForgeScore -= 5;  // Decrease score for this section
		EmitSignal(SignalName.ScoreUpdate, ForgeScore);  // Emit the updated score

		// Deactivate glow and highlight
		UpdateScaleFromScore(); 
		Highlighted(mouseHover);
	}

	// Called when un-clicked
	public void UnClick()
	{
		// Reset selection and color
		material.AlbedoColor = new Color(1.0f, 1.0f, 1.0f);  // Change back to white
		clicked = false;

		// Deactivate glow and highlight
		UpdateScaleFromScore(); 
		Highlighted(mouseHover);
	}

	// Called when mouse hovers over the object
	public void Highlighted(bool mouseHover)
	{
		this.mouseHover = mouseHover;
		if (mouseHover && !clicked)
		{
			// Activate glow effect when mouse hovers and the item isn't clicked
			material.EmissionEnabled = true;
			material.Emission = new Color(1.0f, 1.0f, 0.0f);  // Yellow glow
		}
		else
		{
			// Deactivate glow if clicked or mouse is not hovering
			material.EmissionEnabled = false;
		}
	}
	
		private void UpdateScaleFromScore()
		{
		float scaleFactor = Mathf.Clamp(1.0f + ForgeScore * 0.05f, 0.5f, 3.0f);
		Vector3 scaleVec = new Vector3(scaleFactor, scaleFactor, scaleFactor);

		// Scale ONLY the mesh, not the whole RigidBody
		mesh.Scale = scaleVec;

		// Update collision shape size independently
		if (collider.Shape is BoxShape3D boxShape)
		{
			boxShape.Size = scaleVec;  // Approximate scaling
		}
		else if (collider.Shape is SphereShape3D sphereShape)
		{
			sphereShape.Radius = scaleFactor * 0.5f;
		}
		else if (collider.Shape is CapsuleShape3D capsuleShape)
		{
			capsuleShape.Radius = scaleFactor * 0.3f;
			capsuleShape.Height = scaleFactor;
		}
	}
}
