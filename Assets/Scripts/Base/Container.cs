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

    protected Ingredient TakeFromContainer() {
        var ingredient = ingredientInContainer;
        ingredientInContainer = null;
        return ingredient;
    }

    protected void AddToContainer(Ingredient ingredient) {
        if(ingredientInContainer != null) {
            return;
        }
        ingredientInContainer = ingredient;
    }
}

[RequireComponent(typeof(ObjectPooling))]
public abstract class StorageContainer : Container {
    protected ObjectPooling ingredientPool;

    private void Start() {
        ingredientPool = GetComponent<ObjectPooling>();
        AddToContainer(ingredientPool.ObjectPool[0]);
    }

    public Ingredient TakeFromStorageContainer() {
        return TakeFromContainer();
    }
}
