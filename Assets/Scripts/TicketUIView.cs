using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketUIView : MonoBehaviour {

  public Image timer;
  public Text title;

  public void SetTitle(string text) {
    title.text = text;
  }

  public void SetTimer(float timeLeft, float startTime) {
    float fillAmount = (timeLeft / startTime);
    Debug.Log(fillAmount);
    timer.fillAmount = fillAmount;
  }

}
