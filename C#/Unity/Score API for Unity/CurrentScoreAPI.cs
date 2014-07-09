using UnityEngine;
using System.Collections;

//Different types of available score APIs
public enum ScoreAPI {
	Local,
	GameJolt
}

//Unity MonoBehaviour to allow in editor API choosing. (And service location via a Singleton, which
//could be done in a number of different ways).
public class CurrentScoreAPI : MonoBehaviour {
	public ScoreAPI CurrentAPI;

	public static IScoreAPI Instance { get; private set; }
	
	void Start () {
		if(Instance == null) {
			switch (CurrentAPI) {
				case ScoreAPI.Local: Instance = new LocalScoreAPI(); break;
				case ScoreAPI.GameJolt: Instance = new GameJoltScoreAPI(); break;
			}
		}
	}

}
