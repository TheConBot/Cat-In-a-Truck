using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour {

    [SerializeField]
    private StoreHighScores hs;

    private void Start() {
        GetHighScores();
    }

    public void LoadScene(string level) {
        Manager.Instance.GenerateRecipes();

        SceneManager.LoadScene(level);
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void GetHighScores() {
        hs.GetHighScores();
        hs.DisplayTopScores();
    }

}
