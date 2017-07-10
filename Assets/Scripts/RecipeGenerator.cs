using System.Collections.Generic;
using UnityEngine;
using System;

public static class RecipeGenerator {

    private static int numberToGen = 10;

    private static string[] sauceSweetWords = { "Candied", "Charming", "Delectable", "Delicious", "Rich", "Savory", "Sweetened", "Piquant" };
    private static string[] sauceSpicyWords = { "Hot", "Fiery", "Volcanic", "Blazing", "Scorching", "Sultry", "Combustible", "Fierce" };
    private static string[] fishWords = { "Aquatic", "Wild", "Fine", "Squishy" };
    private static string[] chickenWords = { "Free-Range", "Grazing", "Roasted", "Plucked", "Plump", "Domesticated" };
    private static string[] carrotWords = { "Organic", "Crunchy", "Sweet", "Traditional" };
    private static string[] broccoliWords = { "Crisp", "Healthy", "Tufted", "Fresh" };
    private static string[] flavorWords = { "Grand Slam", "Nor’easter", "Chef Special", "South of the Border", "Wicked Banger", "Nice n’ Easy", "Tender Trap", "Bad News", "Colon Coaster", "El Toro", "Desperado", "Big One" };

    public static List<Recipie> GenerateRecipies(int ingredientsPerRecipie) {
        List<Recipie> recipies = new List<Recipie>();
        System.Random randomGenNumber = new System.Random(42069666);

        for (int i = 0; i < numberToGen; i++) {

            //int amountOfIngredients = randomGenNumber.Next(Recipie.INGREDIENT_MAX_AMOUNT - 1, Recipie.INGREDIENT_MAX_AMOUNT + 1);
            int amountOfIngredients = ingredientsPerRecipie;
            int liquidIngredient = randomGenNumber.Next(0, Enum.GetNames(typeof(LiquidIngredient.LiquidType)).Length);

            Recipie recipe = null;

            recipe = ScriptableObject.CreateInstance<Recipie>();

            string name = "";

            // set liquid ingredient
            recipe.ingredients[amountOfIngredients - 1] = new LiquidRecipieIngredient((LiquidIngredient.LiquidType)liquidIngredient);

            LiquidRecipieIngredient lri = recipe.ingredients[amountOfIngredients - 1] as LiquidRecipieIngredient;

            if ((int)lri.GetLiquidType == 0) {
              name += sauceSweetWords[randomGenNumber.Next(0, sauceSweetWords.Length)];
            } else {
              name += sauceSpicyWords[randomGenNumber.Next(0, sauceSpicyWords.Length)];
            }

            name += " ";

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

            SolidRecipieIngredient sri = recipe.ingredients[0] as SolidRecipieIngredient;

            int sriType = (int)sri.GetSolidType;

            if (sriType == 0) {
                name += broccoliWords[randomGenNumber.Next(0, broccoliWords.Length)];
            } else if (sriType == 1) {
                name += carrotWords[randomGenNumber.Next(0, carrotWords.Length)];
            } else if (sriType == 2) {
                name += chickenWords[randomGenNumber.Next(0, chickenWords.Length)];
            } else if (sriType == 3) {
                name += fishWords[randomGenNumber.Next(0, fishWords.Length)];
            }

            name += " ";

            recipe.displayName = name + flavorWords[randomGenNumber.Next(0, flavorWords.Length)];

            recipies.Add(recipe);

        }
        return recipies;
    }
}
