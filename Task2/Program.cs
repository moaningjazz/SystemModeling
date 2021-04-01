using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task2
{
	class Program
	{
		static void Main(string[] args)
		{
			IList<DataPoint> data = new List<DataPoint>();

			var model = new PlotModel { Title = "Graph", DefaultFont = "Arial" };
			model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
			model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });

			for (double j = 0; j < 10; j += 1)
			{
				for (double i = 0; i < 1; i += 0.0001)
				{
					var values = F(i, i, j);
					data.Add(new DataPoint(values.xDot, values.yDot));
				}
				
				var areaSeries = new AreaSeries();
				areaSeries.Points.AddRange(data);

				model.Series.Add(areaSeries);
				data = new List<DataPoint>();
			}

			var exporter = new PdfExporter { Width = 400, Height = 400 };
			using (var streamGraph = File.Create("graph.pdf"))
			{
				exporter.Export(model, streamGraph);
			}

		}

		static (double xDot, double yDot) F(double x, double y, double c)
		{
			var xDot = Math.Exp(Math.Sqrt(6) * y) + Math.Exp(-Math.Sqrt(6) * y) - (y / 3);
			var yDot = c * Math.Exp(Math.Sqrt(7) * x) + c * Math.Exp(-Math.Sqrt(7) * x) - ((3 * x) / 7);

			return (xDot, yDot);
		}
	}
}
