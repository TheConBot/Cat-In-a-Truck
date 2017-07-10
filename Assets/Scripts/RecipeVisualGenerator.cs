using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeVisualGenerator : MonoBehaviour {

  [SerializeField]
  private CanvasGroup mainContainerGroup;

  [SerializeField]
  private Text titleText;

  [SerializeField]
  private Text pageNumber;

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

  private int bookIndex = 0;

  private void Start() {
    steps = StoreSteps();

    ToggleMenu(1);
    IncrementRecipe(0);
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

  public void IncrementRecipe(int index) {
    bookIndex += index;
    int count = Manager.instance.recipies.Count;
    if (bookIndex < 0) {
      bookIndex = count - 1;
    } else if (bookIndex >= count) {
      bookIndex = 0;
    }

    pageNumber.text = (bookIndex + 1) + "/" + count;
    CreateRecipeVisual(Manager.instance.recipies[bookIndex]);
  }

  public void ToggleMenu(int open = -1) {
    if (open == -1) {
      open = (int)mainContainerGroup.alpha;
    }
    mainContainerGroup.alpha = open;
    mainContainerGroup.interactable = (open == 1);
    mainContainerGroup.blocksRaycasts = (open == 1);
  }

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
        images[imageIndex].sprite = liquidSprites[(int)lri.GetLiquidType];
        images[imageIndex].gameObject.SetActive(true);

      }

      steps[i].SetActive(true);
    }
  }

}
