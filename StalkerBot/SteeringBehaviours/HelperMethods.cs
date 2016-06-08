using System;
using Robocode;

namespace StalkerBot.SteeringBehaviours
{
	// Made some nice XML documentation here, just to try it out
	public class HelperMethods
	{

		/// <summary>
		/// Normalize the bearing to the range (-180, 180) Degrees
		/// </summary>
		/// <param name="angle"></param>
		/// <returns>Normalized bearing in degrees</returns>
		public static double NormalizeBearing(double angle)
		{
			while (angle > 180) angle -= 360;
			while (angle < -180) angle += 360;
			return angle;
		}


		/// <summary>
		/// Computes the absolute bearing between two points
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns>The absolute bearing between 2 points</returns>
		public static double AbsoluteBearing(PointF p1, PointF p2)
		{
			var xo = p2.X - p1.X;
			var yo = p2.Y - p1.Y;
			var hyp = p1.Distance(p2);
			var arcSin = (180/Math.PI)*(Math.Asin(xo / hyp));
			double bearing = 0.0f;

			if (xo > 0 && yo > 0)
			{ // both pos: lower-Left
				bearing = arcSin;
			}
			else if (xo < 0 && yo > 0)
			{ // x neg, y pos: lower-right
				bearing = 360 + arcSin; // arcsin is negative here, actuall 360 - ang
			}
			else if (xo > 0 && yo < 0)
			{ // x pos, y neg: upper-left
				bearing = 180 - arcSin;
			}
			else if (xo < 0 && yo < 0)
			{ // both neg: upper-right
				bearing = 180 - arcSin; // arcsin is negative here, actually 180 + ang
			}

			return bearing;
		}


		/// <summary>
		/// Used to project a simple ray with given lengt and angle
		/// </summary>
		/// <param name="source"></param>
		/// <param name="angle"></param>
		/// <param name="length"></param>
		/// <returns>Returns a projected point from the source</returns>
		public static PointF Project(PointF source, double angle, double length)
		{
			return new PointF(source.X + Math.Sin(angle) * length,
				source.Y + Math.Cos(angle) * length);
		}

		/// <summary>
		/// Gets the bearing in degrees
		/// </summary>
		/// <param name="point"></param>
		/// <param name="robot"></param>
		/// <returns>Bearing in degrees to target</returns>
		public static double GetBearing(PointF point, AdvancedRobot robot)
		{
			var bearing = Math.PI/2 - Math.Atan2(point.Y - robot.Y, point.X - robot.X);
			return NormalizeBearing((180/Math.PI)*(bearing - robot.HeadingRadians));
		}


	}
}
