using UnityEngine;
using System;

[CreateAssetMenu()]
public class Recipie : ScriptableObject
{
    public string displayName;
    [Range(1, 5)]
    public int occuranceFrequency = 1;
    //Change to Ingrediant Object once it's made
    private const int INGREDIENT_MAX_AMOUNT = 4;
    public Ingredient[] ingredients = new Ingredient[INGREDIENT_MAX_AMOUNT];

    void OnValidate(){
        if (ingredients.Length != INGREDIENT_MAX_AMOUNT) {
         Debug.LogWarning("Don't change the 'ingredients' field's array size!");
         Array.Resize(ref ingredients, INGREDIENT_MAX_AMOUNT);
        }
    }
}