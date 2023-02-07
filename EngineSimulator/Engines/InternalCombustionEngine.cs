using System;
using EngineTests;

namespace EngineSimulator
{
	public class InternalCombustionEngine : IEngine
	{
		public string EngineModel { get; set; } 
		public bool IsStarted { get; set; } = false;
		public double I { get; set; } //Момент инерции двигателя
		public int[] M { get; set; } //Крутящий момент
		public int[] V { get; set; } //Скорость вращения каленвала
		public double T { get; set; } //Температура перегрева
		public double Hm { get; set; } //Коэффициент зависимости скорости нагрева от крутящего момента
		public double Hv { get; set; } //Коэффициент зависимости скорости нагрева от скорости вращения коленвала
		public double C { get; set; } //Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды
		public double EngineT { get; set; } //Температура двигателя
		

		



		private int index = 0;
		

		public void StartEngine(double airT)
		{
			IsStarted = true;
			int runTime = 0;
			double mNow = M[index]; //Крутящий момент промежуточное значение
			double vNow = V[index]; //Скорость вращения каленвала промежуточное значение
			double a; //Ускорение вращения каленвала
	

			EngineT = airT;

			while (IsStarted)
			{
				EngineT += Vh(vNow, mNow) + Vc(airT, EngineT);
				
				if (index != (M.Length - 1))
				{
					if (mNow >= M[index + 1] && index < (M.Length - 1))
					{
						index++;
					}
						
				}
				
				a = mNow / I;
				vNow = a * runTime;
				
				if (index != M.Length - 1)
				{
					mNow = M[index] + (((vNow - V[index]) / (V[index + 1] - V[index])) * ((M[index + 1] - M[index]) / 1));
				}
				else
				{
					mNow = M[M.Length - 1];
					vNow = V[V.Length - 1];
				}


				
				if (TestingStand.OverheatingSensor(EngineT, T, runTime))
				{
					TestingStand.EndTeperatureTest(this, runTime);
				}
				runTime++;
			}
			


		}

		public void OffEngine()
		{
			IsStarted = false;
		}

		private double Vh(double vNow, double mNow) //Возвращает скорость нагрева двигателя
		{
			double vH;
			vH = mNow * Hm + Math.Pow(vNow, 2) * Hv;
			return vH;
		}

		private double Vc(double airT, double engineT) //Возвращает скорость охлаждения двигателя
		{
			double vC;
			vC = C * (airT - engineT);
			return vC;
		}
	}
}





