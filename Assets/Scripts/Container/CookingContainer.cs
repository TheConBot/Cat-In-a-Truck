using System.Collections;
using UnityEngine;

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
    [Range(1, 60)]
    public float cookTime = 10, collectTime = 5;

    override public void AddToContainer(Ingredient ingredient) {
        base.AddToContainer(ingredient);
        StartCoroutine(CookFood(ingredient as SolidIngredient));
    }

    override public Ingredient TakeFromContainer() {
        StopAllCoroutines();
        return base.TakeFromContainer();
    }

    public bool CanUseCookingContainer(SolidIngredient ingredient) {
        if (cookOption == CookOptions.Cut && ingredient.IsCut) {
            Debug.LogWarning("Cannot use " + DisplayName + ". " + ingredient.DisplayName + " has already been cut.");
            return false;
        }
        else if (cookOption != CookOptions.Cut && ingredient.cookState != SolidIngredient.CookState.Raw) {
            Debug.LogWarning("Cannot use " + DisplayName + ". " + ingredient.DisplayName + " has already been " + ingredient.cookState + ".");
            return false;
        }
        return true;
    }

    private IEnumerator CookFood(SolidIngredient ingredient) {
        Debug.Log("Ingredient Added: " + ingredient.DisplayName + ", Cook Time: " + cookTime + ", Cook Method: " + cookOption + ", Can Ruin: " + collectTimeEnabled);
        yield return new WaitForSeconds(cookTime);
        switch (cookOption) {
            case CookOptions.Cut:
                ingredient.Cut();
                break;
            case CookOptions.Fry:
                ingredient.Fry();
                break;
            case CookOptions.Steam:
                ingredient.Steam();
                break;
        }
        Debug.Log("Cooking Complete! " + ingredient.DisplayName + " is " + ingredient.cookState + " and " + (ingredient.IsCut ? "Cut" : "Not Cut") + ". Collect your " + ingredient.DisplayName + ".");
        if (collectTimeEnabled) {
            Debug.Log("Collect Time: " + collectTime);
            yield return new WaitForSeconds(collectTime);
            ingredient.cookState = SolidIngredient.CookState.Ruined;
            Debug.Log("Oh No! " + ingredient.DisplayName + " is " + ingredient.cookState + ".");
        }
        yield return null;
    }
}
