namespace StalkerBot
{
	public class Connection<T>
	{
		private readonly int _cost;
		private readonly T _fromNode;
		private readonly T _toNode;

		public Connection(int cost, T fromNode, T toNode)
		{
			_cost = cost;
			_fromNode = fromNode;
			_toNode = toNode;
		}


		public T GetToNode()
		{
			return _toNode;
		}

		public T GetFromNode()
		{
			return _fromNode;
		}

		public int GetCost()
		{
			return _cost;
		}
	}
}
