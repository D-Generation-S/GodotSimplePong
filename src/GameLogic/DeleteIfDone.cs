using Godot;
using System;

public partial class DeleteIfDone : Node2D
{
	[Export]
	private Node nodeToDelete;

	protected bool deleted;

	public void Delete()
	{
		deleted = true;
		nodeToDelete?.QueueFree();
	}
}
