using System;
using EngineSimulator;

namespace EngineTests
{
	static public class TestingStand
	{
		static bool isTesting = false;
		static bool isOverheat;
		static private int maxTime = 10000;


		static public void StartTemperatureTest(InternalCombustionEngine engine, double airT)
		{
			isTesting = true;
			engine.StartEngine(airT);
		}

		static public bool OverheatingSensor(double engineT, double maxT, int workTime)
		{
			if (Math.Round(engineT, MidpointRounding.AwayFromZero) >= maxT)
			{
				isOverheat = true;
				return true;
			}
			else
			{
				if (workTime > maxTime)
				{
					isOverheat = false;
					return true;
				}
				return false;
			}
			
			
		}

		static public void EndTeperatureTest(InternalCombustionEngine engine, int runtime)
		{
			isTesting = false;
			engine.OffEngine();
			if (isOverheat == true)
				Console.WriteLine("Time to overheat: " + runtime + " seconds");
			else
				Console.WriteLine("Time to overheat more than 10000 seconds");
		}
	}
}
