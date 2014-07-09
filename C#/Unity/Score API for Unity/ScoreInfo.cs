using System;

//Data structure to hold information about a single score entry.
//Common between different APIs
public struct ScoreInfo {
	#region Properties
	public string Name;
	public int Score;
	#endregion

	#region Constructors
	public ScoreInfo(string _name, int _score) {
		Name = _name;
		Score = _score;
	}
	#endregion
	
	#region Public Methods
 	public override bool Equals (object obj) {
		return (obj is ScoreInfo) && Name == ((ScoreInfo)obj).Name && Score == ((ScoreInfo)obj).Score;
	}
	#endregion
}
