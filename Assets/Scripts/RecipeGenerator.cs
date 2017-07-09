using System.Collections.Generic;
using UnityEngine;
using System;

public static class RecipeGenerator {

    private static int numberToGen = 10;

    public static List<Recipie> GenerateRecipies() {
        List<Recipie> recipies = new List<Recipie>();
        System.Random randomAmountOfIngredients = new System.Random();

        for (int i = 0; i < numberToGen; i++) {
            System.Random randomLiquidIngredient = new System.Random();
            System.Random randomSolidIngredient = new System.Random();
            System.Random randomCutState = new System.Random();
            System.Random randomCookState = new System.Random();

            int amountOfIngredients = randomAmountOfIngredients.Next(Recipie.INGREDIENT_MAX_AMOUNT - 1, Recipie.INGREDIENT_MAX_AMOUNT);
            int liquidIngredient = randomLiquidIngredient.Next(0, Enum.GetNames(typeof(LiquidIngredient.LiquidType)).Length + 1);

            Recipie recipe = null;

            recipe = ScriptableObject.CreateInstance<Recipie>();

            recipe.displayName = "Fish Bitch";

            // set liquid ingredient
            recipe.ingredients[amountOfIngredients - 1] = new LiquidRecipieIngredient((LiquidIngredient.LiquidType)liquidIngredient);

            // set solid ingredients
            for (int j = 0; j < amountOfIngredients - 1; j++) {
                int randSolidIngredient = randomSolidIngredient.Next(0, Enum.GetNames(typeof(SolidIngredient.SolidType)).Length + 1);
                int randCookState = randomCookState.Next(0, 3);
                bool cut = false; ;
                if (randomCutState.Next(0, 2) == 1) {
                    cut = true;
                }
                SolidRecipieIngredient _ingredient = new SolidRecipieIngredient((SolidIngredient.SolidType)randSolidIngredient, (SolidIngredient.CookState)randCookState, cut);

                recipe.ingredients[j] = _ingredient;
            }

            recipies.Add(recipe);

        }
        return recipies;
    }
}
