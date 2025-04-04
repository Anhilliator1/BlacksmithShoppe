using Godot;
using System;

public partial class Main : Node
{
	private int totalForgeScore = 0;
	private Label forgeScoreLabel;

	public override void _Ready()
{
	Node userInterface = GetNode("UserInterface");
	forgeScoreLabel = userInterface.GetNode<Label>("Label");

	ForgingSections forgingSections = GetNode<ForgingSections>("ForgingSections");
	forgingSections.Connect("TotalScoreUpdated", new Callable(this, nameof(UpdateForgeScore)));
}	
public void UpdateForgeScore(int totalScore)
{
	forgeScoreLabel.Text = $"Forge Score: {totalScore}";
}	

	// This will update whenever a ForgingItem emits a ScoreUpdate signal
	public void OnForgingItemScoreUpdate(int sectionScore)
	{
		// Recalculate the total score
		totalForgeScore = 0;

		// Sum all scores from all ForgingItem instances
		foreach (ForgingItem item in GetTree().GetNodesInGroup("ForgingSections"))
		{
			totalForgeScore += item.ForgeScore;
		}

		// Update the Label with the new total score
		forgeScoreLabel.UpdateForgeScore(totalForgeScore);
	}
}
