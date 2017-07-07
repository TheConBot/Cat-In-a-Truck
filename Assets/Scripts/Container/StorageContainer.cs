using UnityEngine;

[RequireComponent(typeof(IngredientPooling))]
public class StorageContainer : Container {
    protected IngredientPooling ingredientPool;

    private void Start() {
        ingredientPool = GetComponent<IngredientPooling>();
        AddToContainer(ingredientPool.GetIngredientFromPool());
    }

    override public Ingredient TakeFromContainer() {
        var ingredient = base.TakeFromContainer();
        AddToContainer(ingredientPool.GetIngredientFromPool());
        return ingredient;
    }
}