using Godot;
using System;

public partial class Label : Godot.Label
{
	private int _score = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Connect the ScoreUpdate signal for each ForgingItem
		foreach (ForgingItem item in GetTree().GetNodesInGroup("ForgingSections"))
		{
			item.Connect("ScoreUpdate", new Callable(this, nameof(UpdateForgeScore)));
		}
	}

	// Method to update the forge score displayed on the label
	public void UpdateForgeScore(int score)
	{
		// Update the internal score
		_score = score;

		// Update the text to display the new score
		Text = $"Forge Score: {_score}";
	}
}
