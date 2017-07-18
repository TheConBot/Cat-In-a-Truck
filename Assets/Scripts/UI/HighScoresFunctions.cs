using UnityEngine;
using UnityEngine.UI;
using System;

public class HighScoresFunctions : BaseMenuFuctions {

    public CanvasGroup inputGroup;
    public InputField nameEntry;

    public void HighScoreEntry() {
        if (String.IsNullOrEmpty(nameEntry.text)) {
            Debug.LogWarning("Empty Field");
            return;
        }
        Manager.Instance.StoreHighScore(nameEntry.text, Manager.Instance.RoundScore, Manager.Instance.DifficultySetting.name);
        inputGroup.interactable = false;
        nameEntry.text = String.Empty;
        Manager.Instance.RefreshHighScores();
        DisplayTopScores();
    }
}
