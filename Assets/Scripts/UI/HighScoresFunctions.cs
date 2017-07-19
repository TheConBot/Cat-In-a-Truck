using UnityEngine;
using UnityEngine.UI;
using System;

public class HighScoresFunctions : BaseMenuFuctions {

    public CanvasGroup inputGroup;
    public InputField nameEntry;
    public Text yourScoreText;


    override protected void Start() {
        Manager.Instance.RefreshHighScores();
        var score = Manager.Instance.RoundScore;
        var highScoresList = Manager.Instance.HighScoreInfo;
        if (highScoresList[highScoresList.Count - 1].score < score) {
            yourScoreText.text = $"Enter Your Highscore: {score}";
        }
        else {
            yourScoreText.text = $"Your Score: {score}";
            //inputGroup.interactable = false;
            //inputGroup.alpha = 0;
        }
        base.Start();
    }

    public void SubmitHighScore() {
        if (String.IsNullOrEmpty(nameEntry.text)) {
            Debug.LogWarning("Empty Field");
            return;
        }
        foreach(PlayerInfo player in Manager.Instance.HighScoreInfo) {
            if(nameEntry.text == player.name && Manager.Instance.RoundScore < player.score) {
                nameEntry.placeholder.GetComponent<Text>().text = "Name Already Taken";
                nameEntry.text = string.Empty;
                return;
            }
        }
        Manager.Instance.StoreHighScore(nameEntry.text, Manager.Instance.RoundScore, Manager.Instance.DifficultySetting.name);
        inputGroup.interactable = false;
        nameEntry.text = String.Empty;
        Manager.Instance.RefreshHighScores();
        DisplayTopScores();
    }
}
