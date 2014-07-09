using System;
using System.Collections.Generic;
using UnityEngine;

//API implementation to save and load scores from local storage.
//Scores are saved in a JSON string in Unity's PlayerPrefs
public class LocalScoreAPI : IScoreAPI {
	#region Fields
	List<ScoreInfo> scores;
	ScoreInfo lastScore;
	#endregion
	
	#region Constructors
	public LocalScoreAPI() {
		Load ();
	}
	#endregion
	
	#region IScoreAPI Implementation
	public ScoreAPIMode CurrentMode { get { return ScoreAPIMode.Guest; } }

	public string CurrentUser { get { return lastScore.Name; } }

	public void GetAllScores (Action<List<ScoreInfo>> _getCallback) {
		_getCallback (new List<ScoreInfo>(scores.ToArray()));
	}
	
	public ScoreInfo GetLastScore () {
		return lastScore;
	}

	public void PushNewScore (ScoreInfo _info, Action<bool> _addCallback) {
		lastScore = _info;
		int index = scores.FindIndex (s => s.Name == _info.Name);
		if (index != -1) {
			if(scores[index].Score < _info.Score) {
				scores[index] = _info;
			}
		} else {
			scores.Add (_info);	
		}

		scores.Sort( (x, y) => y.Score.CompareTo(x.Score));
		Save ();
		_addCallback (true);
	}
	#endregion

	#region Private Methods
	//Loads scores from a JSON string saved in PlayerPrefs
	void Load() {
		var jsonString = PlayerPrefs.GetString ("Scores", "[]");
		var jsonObj = new JSONObject (jsonString);
		scores = new List<ScoreInfo> ();
		foreach (var score in jsonObj.list) {
			ScoreInfo newInfo = new ScoreInfo();
			score.GetField(ref newInfo.Name, "name");
			score.GetField(ref newInfo.Score, "score");
			scores.Add(newInfo);
		}
		scores.Sort( (x, y) => y.Score.CompareTo(x.Score));
	}
	
	void Save() {
		var json = new JSONObject (JSONObject.Type.ARRAY);
		foreach (ScoreInfo score in scores) {
			var scoreObj = new JSONObject(JSONObject.Type.OBJECT);
			scoreObj.AddField("name", score.Name);
			scoreObj.AddField("score", score.Score);
			json.Add(scoreObj);
		}
		
		PlayerPrefs.SetString("Scores", json.Print ());
		PlayerPrefs.Save ();
	}
	#endregion
}