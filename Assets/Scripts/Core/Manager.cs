using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
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
            generalSoundSource.PlayOneShot(cashRegisterSound);
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
            generalSoundSource.PlayOneShot(cashRegisterSound);
            difficultySetting = value;
        }
    }
    private HighScoresManager highScoresManager;

    private List<PlayerInfo> highScoreInfo;
    public List<PlayerInfo> HighScoreInfo {
        get {
            return highScoreInfo;
        }

        set {
            highScoreInfo = value;
        }
    }
    [HideInInspector]
    public bool gameInputEnabled;
    private bool gameStarted;

    [Header("Sprites")]
    public Sprite[] catSprites;

    private AudioSource generalSoundSource;
    [Header("Sound Effects")]
    public AudioClip buzzerSound;
    public AudioClip cashRegisterSound;
    public AudioClip tenSecondWarningSound;
    public AudioClip[] meowingSounds;
    public AudioClip[] hissingSounds;
    public AudioClip[] eatingSounds;

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
        highScoresManager = GetComponent<HighScoresManager>();
        generalSoundSource = GetComponent<AudioSource>();
        RefreshHighScores();
    }

    public void RefreshHighScores() {
        HighScoreInfo = highScoresManager?.GetHighScores();
    }

    public void SetNewRecipies() {
        Recipies = RecipeGenerator.GenerateRecipies(difficultySetting.maxRecipieIngrediants, difficultySetting.recipieAmount);
    }

    public void StartRound() {
        if (gameStarted) {
            return;
        }
        StartCoroutine(StartRoundTimer());
    }

    private IEnumerator StartRoundTimer() {
        gameStarted = true;
        roundTime = difficultySetting.maxRecipieIngrediants * 60;
        roundScore = 0;
        while (roundTime > 0) {
            yield return new WaitForSeconds(1);
            roundTime--;
            if(roundTime == 10) {
                generalSoundSource.PlayOneShot(tenSecondWarningSound);
            }
        }
        generalSoundSource.PlayOneShot(buzzerSound);
        gameInputEnabled = false;
        yield return new WaitForSeconds(1);
        LoadScene(2);
        gameStarted = false;
    }

    public void LoadScene(int sceneIndex) {
        Cursor.visible = true;
        SceneManager.LoadScene(sceneIndex);
    }

    public void StoreHighScore(string name, int score, string difficulty) {
        if (name.IndexOf("*") != -1) {
            name = name.Replace("*", "");
        }
        string url = HighScoresManager.API_URL_BASE + HighScoresAPIKey.privateKey + "/add/" + name + "/" + score + "/0/" + difficulty;
        highScoresManager.SetHighScore(url);
    }
}
