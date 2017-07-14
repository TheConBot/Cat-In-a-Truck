using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IngredientPooling : MonoBehaviour {

    [SerializeField]
    private Ingredient ingredientToSpawn = null;
    private List<Ingredient> ingredientPool;
    [SerializeField, Range(1, 10)]
    private int numberToSpawn = 5;

    public List<Ingredient> IngredientPool {
        get {
            return ingredientPool;
        }
    }

    private void Awake() {
        Pool();
    }

    public Ingredient GetIngredientFromPool() {
        Ingredient inactiveIngredient = IngredientPool.FirstOrDefault(s => !s.gameObject.activeSelf);
        if (inactiveIngredient == null) {
            Debug.LogWarning("Object Pool is empty. Repooling...");
            Pool();
            inactiveIngredient = IngredientPool[0];
        }
        return inactiveIngredient;
    }

    private void Pool() {
        ingredientPool = new List<Ingredient>();
        for (int i = 0; i < numberToSpawn; i++) {
            Ingredient ingredient = Instantiate(ingredientToSpawn);
            ingredient.gameObject.SetActive(false);
            ingredient.transform.SetParent(transform);
            ingredientPool.Add(ingredient);
        }
    }
}
