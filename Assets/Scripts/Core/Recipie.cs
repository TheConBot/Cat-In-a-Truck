using System.Collections.Generic;
using System;

public class Recipie
{
    public string displayName;
    public List<RecipieIngredient> ingredients;
}

public static class RecipeGenerator {

    private static readonly string[] sauceSweetWords = { "Candied", "Charming", "Delectable", "Delicious", "Rich", "Savory", "Sweetened", "Piquant" };
    private static readonly string[] sauceSpicyWords = { "Hot", "Fiery", "Volcanic", "Blazing", "Scorching", "Sultry", "Combustible", "Fierce" };
    private static readonly string[] fishWords = { "Aquatic", "Wild", "Fine", "Squishy" };
    private static readonly string[] chickenWords = { "Free-Range", "Grazing", "Roasted", "Plucked", "Plump", "Domesticated" };
    private static readonly string[] carrotWords = { "Organic", "Crunchy", "Sweet", "Traditional" };
    private static readonly string[] broccoliWords = { "Crisp", "Healthy", "Tufted", "Fresh" };
    private static readonly string[] flavorWords = { "Grand Slam", "Nor’easter", "Chef Special", "South of the Border", "Wicked Banger", "Nice n’ Easy", "Tender Trap", "Bad News", "Colon Coaster", "El Toro", "Desperado", "Big One" };

    public static List<Recipie> GenerateRecipies(int ingredientsPerRecipie, int recipieAmount) {

        List<Recipie> recipies = new List<Recipie>();
        Random randomGenNumber = new Random();

        for (int i = 0; i < recipieAmount; i++) {

            Recipie recipie = new Recipie();
            recipie.ingredients = new List<RecipieIngredient>();
            int amountOfIngredients = ingredientsPerRecipie;

            // set solid ingredients
            for (int j = 0; j < amountOfIngredients - 1; j++) {
                int randSolidIngredient = randomGenNumber.Next(0, Enum.GetNames(typeof(SolidIngredient.SolidType)).Length);
                int randCookState = randomGenNumber.Next(0, 3);
                bool randCutState = Convert.ToBoolean(randomGenNumber.Next(0, 2));

                SolidRecipieIngredient solidIngredient = new SolidRecipieIngredient((SolidIngredient.SolidType)randSolidIngredient, (SolidIngredient.CookState)randCookState, randCutState);
                recipie.ingredients.Add(solidIngredient);
            }

            SolidRecipieIngredient sri = recipie.ingredients[0] as SolidRecipieIngredient;
            SolidIngredient.SolidType sriType = sri.GetSolidType;
            string solidNameChunk = string.Empty;
            if (sriType == SolidIngredient.SolidType.Broccoli) {
                solidNameChunk = broccoliWords[randomGenNumber.Next(0, broccoliWords.Length)];
            }
            else if (sriType == SolidIngredient.SolidType.Carrot) {
                solidNameChunk = carrotWords[randomGenNumber.Next(0, carrotWords.Length)];
            }
            else if (sriType == SolidIngredient.SolidType.Chicken) {
                solidNameChunk = chickenWords[randomGenNumber.Next(0, chickenWords.Length)];
            }
            else if (sriType == SolidIngredient.SolidType.Fish) {
                solidNameChunk = fishWords[randomGenNumber.Next(0, fishWords.Length)];
            }

            // set liquid ingredient
            int randLiquidIngredient = randomGenNumber.Next(0, Enum.GetNames(typeof(LiquidIngredient.LiquidType)).Length);
            LiquidRecipieIngredient liquidIngredient = new LiquidRecipieIngredient((LiquidIngredient.LiquidType)randLiquidIngredient);
            recipie.ingredients.Add(liquidIngredient);

            LiquidRecipieIngredient lri = recipie.ingredients[amountOfIngredients - 1] as LiquidRecipieIngredient;

            string liquidNameChunk = string.Empty;
            if (lri.GetLiquidType == LiquidIngredient.LiquidType.Sweet) {
                liquidNameChunk = sauceSweetWords[randomGenNumber.Next(0, sauceSweetWords.Length)];
            }
            else {
                liquidNameChunk = sauceSpicyWords[randomGenNumber.Next(0, sauceSpicyWords.Length)];
            }

            recipie.displayName = $"{liquidNameChunk} {solidNameChunk} {flavorWords[randomGenNumber.Next(0, flavorWords.Length)]}";

            recipies.Add(recipie);
        }
        return recipies;
    }
}