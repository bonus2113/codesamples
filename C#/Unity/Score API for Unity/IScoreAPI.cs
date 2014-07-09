using System;
using System.Collections.Generic;

//Score API interface. Provides a template for basic communication with a system that provides a single list of scores.
public interface IScoreAPI {
	ScoreAPIMode CurrentMode { get; }
	string CurrentUser { get; }
	void GetAllScores(Action<List<ScoreInfo>> _getCallback);
	ScoreInfo GetLastScore();
	void PushNewScore(ScoreInfo _info, Action<bool> _addCallback);
}
