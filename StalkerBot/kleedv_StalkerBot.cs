using System;
using System.Collections.Generic;
using System.Drawing;
using Robocode;
using Robocode.Util;
using StalkerBot;
using PointF = StalkerBot.SteeringBehaviours.PointF;

namespace PG4500_2016_Exam2
{
    public class kleedv_StalkerBot : AdvancedRobot
    {
	    private ScannedRobotEvent _evnt;

	    private bool _atNode = false;
	    private bool _findPath = false;
	    private bool _readyToMove = false;

		private List<Connection<Node>> _path = new List<Connection<Node>>();
	    private int _lenght = -1;

		private PathFinding _pathfinder = new PathFinding();
		private readonly Graph _graph = new Graph();
	

		public override void Run()
		{
			IsAdjustRadarForRobotTurn = true;
			IsAdjustRadarForGunTurn = true;

			_graph.FillGraph();

			while (!_atNode)
			{
				GoTo(new PointF(25, 25));
				Execute();
			}

			
			// Wait for Commisioner
			WaitForCommisioner();

			_findPath = true;

			while (true)
			{
				// Radar lock
				if (RadarTurnRemaining == 0.0)
				{
					SetTurnRadarRightRadians(double.PositiveInfinity);
				}

				if (_readyToMove)
				{
					for (var i = 0; i < _path.Count; i++)
					{
						_atNode = false;
						while (!_atNode && _path.Count > 0 && _lenght < _path.Count)
						{
							GoTo(new PointF(_path[i].GetToNode().Y*50 + 25, (11 - _path[i].GetToNode().X)*50 + 25));
							Execute();
						}
					}

				}
				WaitForCommisioner();
				_findPath = true;
				Execute();
			}
		}

	    private void WaitForCommisioner()
	    {
			for (var i = 0; i < 125; i++)
			{
				DoNothing();
			}
		}

	    private void Wait()
	    {
			for (var i = 0; i < 20; i++)
			{
				DoNothing();
			}
		}

		private void GoTo(PointF point)
		{
			var angle = NormalRelativeAngleRadians(
					AbsoluteBearingRadians(GetRobotLocation(), point) - HeadingRadians);

			SetTurnRightRadians(angle);
			Execute();

			if (Math.Abs(angle) > 0.003)
			{
				Wait();
			}

			SetAhead(GetRobotLocation().Distance(point));
			Execute();
			if (GetRobotLocation().Distance(point) < 0.3)
			{
				_atNode = true;
				_lenght++;
			}
		}
		private static double NormalRelativeAngleRadians(double angle)
		{
			return Math.Atan2(Math.Sin(angle), Math.Cos(angle));
		}
		private static double AbsoluteBearingRadians(PointF source, PointF target)
		{
			return Math.Atan2(target.X - source.X, target.Y - source.Y);
		}

		private PointF GetRobotLocation()
		{
			return new PointF(X, Y);
		}

		public override void OnScannedRobot(ScannedRobotEvent evnt)
	    {
			_evnt = evnt;

			
			// Get enemy X,Y coordinates
			var absoluteBearing = HeadingRadians + evnt.BearingRadians;

			var enemyX = X + Math.Sin(absoluteBearing) * evnt.Distance;
			var enemyY = Y + Math.Cos(absoluteBearing) * evnt.Distance;

			var myX = (int)(X / 50);
			var myY = (int)(Y / 50);

			var myNode = new Node(11 - myY, myX);

			var enemyNode = new Node(11 - ((int)enemyY / 50), (int)enemyX / 50);

			Console.WriteLine("EnemeyX: " + enemyNode.X + " EnemyY: " + enemyNode.Y);


			if(_graph.NodeGraph[enemyNode.X, enemyNode.Y].Connections.Count > 0 && _findPath)
			{ 
				_path = _pathfinder.AStar(_graph, _graph.NodeGraph[myNode.X, myNode.Y], 
						_graph.NodeGraph[enemyNode.X, enemyNode.Y], new Heuristics(enemyNode));
				_lenght = 0;
				_findPath = false;
				_readyToMove = true;
			}

			Console.WriteLine("\n\n_________PATH________");
			foreach (var node in _path)
			{
				Console.WriteLine("Coords: " + node.GetToNode().X + ", " + node.GetToNode().Y);
			}
			

			// Radar lock
			var angleToEnemy = HeadingRadians + evnt.BearingRadians;
			var radarTurn = Utils.NormalRelativeAngle(angleToEnemy - RadarHeadingRadians);
			var extraTurn = Math.Min(Math.Atan(50.0 / evnt.Distance), Rules.RADAR_TURN_RATE_RADIANS);

			radarTurn += (radarTurn < 0 ? -extraTurn : extraTurn);

			SetTurnRadarRightRadians(radarTurn);

		}

	    public override void OnPaint(IGraphics graphics)
	    {
		    foreach (var node in _path)
		    {
			    var rect = new Rectangle((node.GetToNode().Y )* 50, (11-node.GetToNode().X)*50, 50, 50);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(127, Color.DarkGreen)), rect);

		    }
			var rect2 = new Rectangle(0, 0, 50, 50);
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(127, Color.BlueViolet)), rect2);
	    }
    }
}
