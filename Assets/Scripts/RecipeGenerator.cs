using System.Collections.Generic;
using UnityEngine;
using System;

public static class RecipeGenerator {

    private static int numberToGen = 10;

    public static List<Recipie> GenerateRecipies(int ingredientsPerRecipie) {
        List<Recipie> recipies = new List<Recipie>();
        System.Random randomGenNumber = new System.Random(42069666);

        for (int i = 0; i < numberToGen; i++) {

            //int amountOfIngredients = randomGenNumber.Next(Recipie.INGREDIENT_MAX_AMOUNT - 1, Recipie.INGREDIENT_MAX_AMOUNT + 1);
            int amountOfIngredients = ingredientsPerRecipie;
            int liquidIngredient = randomGenNumber.Next(0, Enum.GetNames(typeof(LiquidIngredient.LiquidType)).Length);

            Recipie recipe = null;

            recipe = ScriptableObject.CreateInstance<Recipie>();

            recipe.displayName = "Fish Bitch";

            // set liquid ingredient
            recipe.ingredients[amountOfIngredients - 1] = new LiquidRecipieIngredient((LiquidIngredient.LiquidType)liquidIngredient);

            // set solid ingredients
            for (int j = 0; j < amountOfIngredients - 1; j++) {
                int randSolidIngredient = randomGenNumber.Next(0, Enum.GetNames(typeof(SolidIngredient.SolidType)).Length);
                int randCookState = randomGenNumber.Next(0, 3);
                bool cut = false; ;
                if (randomGenNumber.Next(0, 2) == 1) {
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
