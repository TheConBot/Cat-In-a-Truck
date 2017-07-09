using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class RecipeGenerator : EditorWindow {

  private string savePath = "Assets/Data/Recipies";
  private int numberToGen = 10;
  private int numberOfIngredients = 1;
  [SerializeField]
  private Ingredient[] ingredients = new Ingredient[1];

  [MenuItem("Tools/Create Recipies")]
  private static void ShowRecipeWindow() {
    GetWindow<RecipeGenerator>(false, "Item Generator", true);
  }

  private void OnGUI() {
    GUILayout.Label("Options", EditorStyles.boldLabel);
    numberToGen = EditorGUILayout.IntField("Number to Generate", numberToGen);
    numberOfIngredients = EditorGUILayout.IntField("Number of Ingredients", numberOfIngredients);
    if (numberOfIngredients != ingredients.Length) {
      ingredients = new Ingredient[numberOfIngredients];
    }
    for (int k = 0; k < numberOfIngredients; k++) {
      ingredients[k] = (Ingredient)EditorGUILayout.ObjectField(ingredients[k], typeof (Ingredient), false);
    }
    savePath = EditorGUILayout.TextField("Items Folder Path", savePath);
    GUILayout.FlexibleSpace();
    if (GUILayout.Button("Generate Recipies")) {
      GenerateRecipies();
    }
  }

  private void GenerateRecipies() {
    System.Random randomAmountOfIngredients = new System.Random();

    List<Ingredient> solidIngredients = new List<Ingredient>();
    List<Ingredient> liquidIngredients = new List<Ingredient>();

    foreach (Ingredient ing in ingredients) {
      if (ing is LiquidIngredient) {
        liquidIngredients.Add(ing);
      } else {
        solidIngredients.Add(ing);
      }
    }

    for (int i = 0; i < numberToGen; i++) {

      Recipie recipe = null;

      recipe = CreateInstance<Recipie>();

      recipe.displayName = "Fish Bitch";

      int amountOfIngredients = randomAmountOfIngredients.Next(3, 4);

      System.Random randomLiquidIngredient = new System.Random();
      int liquidIngredient = randomLiquidIngredient.Next(0, liquidIngredients.Count);

      recipe.ingredients[amountOfIngredients - 1] = liquidIngredients[liquidIngredient];

      System.Random randomSolidIngredient = new System.Random();
      for (int j = 0; j < amountOfIngredients - 1; j++) {
        recipe.ingredients[j] = solidIngredients[randomSolidIngredient.Next(0, solidIngredients.Count)];
      }

      AssetDatabase.CreateAsset(recipe, savePath + "/" + recipe.displayName + "-" + i + ".asset");

    }
  }




}
