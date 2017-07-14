using UnityEngine;

[RequireComponent(typeof(IngredientPooling))]
public class StorageContainer : Container {
    protected IngredientPooling ingredientPool;

    public AudioSource takeNoise;

    private void Start() {
        ingredientPool = GetComponent<IngredientPooling>();
        AddToContainer(ingredientPool.GetIngredientFromPool());
    }

    override public Ingredient TakeFromContainer() {
        var ingredient = base.TakeFromContainer();
        AddToContainer(ingredientPool.GetIngredientFromPool());
        takeNoise.Play();
        return ingredient;
    }
}