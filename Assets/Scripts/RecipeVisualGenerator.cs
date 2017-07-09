using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeVisualGenerator : MonoBehaviour {

  [SerializeField]
  private Text titleText;

  [SerializeField]
  private GameObject stepPanelContainer;
  [SerializeField]
  private List<GameObject> steps;

  [SerializeField]
  private List<Sprite> solidSprites;

  [SerializeField]
  private List<Sprite> liquidSprites;

  [SerializeField]
  private Sprite cutSprite;

  private void Start() {
    steps = StoreSteps();
  }

  private List<GameObject> StoreSteps() {
    List<GameObject> _steps = new List<GameObject>();
    for (int i = 0; i < stepPanelContainer.transform.childCount; i++) {
      GameObject step = stepPanelContainer.transform.GetChild(i).gameObject;
      step.SetActive(false);
      _steps.Add(step);
    }
    return _steps;
  }

  // this is all pseudo-code until i fully see how the recipes are written!

  private void CreateRecipe(Recipie r) {
    titleText.text = r.displayName;

    for (int i = 0; i < r.ingredients.Length; i++) {
      if (r.ingredients[i] is SolidRecipieIngredient) {

      }
      steps[i].SetActive(true);
    }
  }

}
