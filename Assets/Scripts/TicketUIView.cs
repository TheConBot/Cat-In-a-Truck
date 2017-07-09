using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketUIView : MonoBehaviour {

  private Image timer;
  private Text title;

  public void Start() {
    timer = GetComponentInChildren<Image>();
    title = GetComponentInChildren<Text>();
  }

  public void SetTitle(string text) {
    title.text = text;
  }

  public void SetTimer(float timeLeft, float startTime) {
    timer.fillAmount = (timeLeft / startTime);
  }

}
