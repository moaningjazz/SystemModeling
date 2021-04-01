using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task6
{
	class Program
	{
	/*Задача 17. Пуассоновские потоки частиц из двух источников сначала соединяются в один поток,
    в котором частицы нумеруются в порядке поступления.Затем этот поток разделяется на два таким образом,
    что первый поток состоит из четных частиц, а второй из нечетных.
    Построить функцию распределения и функцию плотности распределения длительности
    интервала времени между двумя частицами первого потока на основании 1000 выборочных значений уже разделенного потока.*/

		static void Main(string[] args)
		{
		    //общий поток, ключ -- номер
      Dictionary<int, double> stream = new Dictionary<int, double>();

            //лямбда -- значение
			double val = 0.1;

			for (int i = 1; i <= 1000; i++)
			{
        stream.Add(i, val);
				val += 0.000001;
			}

            //два потока для чётных и не
			//деление по номеру
			Dictionary<int, double> firstStream = new Dictionary<int, double>();
			Dictionary<int, double> secondStream = new Dictionary<int, double>();

      foreach (var particle in stream)
			{
        if (particle.Key % 2 == 0)
				{
          firstStream.Add(particle.Key, particle.Value);
				}
        else
				{
          secondStream.Add(particle.Key, particle.Value);
				}
			}

			//сами значения
			//цикл с 1 до 500 -- время
			//флаг для определения потока: чёт или не
			//вычисление значений для графиков

      int time = 1;

			IList<DataPoint> dataDensity = new List<DataPoint>();
			IList<DataPoint> dataDistribution = new List<DataPoint>();

      bool flagUseFirstStream = false;
			while (time <= 500)
			{
        if (flagUseFirstStream)
				{
          double point = firstStream[time] * Math.Pow(2.7, -(firstStream[time] * time));
          dataDensity.Add(new DataPoint(time, point));

          point = 1 - Math.Exp(-firstStream[time] * time);
          dataDistribution.Add(new DataPoint(time, point));
        }
        else
				{
          double point = secondStream[time] * Math.Pow(2.7, -(secondStream[time] * time));
          dataDensity.Add(new DataPoint(time, point));

          point = 1 - Math.Exp(-secondStream[time] * time);
          dataDistribution.Add(new DataPoint(time, point));
        }

        flagUseFirstStream = !flagUseFirstStream;
				time++;
			}

			var model = new PlotModel { Title = "Graph", DefaultFont = "Arial" };
			model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
			model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });

			var areaSeries = new AreaSeries();
			areaSeries.Points.AddRange(dataDensity);

			model.Series.Add(areaSeries);

			//график
			// х -- время
			// у -- лямбда от времени
//плотность
			var exporter = new PdfExporter { Width = 400, Height = 400 };
			using (var streamGraph = File.Create("density.pdf"))
			{
				exporter.Export(model, streamGraph);
			}

			var modelDistribution = new PlotModel { Title = "Graph", DefaultFont = "Arial" };
			modelDistribution.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
			modelDistribution.Axes.Add(new LinearAxis { Position = AxisPosition.Left });

			var areaSeriesDistribution = new AreaSeries();
			areaSeriesDistribution.Points.AddRange(dataDistribution);

			modelDistribution.Series.Add(areaSeriesDistribution);
//распр
			var exporterDistribution = new PdfExporter { Width = 400, Height = 400 };
			using (var streamGraph = File.Create("distribution.pdf"))
			{
				exporterDistribution.Export(modelDistribution, streamGraph);
			}
		}
	}
}
