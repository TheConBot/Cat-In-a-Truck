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

    List<SolidIngredient> solidIngredients = new List<SolidIngredient>();
    List<LiquidIngredient> liquidIngredients = new List<LiquidIngredient>();

    foreach (Ingredient ing in ingredients) {
      if (ing is LiquidIngredient) {
        liquidIngredients.Add(ing as LiquidIngredient);
      } else {
        solidIngredients.Add(ing as SolidIngredient);
      }
    }

    for (int i = 0; i < numberToGen; i++) {
      System.Random randomLiquidIngredient = new System.Random();
      System.Random randomSolidIngredient = new System.Random();
      System.Random randomCutState = new System.Random();
      System.Random randomCookState = new System.Random();

      int amountOfIngredients = randomAmountOfIngredients.Next(3, 4);
      int liquidIngredient = randomLiquidIngredient.Next(0, liquidIngredients.Count);

      Recipie recipe = null;

      recipe = CreateInstance<Recipie>();

      recipe.displayName = "Fish Bitch";

      // set liquid ingredient
      recipe.ingredients[amountOfIngredients - 1] = liquidIngredients[liquidIngredient];

      // set solid ingredients
      for (int j = 0; j < amountOfIngredients - 1; j++) {
        SolidIngredient _ingredient = solidIngredients[randomSolidIngredient.Next(0, solidIngredients.Count)];

        if (randomCutState.Next(0, 1) == 1) {
          _ingredient.Cut();
        }

        int cookState = randomCookState.Next(0, 2);
        if (cookState == 1) {
          _ingredient.Fry();
        } else if (cookState == 2) {
          _ingredient.Steam();
        }

        recipe.ingredients[j] = _ingredient;
      }

      AssetDatabase.CreateAsset(recipe, savePath + "/" + recipe.displayName + "-" + i + ".asset");

    }
  }




}
