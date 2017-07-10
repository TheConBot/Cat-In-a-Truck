using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasUIView : MonoBehaviour {

  [SerializeField]
  private CanvasGroup mainGroup;

  public void ToggleMenu(int open = -1) {
    if (open == -1) {
      open = (int)mainGroup.alpha;
    }
    mainGroup.alpha = open;
    mainGroup.interactable = (open == 1);
    mainGroup.blocksRaycasts = (open == 1);
  }


}
