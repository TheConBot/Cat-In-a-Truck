using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasUIView : MonoBehaviour {

    [SerializeField]
    private CanvasGroup mainGroup;

    [SerializeField]
    private Text timer;

    [SerializeField]
    private Text score;

    public void ToggleMenu(int open = -1) {
        if (open == -1) {
            open = (int)mainGroup.alpha;
        }
        mainGroup.alpha = open;
        mainGroup.interactable = (open == 1);
        mainGroup.blocksRaycasts = (open == 1);
    }

    private void Update() {
        UpdateTimer();
        UpdateScore();
    }

    public void UpdateTimer() {
        float time = Manager.instance.RoundTime;
        float m = 0;
        float s = 0;
        m = time % 60;
        s = time - (m * 60); 
        if (m > 0) {
            timer.text = m + ":" + s;
        } else {
            timer.text = s + "";
        }
    }

    public void UpdateScore() {
        score.text = Manager.instance.RoundScore + "";
    }


}
