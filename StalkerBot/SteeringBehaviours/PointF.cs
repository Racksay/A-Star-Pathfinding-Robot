namespace StalkerBot.SteeringBehaviours
{
    public class PointF
	{
		public double X { get; set; }
		public double Y { get; set; }

		public PointF()
		{
			Y = 0;
			X = 0;
		}

		public PointF(PointF p)
		{
			Y = p.Y;
			X = p.X;
		}

		public PointF(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double Distance(PointF target)
		{
			var dx = X - target.X;
			var dy = Y - target.Y;
			return System.Math.Sqrt(dx*dx + dy*dy);
		}
	}
}
