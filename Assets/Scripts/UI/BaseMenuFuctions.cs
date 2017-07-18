using UnityEngine.UI;
using UnityEngine;

public abstract class BaseMenuFuctions : MonoBehaviour {
    [SerializeField]
    protected Text highScoresText;

    private void Start() {
        if (Manager.Instance.HighScoreInfo != null) {
            DisplayTopScores();
        }
        else {
            highScoresText.text = "Could Not Display Highscores";
        }
    }

    protected void DisplayTopScores() {
        highScoresText.text = "Top 10 Scores:\n";
        foreach (PlayerInfo info in Manager.Instance.HighScoreInfo) {
            highScoresText.text += info.name + ": ";
            highScoresText.text += info.score + " ";
            highScoresText.text += "(" + info.text + ")";
            highScoresText.text += "\n";
        }
    }

    virtual public void LoadSceneFromButton(int sceneIndex) {
        Manager.Instance.LoadScene(sceneIndex);
    }
}
