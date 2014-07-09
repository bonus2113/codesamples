using UnityEngine;
using System;
using System.Collections; 
using System.Collections.Generic;

//API implementation to save and load scores from and to the GameJolt server.
//Uses the Unity GameJolt API provided by GameJolt (GJAPI) to verify users and load and save scores.
public class GameJoltScoreAPI : IScoreAPI {
	#region Constants
	//The game id from the GameJolt settings
	const int GAME_ID = 1337;
	const string PRIVATE_KEY = "game jolt private key";

	//Test values to use in the editor.
	const string TEST_USER = "user";
	const string TEST_TOKEN = "token";
	#endregion
	
	#region Fields
	//Fields to remember callbacks for async get and add
	//Problem to solve: Callbacks get overwritten on each call to get or add. Callbacks might be wrong. Not a problem in my use cases.
	Action<List<ScoreInfo>> currentGetCallback;
	Action<bool> currentAddCallback;
	
	bool wasInitialized = false;
	ScoreInfo lastScore;
	ScoreAPIMode currentMode = ScoreAPIMode.Guest;
	#endregion
	
	#region Constructors
	public GameJoltScoreAPI () {
		if(GJAPI.GameID == 0) {
			currentMode = ScoreAPIMode.Guest;
			GJAPI.Init ( GAME_ID, PRIVATE_KEY );
			
			//Use a test user when starting the game from the editor
#if UNITY_EDITOR
			GJAPI.Users.Verify ( TEST_USER, TEST_TOKEN );
#else
			GJAPIHelper.Users.GetFromWeb(OnGetFromWeb);
#endif
			//Hook up callbacks
			GJAPI.Users.VerifyCallback += OnVerifyUser;
			GJAPI.Scores.GetMultipleCallback += OnGetMultiple;
			GJAPI.Scores.AddCallback += OnScoreAdd;
			wasInitialized = true;
		}
	}
	#endregion
	
	#region IScoreAPI Implementation
	public ScoreAPIMode CurrentMode { get { return currentMode; } }
	public string CurrentUser { get { return CurrentMode == ScoreAPIMode.Guest ? lastScore.Name : GJAPI.User.Name; } }

	public void GetAllScores (Action<List<ScoreInfo>> _getCallback) {
		currentGetCallback = _getCallback;
		GJAPI.Scores.Get (false, 0, 20);
	}
	
	public ScoreInfo GetLastScore () {
		return lastScore;
	}
	
	public void PushNewScore (ScoreInfo _info, Action<bool> _addCallback) {
		currentAddCallback = _addCallback;
		lastScore = _info;
		if (CurrentMode == ScoreAPIMode.Verified) {
			GJAPI.Scores.Add (_info.Score.ToString (), (uint)_info.Score);
		} else {
			GJAPI.Scores.AddForGuest (_info.Score.ToString (), (uint)_info.Score, _info.Name);
		}
	}
	#endregion
	
	#region Private Methods
	void OnGetMultiple(GJScore[] _scores) {
		List<ScoreInfo> convertedScores = new List<ScoreInfo> ();
		foreach(var score in _scores) {
			convertedScores.Add( new ScoreInfo() { Name = score.Name, Score = (int)score.Sort });
		}
		currentGetCallback (convertedScores);
	}

	void OnGetFromWeb(string _user, string _token) {
		GJAPI.Users.Verify ( _user, _token );
	}

	void OnScoreAdd( bool _success) {
		currentAddCallback (_success);
	}
	
	void OnVerifyUser ( bool success ) {
		if ( success ) {
			Debug.Log ( "User successfully verified!" );
			currentMode = ScoreAPIMode.Verified;
		}
		else {
			Debug.Log ( "Um... Something went wrong." );
			currentMode = ScoreAPIMode.Guest;
		}
	}
	#endregion
}