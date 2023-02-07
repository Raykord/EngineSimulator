using System;
using EngineTests;


namespace EngineSimulator
{
	internal class Program
	{
		static void Main(string[] args)
		{
			bool flag = true;
			double airT = 0;

			InternalCombustionEngine engine = new InternalCombustionEngine()
			{
				EngineModel = "SR20",
				I = 10,
				M = new int[] { 20, 75, 100, 105, 75, 0 },
				V = new int[] { 0, 75, 150, 200, 250, 300 },
				T = 110,
				Hm = 0.01,
				Hv = 0.0001,
				C = 0.1
			};

			
			do
			{
				try
				{
					Console.WriteLine("Input ambient temperature");
					airT = Convert.ToDouble(Console.ReadLine());
					flag = false;
				}
				catch (Exception e)
				{
					Console.Clear();
					Console.WriteLine(e.Message);
				}

			} while (flag);

			TestingStand.StartTemperatureTest(engine, airT);
		}
	}
}
