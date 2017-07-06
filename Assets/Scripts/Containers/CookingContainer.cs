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
    [Range(1, 60)]
    public float cookTime, collectTime;

    public void AddToContainer(GameObject newIngredient) {
        SolidIngrediant newSolidIngredient = newIngredient.GetComponent<SolidIngrediant>();
        int ingredientAmount = IngredientsInContainer();
        if (newSolidIngredient == null) {
            Debug.LogWarning("Cannot cook a non-solid ingredient");
            return;
        }
        else if (!CanCookIngredient(newSolidIngredient)) {
            Debug.LogWarning("Cannot cook an ingredient that has already been cooked or cut.");
            return;
        }
        else if (ingredientAmount == containerSize) {
            Debug.LogWarning("Container is full, cannot add a new ingredient.");
            return;
        }
        ingredientsToHold[ingredientAmount] = newIngredient;
        StartCoroutine(CookFood(newSolidIngredient));
    }

    private bool CanCookIngredient(SolidIngrediant ingredient) {
        if(cookOption == CookOptions.Cut && ingredient.IsCut) {
            return false;
        }
        else if(ingredient.cookState != SolidIngrediant.CookState.Raw) {
            return false;
        }
        return true;
    }

    private IEnumerator CookFood(SolidIngrediant ingredient) {
        Debug.Log("Cooking Ingredient: " + ingredient.DisplayName + ", Cook Time: " + cookTime + ", Cook Method: " + cookOption);
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
        Debug.Log("Cooking Complete! Collect your " + ingredient.DisplayName);
        yield return new WaitForSeconds(collectTime);
        ingredient.cookState = SolidIngrediant.CookState.Ruined;
        Debug.Log("You burnt the " + ingredient.DisplayName);
        yield return null;
    }
}
