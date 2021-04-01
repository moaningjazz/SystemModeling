using System;

namespace Task3
{
	class Program
	{
		//Задача 17. Вероятность победы первой футбольной команды в матче над второй командой равна 0,3, а вероятность победы первой команды в матче над третьей командой равна 0,4. Оценить вероятность победы первой команды в матчах со второй и третьей командами.Оценку провести на основании 1000 испытаний.
		static void Main(string[] args)
		{
			//Колиество эксперивметнов
			int countExperiments = 1000;

			//Вероятности выигрыша у второй и третьей команды
			double probabilityWinOnSecondTeam = 0.3;
			double probabilityWinOnThirdTeam = 0.4;

			Random random = new Random();

			//Подсчёт количества выигешей
			int countVictories = 0;
			for (int i = 0; i < countExperiments; i++)
			{
				//Генерируем рандомно два значения
				//Значения от 0 до 1, это вероятности
				var fistProbability = random.NextDouble();
				var secondProbability = random.NextDouble();

				//Если выпашие вероятности больше либо равны чем вероятности выигрыша над командами
				//то считаем победу
				if (probabilityWinOnSecondTeam >= fistProbability &&
					probabilityWinOnThirdTeam >= secondProbability)
				{
					countVictories++;
				}
			}

			//Делим количество побед на общее количестов экспериментов, получаем вероятность выигрыша
			Console.WriteLine($"Probability win at both team: {(double)countVictories / countExperiments}");
		}
	}
}
