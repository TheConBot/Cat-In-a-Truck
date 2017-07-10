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

    private void Start() {
        ToggleMenu(0);
    }

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
        m = Mathf.Ceil(time / 60);
        s = time % 60;
        string sStr = s + "";
        if (s < 10) {
            sStr = "0" + s;
        }
        if (m > 0) {
            timer.text = m + ":" + sStr;
        } else {
            timer.text = sStr + "";
        }
    }

    public void UpdateScore() {
        score.text = "$" + Manager.instance.RoundScore;
    }


}
