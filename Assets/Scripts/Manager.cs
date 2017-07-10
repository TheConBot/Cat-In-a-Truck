using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static Manager instance { get; private set; }

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
    [SerializeField]
    private Difficulty difficultySetting;
    public Difficulty DifficultySetting {
        get {
            return difficultySetting;
        }

        set {
            difficultySetting = value;
        }
    }

    public Sprite[] cats;

    public enum GameState {
        TitleScreen,
        Playing,
        NewHighscore
    }
    private GameState currentState = GameState.TitleScreen;
    public GameState CurrentState {
        get {
            return currentState;
        }
    }

    public void Awake() {
        if (instance != null && instance != this) {
            // Destroy if another Gamemanager already exists
            Destroy(gameObject);
        }
        else {

            // Here we save our singleton instance
            instance = this;
            // Furthermore we make sure that we don't destroy between scenes
            DontDestroyOnLoad(gameObject);
        }
        recipies = RecipeGenerator.GenerateRecipies(difficultySetting.maxRecipieIngrediants);
    }

    public void StartRound() {
        if (currentState != GameState.Playing) {
            currentState = GameState.Playing;
            StartCoroutine(StartRoundTimer());
        }
    }

    private IEnumerator StartRoundTimer() {
        roundTime = difficultySetting.roundTime * 60;
        while (roundTime > 0) {
            yield return new WaitForSeconds(1);
            roundTime--;
        }

        currentState = GameState.NewHighscore;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("HighScore");
    }
}
