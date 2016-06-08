using System;

namespace StalkerBot
{
	public class Heuristics
	{

		private readonly Node _goalNode;

		public Heuristics(Node goalNode)
		{
			_goalNode = goalNode;

		}

		// Generates an estimated cost to reach the goal
		// from the given node
		public float Estimate(Node node)
		{
			var dx = Math.Abs(node.X - _goalNode.X);
			var dy = Math.Abs(node.Y - _goalNode.Y);
			return 50*(dx + dy);
		}
	}
}
