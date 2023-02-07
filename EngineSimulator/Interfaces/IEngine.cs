

namespace EngineSimulator
{
	public interface IEngine
	{
		string EngineModel { get; set; }
		bool IsStarted { get; set; }
		void StartEngine(double airT);
	}
}
