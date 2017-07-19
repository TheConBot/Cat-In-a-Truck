using UnityEngine;

public abstract class Container : MonoBehaviour {
    private Ingredient ingredientInContainer;
    public Ingredient GetIngredientInContainer {
        get {
            return ingredientInContainer;
        }
    }
    public bool IsContainerEmpty {
        get {
            return ingredientInContainer == null;
        }
    }
    [SerializeField]
    private string displayName;

    public string DisplayName {
        get {
            return displayName;
        }
    }

    public Transform childLocation;

    virtual public Ingredient TakeFromContainer() {
        var ingredient = ingredientInContainer;
        ingredientInContainer = null;
        ingredient.gameObject.SetActive(true);
        Debug.Log("Took the " + ingredient.DisplayName + " from the " + DisplayName);
        return ingredient;
    }

    virtual public void AddToContainer(Ingredient ingredient) {
        if(ingredientInContainer != null) {
            return;
        }
        ingredientInContainer = ingredient;
        ChildIngredient(ingredient);
    }

    protected void ChildIngredient(Ingredient ingredient) {
        if (childLocation == null) {
            ingredient.transform.SetParent(transform);
        }
        else {
            ingredient.transform.SetParent(childLocation);
        }
        ingredient.transform.localPosition = Vector3.zero;
    }
}
