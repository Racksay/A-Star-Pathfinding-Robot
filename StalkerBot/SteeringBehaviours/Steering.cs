using Robocode;

namespace StalkerBot.SteeringBehaviours
{
	public abstract class Steering
	{
		protected AdvancedRobot Robot { get; set; }
		protected ScannedRobotEvent Event { get; set; }

		protected Steering(AdvancedRobot robot, ScannedRobotEvent evnt)
		{
			Robot = robot;
			Event = evnt;
		}

		public abstract void GetSteering();
	}
}
