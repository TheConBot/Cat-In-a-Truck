using UnityEngine;
using System;

[CreateAssetMenu()]
public class Recipie : ScriptableObject
{
    public string displayName;
    [Range(1, 5)]
    public int occuranceFrequency = 1;
    //Change to Ingrediant Object once it's made
    public const int INGREDIENT_MAX_AMOUNT = 4;
    public RecipieIngredient[] ingredients = new RecipieIngredient[INGREDIENT_MAX_AMOUNT];
}