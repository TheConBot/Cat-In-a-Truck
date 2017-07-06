using UnityEngine;
using System.Linq;
using System;

public abstract class Container : MonoBehaviour {
  protected Ingredient[] ingredientsToHold;
  [Range(1, 5), SerializeField]
  protected int containerSize;

  private void Awake() {
    ingredientsToHold = new Ingredient[containerSize];
  }

  public Ingredient PickUpIngredient(int index) {
    var ingredient = ingredientsToHold[index];
    ingredientsToHold[index] = null;
    return ingredient;
  }

  protected int IngredientsInContainer() {
    return ingredientsToHold.Count(s => s != null);
  }

  public int FirstNullIndex() {
    return Array.FindIndex(ingredientsToHold, i => i == null);
  }

  public int FirstValidIndex() {
    return Array.FindIndex(ingredientsToHold, i => i != null);
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
