using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CookingContainer : Container {

    private enum CookOptions {
        Cut,
        Fry,
        Steam
    }
    [SerializeField]
    private CookOptions cookOption;
    [SerializeField]
    private bool collectTimeEnabled;
    [SerializeField]
    private Text statusText;
    [SerializeField]
    private Image collectTimer;
    [SerializeField]
    private CanvasGroup mainGroup;
    [Range(1, 60)]
    public float cookTime = 10, collectTime = 5;

    private void Start() {
        ToggleStatus(0);
    }

    override public void AddToContainer(Ingredient ingredient) {
        base.AddToContainer(ingredient);
        StartCoroutine(CookFood(ingredient as SolidIngredient));
    }

    override public Ingredient TakeFromContainer() {
        StopAllCoroutines();
        ToggleStatus(0);
        return base.TakeFromContainer();
    }

    public bool CanUseCookingContainer(SolidIngredient ingredient) {
        if (cookOption == CookOptions.Cut && ingredient.IsCut) {
            Debug.LogWarning("Cannot use " + DisplayName + ". " + ingredient.DisplayName + " has already been cut.");
            return false;
        }
        else if (cookOption != CookOptions.Cut && ingredient.GetCookState != SolidIngredient.CookState.Raw) {
            Debug.LogWarning("Cannot use " + DisplayName + ". " + ingredient.DisplayName + " has already been " + ingredient.GetCookState + ".");
            return false;
        }
        return true;
    }

    private IEnumerator CookFood(SolidIngredient ingredient) {
        ToggleStatus(1);
        Debug.Log("Ingredient Added: " + ingredient.DisplayName + ", Cook Time: " + cookTime + ", Cook Method: " + cookOption + ", Can Ruin: " + collectTimeEnabled);


        SetStatus("Cooking");
        StartCoroutine(PlayAudio());
        float _cookTime = cookTime;
        while (_cookTime > 0) {
            yield return new WaitForSeconds(Time.deltaTime);
            SetTimer(_cookTime, cookTime);
            _cookTime -= Time.deltaTime;
        }

        switch (cookOption) {
            case CookOptions.Cut:
                ingredient.Cut();
                SetStatus("Cut!");
                break;
            case CookOptions.Fry:
                ingredient.Fry();
                SetStatus("Fried!");
                break;
            case CookOptions.Steam:
                ingredient.Steam();
                SetStatus("Steamed!");
                break;
        }

        Debug.Log("Cooking Complete! " + ingredient.DisplayName + " is " + ingredient.GetCookState + " and " + (ingredient.IsCut ? "Cut" : "Not Cut") + ". Collect your " + ingredient.DisplayName + ".");


        if (collectTimeEnabled) {
            Debug.Log("Collect Time: " + collectTime);
            float _collectTime = collectTime;
            while (_collectTime > 0) {
                yield return new WaitForSeconds(Time.deltaTime);
                SetTimer(_collectTime, collectTime);
                _collectTime -= Time.deltaTime;
            }

            SetStatus("Ruined");
            ingredient.Ruin();
            Debug.Log("Oh No! " + ingredient.DisplayName + " is " + ingredient.GetCookState + ".");
        }
        yield return null;
    }

    public void SetTimer(float timeLeft, float startTime) {
        float fillAmount = (timeLeft / startTime);
        collectTimer.fillAmount = fillAmount;
    }

    public void SetStatus(string text) {
        statusText.text = text;
    }

    public void ToggleStatus(int open = -1) {
        if (open == -1) {
            open = (int)mainGroup.alpha;
        }
        mainGroup.alpha = open;
        mainGroup.interactable = (open == 1);
        mainGroup.blocksRaycasts = (open == 1);
    }

    public IEnumerator PlayAudio() {
        gameObject.GetComponent<AudioSource>().Play();
        yield return null;
    }


}
