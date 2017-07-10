using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour {

    public void LoadScene(string level) {
        SceneManager.LoadScene(level);
    }

}
