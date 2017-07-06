using UnityEngine;
using System.Linq;

public abstract class Container : MonoBehaviour {
    protected GameObject[] ingredientsToHold;
    [Range(1, 5), SerializeField]
    protected int containerSize;

    private void Awake() {
        ingredientsToHold = new GameObject[containerSize];
    }

    public GameObject PickUpIngredient(int index) {
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
