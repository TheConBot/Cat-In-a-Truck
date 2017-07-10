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
        Manager.instance.GenerateRecipes();

        SceneManager.LoadScene(level);
    }

    private void GetHighScores() {
        hs.GetHighScores();
        hs.DisplayTopScores();
    }

}
