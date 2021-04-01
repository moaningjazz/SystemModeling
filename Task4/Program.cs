using System;

namespace Task4
{
	class Program
	{
		//Задача 17. Два друга условились встретиться в определенном месте между 17 час.и 17 ч. 20 мин.Пришедший первым ожидает второго в течение десяти минут, после чего уходит.Построить модель процесса, оценить вероятность встречи этих друзей, если моменты прихода каждого из них являются независимыми равномерно распределенными непрерывными случайными величинами.
		static void Main(string[] args)
		{
			//Вреям начала 17:00
			double tstart = 0;
			//Время конца встречи 17:20
			double tend = 20;

			//Время ожидания
			double waitingTime = 10;

			//Количество экспериментов
			int experimentCount = 1000;

			Random random = new Random();

			//Переменная для количества подсчёта встреч
			int countMeetings = 0;
			for (int i = 0; i < experimentCount; i++)
			{
				//Генерируем в какую минуту придёт первый и второй друг (от 0 до 20)
				var timeFirst = random.Next(0, 21);
				var timeSecond = random.Next(0, 21);

				//Если дельта между временем двух друхей меньше времени ожидания, значит они встретились
				if (Math.Abs(timeFirst - timeSecond) < waitingTime)
				{
					countMeetings++;
				}
			}

			//Веротяность встречи количество встреч / количестов экспериментов
			Console.WriteLine($"Probability meetings is: {(double)countMeetings / experimentCount}");
		}
	}
}
