using UnityEngine;
using System.Linq;

public abstract class Container : MonoBehaviour {
    protected Ingredient[] ingredientsToHold;
    [Range(1, 5), SerializeField]
    protected int containerSize;

    private void Awake() {
        ingredientsToHold = new Ingredient[containerSize];
    }

    public Ingredient PickUpIngredient(int index) {
        return ingredientsToHold[index];
    }

    protected int IngredientsInContainer() {
        return ingredientsToHold.Count(s => s != null);
    }
}

[RequireComponent(typeof(ObjectPooling))]
public abstract class StorageContainer : Container {
    protected ObjectPooling ingredientPool;

    private void Start() {
        ingredientPool = GetComponent<ObjectPooling>();
        ingredientsToHold[0] = ingredientPool.ObjectPool[0];
    }
}
