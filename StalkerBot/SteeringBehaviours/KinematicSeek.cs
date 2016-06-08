using Robocode;

namespace StalkerBot.SteeringBehaviours
{
    public class KinematicSeek : Steering
	{
		// the angle to the target
		public double TargetDeg { get; set; }

		public PointF TargetPos { get; set; }
			
		public double MaxSpeed { get; set; } = 100;


	    public KinematicSeek(AdvancedRobot robot, ScannedRobotEvent evnt) : base(robot, evnt)
	    {}

	    public override void GetSteering()
		{
			var angleToTarget = HelperMethods.GetBearing(TargetPos, Robot);

			Robot.SetTurnRightRadians(angleToTarget);
			Robot.SetAhead(MaxSpeed);
		}
	}
}
