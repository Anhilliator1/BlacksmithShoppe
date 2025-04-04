using Godot;
using System;
using System.Collections.Generic;

public partial class ForgingSections : Node3D
{
	[Signal]
	public delegate void TotalScoreUpdatedEventHandler(int totalScore);

	private int totalForgeScore = 0;
	private List<ForgingItem> forgingItems = new List<ForgingItem>();

	public override void _Ready()
	{
		// Get all ForgingItem children
		foreach (ForgingItem item in GetChildren())
		{
			forgingItems.Add(item);
			//item.Connect("ScoreUpdate", new Callable(this, nameof(OnForgingItemScoreUpdate)));
		}
	}

	private void OnForgingItemScoreUpdate(int sectionScore)
	{
		// Recalculate the total score incrementally
		totalForgeScore = 0;
		foreach (ForgingItem item in forgingItems)
		{
			totalForgeScore += item.ForgeScore;
		}

		// Emit updated score to Main or UI
		EmitSignal(SignalName.TotalScoreUpdated, totalForgeScore);
	}
}
