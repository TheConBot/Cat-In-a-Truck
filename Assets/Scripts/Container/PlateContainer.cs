﻿using System.Collections.Generic;
using UnityEngine;

public class PlateContainer : Container {
    [SerializeField]
    private Recipie recipie;
    private List<RecipieIngredient> requiredIngredients;
    private int scoreToGive;
    public const int SCORE_PER_INGREDIENT = 100;

    public Recipie Recipie {
        set {
            recipie = value;
        }
    }

    private void OnEnable()
    {
        requiredIngredients = new List<RecipieIngredient>(recipie.ingredients);
        requiredIngredients.RemoveAll(s => s == null);
        foreach(RecipieIngredient ingredient in requiredIngredients)
        {
            if(ingredient is SolidRecipieIngredient)
            {
                SolidRecipieIngredient solidIngredient = ingredient as SolidRecipieIngredient;
                Debug.Log("Solid Type: " + solidIngredient.GetSolidType + ", Cook State: " + solidIngredient.GetCookState + ", Is Cut: " + solidIngredient.IsCut);
            }
            else if(ingredient is LiquidRecipieIngredient)
            {
                LiquidRecipieIngredient liquidIngredient = ingredient as LiquidRecipieIngredient;
                Debug.Log("Liquid Type: " + liquidIngredient.GetLiquidType);
            }
        }
        scoreToGive = 0;
    }

    override public void AddToContainer(Ingredient ingredient)
    {
        var requiredIngredient = GetMatchingRequiredIngredient(ingredient);
        ingredient.transform.SetParent(transform);
        ingredient.transform.localPosition = Vector3.zero;
        if (requiredIngredient != null)
        {
            requiredIngredients.Remove(requiredIngredient);
            scoreToGive += SCORE_PER_INGREDIENT;
            Debug.Log("Correct! " + requiredIngredients.Count + " left!");
            if (requiredIngredients.Count == 0)
            {
                Debug.Log("You got them all!");
                Manager.instance.RoundScore += scoreToGive;
            }
        }
        else
        {
            Debug.Log("Wrong!");
            gameObject.SetActive(false);
        }
    }

    private RecipieIngredient GetMatchingRequiredIngredient(Ingredient ingredient)
    {
        foreach (var requiredIngredient in requiredIngredients)
        {
            if(ingredient is SolidIngredient && requiredIngredient is SolidRecipieIngredient)
            {
                SolidIngredient solidIngredient = ingredient as SolidIngredient;
                SolidRecipieIngredient requiredSolidIngredient = requiredIngredient as SolidRecipieIngredient;
                if(solidIngredient.GetSolidType == requiredSolidIngredient.GetSolidType && solidIngredient.GetCookState == requiredSolidIngredient.GetCookState && solidIngredient.IsCut == requiredSolidIngredient.IsCut)
                {
                    return requiredIngredient;
                }
            }
            else if(ingredient is LiquidIngredient && requiredIngredient is LiquidRecipieIngredient)
            {
                LiquidIngredient liquidIngredient = ingredient as LiquidIngredient;
                LiquidRecipieIngredient requiredLiquidIngredient = requiredIngredient as LiquidRecipieIngredient;
                if(liquidIngredient.GetLiquidType == requiredLiquidIngredient.GetLiquidType)
                {
                    return requiredIngredient;
                }
            }
        }
        return null;
    }
}
