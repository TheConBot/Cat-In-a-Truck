using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketUIView : MonoBehaviour {

  public Image timer { private get; set; }
  public Text title { private get; set; }

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
