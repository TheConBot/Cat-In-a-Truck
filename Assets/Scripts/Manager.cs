using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public List<Recipie> recipies;

    private float roundTime;
    public float RoundTime { 
		get {
			return roundTime; 
		}
		set {
			roundTime = value; 
		}
	}	

    public Difficulty difficultySetting;

	public enum GameState{
		TitleScreen,
		Playing,
		NewHighscore
	}
	[HideInInspector]
	public GameState currentState;

}
