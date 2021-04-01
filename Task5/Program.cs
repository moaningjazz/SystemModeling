using System;
using System.Collections.Generic;

namespace Task5
{
	class Program
	{
		// Формула для получения случайной велечины с парметрами мю и сигма
		static double GetRandomValue(int expectValue, int deviation)
		{
			double sum = 0;

			Random random = new Random();
			for (int i = 0; i < 12; i++)
			{
				sum += random.NextDouble();
			}

			return expectValue - (deviation * (sum - 6));
		}

		//Задача 17. В начальный момент времени система состоит из трех новых элементов.Длительность безотказной работы каждого из элементов есть нормально распределенная случайная величина с параметрами   и.При отказе любых двух элементов вся система перестает работать.Построить модель возникновения отказов в указанной системе.Провести 1000 испытаний с моделью и оценить математическое ожидание и среднее квадратическое отклонение длительности безотказной работы системы.
		static void Main(string[] args)
		{
			Random random = new Random();

			//Ограничение до 100 (можно любое)
			int time = 100;

			//Генерируем рандомно мю и сигма, сигма пять процетов от мю
			int expectValue = random.Next(1, time);
			int deviation = random.Next((int)(expectValue * 0.5));

			//Количестов экспериметнов
			int experimentsCount = 1000;

			//Генерируем время безотказной работы каждой детали с взятыми мю и сигма
			double timeWorkintFistDetail = GetRandomValue(expectValue, deviation);
			double timeWorkintSecondDetail = GetRandomValue(expectValue, deviation);
			double timeWorkintThirdDetail = GetRandomValue(expectValue, deviation);

			//Количество рабочих систем
			int countWorkingSystem = 0;

			//Делаем словарь, там лежит ключ время работы и сколько систем работало это время
			IDictionary<int, int> timeAndCountWorkingSystem = new Dictionary<int, int>();

			for (int i = 0; i < experimentsCount; i++)
			{
				//Счётчик сломанхы деталей
				int countBrokenDetail = 0;

				//Генерируем время
				var currentTime = random.Next(time);

				//Смотрим сломались ли детали
				if (currentTime < timeWorkintFistDetail)
				{
					countBrokenDetail++;
				}
				if (currentTime < timeWorkintSecondDetail)
				{
					countBrokenDetail++;
				}
				if (currentTime < timeWorkintThirdDetail)
				{
					countBrokenDetail++;
				}

				//Если сломались меньше двух деталей, то система рабочая
				//Записываем время работы и попытку
				//или если указанное время уже было прибавляем попытку к уже существующему времени
				if (countBrokenDetail < 2)
				{
					countWorkingSystem++;
					if (timeAndCountWorkingSystem.ContainsKey(currentTime))
					{
						timeAndCountWorkingSystem[currentTime]++;
					}
					else
					{
						timeAndCountWorkingSystem.Add(currentTime, 1);
					}
				}
			}

			//Считаем мат ожидание по формуле
			double expectValueTimeWorkingSystem = 0;
			foreach (var timeAndCount in timeAndCountWorkingSystem)
			{
				expectValueTimeWorkingSystem += timeAndCount.Key * ((double)timeAndCount.Value / countWorkingSystem);
			}

			//Считаем квадрат мат ожидания
			double expectValueSquare = 0;
			foreach (var timeAndCount in timeAndCountWorkingSystem)
			{
				expectValueSquare += Math.Pow(timeAndCount.Key, 2) * ((double)timeAndCount.Value / countWorkingSystem);
			}

			//Считаем дисперсию
			double dispersion = expectValueSquare - Math.Pow(expectValueTimeWorkingSystem, 2);

			Console.WriteLine($"Expect value: {expectValueTimeWorkingSystem}{Environment.NewLine}" +
				$"Deviation: {Math.Sqrt(dispersion)}");

		}
	}
}
