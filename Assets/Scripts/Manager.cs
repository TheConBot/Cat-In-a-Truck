using System.Collections.Generic;
using UnityEngine;

public class Manager : SingletonMonoBehaviour<Manager> {

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

    private int roundScore;
    public int RoundScore {
        get {
            return roundScore;
        }

        set {
            roundScore = value;
        }
    }

    private Difficulty difficultySetting;
    public Difficulty DifficultySetting {
        get {
            return difficultySetting;
        }

        set {
            difficultySetting = value;
        }
    }

    public enum GameState{
		TitleScreen,
		Playing,
		NewHighscore
	}
	private GameState currentState;
    public GameState CurrentState {
        get {
            return currentState;
        }
    }

    override public void Awake() {
        base.Awake();
        recipies = RecipeGenerator.GenerateRecipies();
    }
}
