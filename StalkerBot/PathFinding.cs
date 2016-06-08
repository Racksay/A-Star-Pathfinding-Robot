using System;
using System.Collections.Generic;
using System.Net.Security;

namespace StalkerBot
{
	public class PathFinding
	{
		public List<Connection<Node>> AStar(Graph graph, Node start, Node goal, Heuristics heuristics)
		{
			// Information needed to keep track of each node 
			// &
			// Initialize for the start node
			var startRecord = start;
			startRecord.Connection = null;
			startRecord.CostSoFar = 0;
			startRecord.EstimatedTotalCost = heuristics.Estimate(start);
			Console.WriteLine(startRecord.EstimatedTotalCost);

			// Initialize the open and closed list
			var open = new PathfindingList(startRecord);
			var closed = new PathfindingList();
			var currentNode = open.SmallestElement();


			while (open.Lenght() > 0)
			{
				// Find the smallest element in the open list
				currentNode = open.SmallestElement();
				
				// If it is the goal node, then terminate
				if (currentNode == goal)
				{
					break;
				}

				// Otherwise get its outgoing connections
				// loop through each connection in turn
				foreach (var connection in currentNode.Connections)
				{
					// Get the cost estimate for the end node
					var endNode = connection.GetToNode();
					var endNodeCost = currentNode.CostSoFar + connection.GetCost();

					Node endNodeRecord;
					float endNodeHeuristic;

					// if the node is closed we may have to skip or
					// remove it from the closed list
					if (closed.Contains(endNode))
					{
						// Here we find the record in the closed list
						// corresponding to the endNode.
						endNodeRecord = closed.Find(endNode);

						// if we didn't find a shorter route skip
						if (endNodeRecord.CostSoFar <= endNodeCost) continue;

						// Oterwise remove it from the closed list
						closed.Remove(endNodeRecord);

						endNodeHeuristic = heuristics.Estimate(endNode);

					}

					// skip if the node is open and we've not
					// found  a better route
					else if (open.Contains(endNode))
					{
						// Here we find the record in the open list
						// corresponding to the endNode
						endNodeRecord = open.Find(endNode);

						// If our route is no better, then skip
						if (endNodeRecord.CostSoFar <= endNodeCost) continue;

						endNodeHeuristic = heuristics.Estimate(endNode);

					}
					// Otherwise we know we’ve got an unvisited
					// node, so make a record for it
					else
					{
						endNodeRecord = endNode;

						// We’ll need to calculate the heuristic value
						// using the function, since we don’t have an
						// existing record to use
						endNodeHeuristic = heuristics.Estimate(endNode);

					}
					// We’re here if we need to update the node
					// Update the cost, estimate and connection
					endNodeRecord.Connection = connection;
					endNodeRecord.CostSoFar = endNodeCost;
					endNodeRecord.EstimatedTotalCost = endNodeCost + endNodeHeuristic;

					// And add it to the list 
					if (!open.Contains(endNode))
					{
						open.Add(endNodeRecord);
					}
				}

				// We’ve finished looking at the connections for
				// the current node, so add it to the closed list
				// and remove it from the open list
				open.Remove(currentNode);
				closed.Add(currentNode);
			}

			// We’re here if we’ve either found the goal, or
			// if we’ve no more nodes to search, find which.

			if (currentNode != goal)
			{
				//	We’ve run out of nodes without finding the
				//  goal, so there’s no solution
				return new List<Connection<Node>>();
			}
		
			// Compile the list of connections in the path
		
			var pathList = new List<Connection<Node>>();

			while (currentNode != start)
			{
				pathList.Add(currentNode.Connection);
				currentNode = currentNode.Connection.GetFromNode();
			}

			pathList.Reverse();

			return pathList;
		}
	}
}
