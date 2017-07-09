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
  private List<Sprite> cookstateSprites;

  [SerializeField]
  private Sprite cutSprite;

  private void Start() {
    steps = StoreSteps();

    CreateRecipeVisual(Manager.instance.recipies[0]);
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

  private List<Image> GetImages(GameObject container) {
    List<Image> _images = new List<Image>();
    for (int i = 0; i < container.transform.childCount; i++) {
      Image img = container.transform.GetChild(i).GetComponent<Image>();
      img.gameObject.SetActive(false);
      _images.Add(img);
    }

    return _images;
  }

  // this is all pseudo-code until i fully see how the recipes are written!

  public void CreateRecipeVisual(Recipie r) {
    titleText.text = r.displayName;

    for (int i = 0; i < r.ingredients.Length; i++) {
      List<Image> images = GetImages(steps[i]);

      int imageIndex = 0;

      if (r.ingredients[i] is SolidRecipieIngredient) {
        SolidRecipieIngredient sri = r.ingredients[i] as SolidRecipieIngredient;
        images[imageIndex].sprite = solidSprites[(int)sri.GetSolidType];
        images[imageIndex].gameObject.SetActive(true);
        imageIndex++;

        if (sri.IsCut) {
          images[imageIndex].sprite = cutSprite;
          images[imageIndex].gameObject.SetActive(true);
          imageIndex++;
        }

        if ((int)sri.GetCookState > 0) {
          images[imageIndex].sprite = cookstateSprites[(int)sri.GetCookState];
          images[imageIndex].gameObject.SetActive(true);
        }

      } else if (r.ingredients[i] is LiquidRecipieIngredient) {
        LiquidRecipieIngredient lri = r.ingredients[i] as LiquidRecipieIngredient;
        Debug.Log(lri.GetLiquidType);
        images[imageIndex].sprite = liquidSprites[(int)lri.GetLiquidType];
        images[imageIndex].gameObject.SetActive(true);

      }

      steps[i].SetActive(true);
    }
  }

}
