using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls.Shapes
{
	public class LineGeometry : Geometry
	{
		public LineGeometry()
		{

		}

		public LineGeometry(Point startPoint, Point endPoint)
		{
			StartPoint = startPoint;
			EndPoint = endPoint;
		}

		public static readonly BindableProperty StartPointProperty =
			BindableProperty.Create(nameof(StartPoint), typeof(Point), typeof(LineGeometry), new Point());

		public static readonly BindableProperty EndPointProperty =
			BindableProperty.Create(nameof(EndPoint), typeof(Point), typeof(LineGeometry), new Point());

		public Point StartPoint
		{
			set { SetValue(StartPointProperty, value); }
			get { return (Point)GetValue(StartPointProperty); }
		}

		public Point EndPoint
		{
			set { SetValue(EndPointProperty, value); }
			get { return (Point)GetValue(EndPointProperty); }
		}

		public override PathF PathForBounds(Graphics.Rectangle rect)
		{
			var path = new PathF();
			path.Move((float)StartPoint.X, (float)StartPoint.Y);
			path.LineTo((float)EndPoint.X, (float)EndPoint.Y);
			return path;
		}
	}
}