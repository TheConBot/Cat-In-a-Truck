using System.Collections.Generic;
using UnityEngine;

public class PlateContainer : Container {
    [SerializeField]
    private Recipie recipie;
    private List<Ingredient> requiredIngredients;

    public Recipie Recipie {
        set {
            recipie = value;
        }
    }

    private void OnEnable()
    {
        requiredIngredients = new List<Ingredient>(recipie.ingredients);
        requiredIngredients.RemoveAll(s => s == null);
        foreach(Ingredient ingredient in requiredIngredients)
        {
            if(ingredient is SolidIngredient)
            {
                SolidIngredient solidIngredient = ingredient as SolidIngredient;
                Debug.Log("Solid Type: " + solidIngredient.GetSolidType + ", Cook State: " + solidIngredient.GetCookState + ", Is Cut: " + solidIngredient.IsCut);
            }
            else if(ingredient is LiquidIngredient)
            {
                LiquidIngredient liquidIngredient = ingredient as LiquidIngredient;
                Debug.Log("Liquid Type: " + liquidIngredient.GetLiquidType);
            }
        }
    }

    override public void AddToContainer(Ingredient ingredient)
    {
        var requiredIngredient = GetMatchingRequiredIngredient(ingredient);
        ingredient.transform.SetParent(transform);
        ingredient.transform.localPosition = Vector3.zero;
        if (requiredIngredient != null)
        {
            requiredIngredients.Remove(requiredIngredient);
            Debug.Log("Correct! " + requiredIngredients.Count + " left!");
            if (requiredIngredients.Count == 0)
            {
                Debug.Log("You got them all!");
                //TODO Cash in the plate for money!
            }
        }
        else
        {
            Debug.Log("Wrong!");
            //TODO Oops they messed up punish the player and remove the plate!
        }
    }

    private Ingredient GetMatchingRequiredIngredient(Ingredient ingredient)
    {
        foreach (var requiredIngredient in requiredIngredients)
        {
            if(ingredient is SolidIngredient && requiredIngredient is SolidIngredient)
            {
                SolidIngredient solidIngredient = ingredient as SolidIngredient;
                SolidIngredient requiredSolidIngredient = requiredIngredient as SolidIngredient;
                if(solidIngredient.GetSolidType == requiredSolidIngredient.GetSolidType && solidIngredient.GetCookState == requiredSolidIngredient.GetCookState && solidIngredient.IsCut == requiredSolidIngredient.IsCut)
                {
                    return requiredIngredient;
                }
            }
            else if(ingredient is LiquidIngredient && requiredIngredient is LiquidIngredient)
            {
                LiquidIngredient liquidIngredient = ingredient as LiquidIngredient;
                LiquidIngredient requiredLiquidIngredient = requiredIngredient as LiquidIngredient;
                if(liquidIngredient.GetLiquidType == requiredLiquidIngredient.GetLiquidType)
                {
                    return requiredIngredient;
                }
            }
        }
        return null;
    }
}
