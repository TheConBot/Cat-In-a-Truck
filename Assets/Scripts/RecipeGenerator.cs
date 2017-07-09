using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class RecipeGenerator {

    private string savePath = "Assets/Data/Recipies";
    private int numberToGen = 10;

    private void GenerateRecipies() {
        System.Random randomAmountOfIngredients = new System.Random();

        for (int i = 0; i < numberToGen; i++) {
            System.Random randomLiquidIngredient = new System.Random();
            System.Random randomSolidIngredient = new System.Random();
            System.Random randomCutState = new System.Random();
            System.Random randomCookState = new System.Random();

            int amountOfIngredients = randomAmountOfIngredients.Next(Recipie.INGREDIENT_MAX_AMOUNT - 1, Recipie.INGREDIENT_MAX_AMOUNT);
            int liquidIngredient = randomLiquidIngredient.Next(0, Enum.GetNames(typeof(LiquidIngredient.LiquidType)).Length);

            Recipie recipe = null;

            recipe = ScriptableObject.CreateInstance<Recipie>();

            recipe.displayName = "Fish Bitch";

            // set liquid ingredient
            recipe.ingredients[amountOfIngredients - 1] = new LiquidRecipieIngredient((LiquidIngredient.LiquidType)liquidIngredient);

            // set solid ingredients
            for (int j = 0; j < amountOfIngredients - 1; j++) {
                int randSolidIngredient = randomSolidIngredient.Next(0, Enum.GetNames(typeof(SolidIngredient.SolidType)).Length);
                int randCookState = randomCookState.Next(0, 2);
                bool cut = false; ;
                if (randomCutState.Next(0, 1) == 1) {
                    cut = true;
                }
                SolidRecipieIngredient _ingredient = new SolidRecipieIngredient((SolidIngredient.SolidType)randSolidIngredient, (SolidIngredient.CookState)randCookState, cut);

                recipe.ingredients[j] = _ingredient;
            }

            Manager.instance.recipies.Add(recipe);

        }
    }




}
