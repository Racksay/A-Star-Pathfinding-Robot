using System;
using System.Collections.Generic;

namespace StalkerBot
{
	public class Node : IComparable<Node>
	{
		public int X { get; set; }
		public int Y { get; set; }
		public float CostSoFar { get; set; }
		public float EstimatedTotalCost { get; set; }
		public Connection<Node> Connection { get; set; }

		public List<Connection<Node>> Connections { get; set; } = new List<Connection<Node>>();

		public Node(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Node()
		{
		}

		public int CompareTo(Node other)
		{
			return (int) (CostSoFar - other.CostSoFar);
		}
	}
}
