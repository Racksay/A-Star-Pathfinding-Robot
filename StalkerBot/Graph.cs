using System;
using System.Collections.Generic;

namespace StalkerBot
{
	public class Graph
	{
		public Node[,] NodeGraph { get; set; } = new Node[12, 16];

		public List<Connection<Node>> GetConnections(Node currentNode)
		{
			return currentNode.Connections;
		}

		public void FillGraph()
		{
			int[][] array =
			{
				new[] {0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
				new[] {0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0},
				new[] {1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0},
				new[] {1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0},
				new[] {0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
				new[] {0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
				new[] {0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
				new[] {0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
				new[] {0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
				new[] {0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0},
				new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
				new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			};
	
			// Add the nodes.
			for (var i = 0; i < array.Length; i++)
			{		
				for (var j = 0; j < array[0].Length; j++)
				{
					NodeGraph[i,j] = new Node(i, j);
				}
			}

			// Add the connections
			for (var i = 0; i < 11; i++)
			{
				for (var j = 0; j < 15; j++)
				{

					if (array[i][j] == 0)
					{
						// Up/Down connection
						if (array[i + 1][j] == 0)
						{
							var connection = new Connection<Node>(50, NodeGraph[i,j], NodeGraph[i + 1,j]);
							var connection2 = new Connection<Node>(50, NodeGraph[i + 1, j], NodeGraph[i, j]);

							NodeGraph[i, j].Connections.Add(connection);
							NodeGraph[i + 1, j].Connections.Add(connection2);
						}

						// Right/left connection
						if (array[i][j + 1] == 0)
						{
							var connection = new Connection<Node>(50, NodeGraph[i, j], NodeGraph[i, j + 1]);
							var connection2 = new Connection<Node>(50, NodeGraph[i, j + 1], NodeGraph[i, j]);

							NodeGraph[i, j].Connections.Add(connection);
							NodeGraph[i, j + 1].Connections.Add(connection2);
						}

						// Vertical down connection
						if (array[i + 1][j + 1] == 0)
						{
							var connection = new Connection<Node>(70, NodeGraph[i, j], NodeGraph[i + 1, j + 1]);
							var connection2 = new Connection<Node>(70, NodeGraph[i + 1, j + 1], NodeGraph[i, j]);


							NodeGraph[i, j].Connections.Add(connection);
							NodeGraph[i + 1, j + 1].Connections.Add(connection2);
						}

						// Vertical Up connections
						if (i > 0 && array[i - 1][j + 1] == 0)
						{
							var connection = new Connection<Node>(70, NodeGraph[i, j], NodeGraph[i - 1, j + 1]);
							var connection2 = new Connection<Node>(70, NodeGraph[i - 1, j + 1], NodeGraph[i, j]);

							NodeGraph[i, j].Connections.Add(connection);
							NodeGraph[i - 1, j + 1].Connections.Add(connection2);
						}
					}
				}
			}

			// Add the remaining Connections
			for (var i = 0; i < 11; i++)
			{
				if(array[i+1][15] == 0 && array[i][15] == 0)
				{ 
					var connection = new Connection<Node>(50, NodeGraph[i, 15], NodeGraph[i + 1, 15]);
					var connection2 = new Connection<Node>(50, NodeGraph[i + 1, 15], NodeGraph[i, 15]);

					NodeGraph[i, 15].Connections.Add(connection);
					NodeGraph[i+1, 15].Connections.Add(connection2);
				}
			}

			for (var j = 0; j < array[0].Length - 1; j++)
			{
				if (array[11][j] == 0 && array[11][j+1] == 0)
				{
					var connection = new Connection<Node>(50, NodeGraph[11, j], NodeGraph[11, j + 1]);
					var connection2 = new Connection<Node>(50, NodeGraph[11, j + 1], NodeGraph[11, j]);

					NodeGraph[11, j].Connections.Add(connection);
					NodeGraph[11, j + 1].Connections.Add(connection2);
				}
			}

			for (var i = 0; i < array[0].Length - 1; i++)
			{
				if (array[11][i] == 0 && array[10][i + 1] == 0)
				{
					var connection = new Connection<Node>(70, NodeGraph[11, i], NodeGraph[10, i + 1]);
					var connection2 = new Connection<Node>(70, NodeGraph[10, i + 1], NodeGraph[11, i]);

					NodeGraph[11, i].Connections.Add(connection);
					NodeGraph[10, i+1].Connections.Add(connection2);
				}
			}

		}
	}
}
