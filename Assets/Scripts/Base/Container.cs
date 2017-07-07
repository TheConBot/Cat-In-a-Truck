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
    }

    virtual public void AddToContainer(SolidIngredient ingredient) {
        if (ingredientInContainer != null) {
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
}
