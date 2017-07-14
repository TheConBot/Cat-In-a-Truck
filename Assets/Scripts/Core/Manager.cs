using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static Manager Instance { get; private set; }

    private List<Recipie> recipies;
    public List<Recipie> Recipies {
        get {
            if(recipies == null) {
                recipies = new List<Recipie>();
            }
            return recipies;
        }

        set {
            if (recipies == null) {
                recipies = new List<Recipie>();
            }
            recipies = value;
        }
    }

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
            cashSound.Play();
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
            cashSound.Play();
            difficultySetting = value;
        }
    }

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

    public Sprite[] catSprites;
    public AudioSource buzzerSound;
    public AudioSource cashSound;
    public AudioSource tenSound;


    public void Awake() {
        if (Instance != null && Instance != this) {
            // Destroy if another Gamemanager already exists
            Destroy(gameObject);
        }
        else {

            // Here we save our singleton instance
            Instance = this;
            // Furthermore we make sure that we don't destroy between scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GenerateRecipes() {
        Recipies = RecipeGenerator.GenerateRecipies(difficultySetting.maxRecipieIngrediants, difficultySetting.recipieAmount);
    }

    public void StartRound() {
        if (currentState != GameState.Playing) {
            currentState = GameState.Playing;
            StartCoroutine(StartRoundTimer());
        }
    }

    private void LoadScene(string sceneName) {
        StopAllCoroutines();
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator StartRoundTimer() {
        roundTime = difficultySetting.roundTime * 60;
        roundScore = 0;
        while (roundTime > 0) {
            yield return new WaitForSeconds(1);
            roundTime--;
            if(roundTime == 10) {
                tenSound.Play();
            }
        }
        currentState = GameState.NewHighscore;
        buzzerSound.Play();
        yield return new WaitForSeconds(1);
        LoadScene("HighScore");
    }
}
