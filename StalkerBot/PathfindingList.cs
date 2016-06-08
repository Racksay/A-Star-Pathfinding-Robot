using System.Collections.Generic;
using System.Linq;

namespace StalkerBot
{
	public class PathfindingList
	{

		private readonly List<Node> _queue = new List<Node>();

		public PathfindingList(Node start)
		{
			_queue.Add(start);
		}

		public PathfindingList()
		{ 
		}

		public void Add(Node startNode)
		{
			_queue.Add(startNode);
		}

		public int Lenght()
		{
			return _queue.Count;
		}

		public Node SmallestElement()
		{	
			return _queue.Min();
		}

		public bool Contains(Node endNode)
		{
			return _queue.Contains(endNode);
		}

		public Node Find(Node endNode)
		{
			foreach (var node in _queue)
			{
				if(node == endNode)
					return node;
			}

			// Should not happen
			return new Node();
		}

		public void Remove(Node endNodeRecord)
		{
			_queue.Remove(endNodeRecord);
		}
	}
}
