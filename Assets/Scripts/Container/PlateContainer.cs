using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PlateContainer : Container {
    [SerializeField]
    private Recipie recipie;
    private List<Ingredient> requiredIngredients;

    public Recipie Recipie {
        set {
            recipie = value;
        }
    }

    override public void AddToContainer(Ingredient ingredient)
    {
        var requiredIngredient = GetMatchingIngredient(ingredient);
        if (requiredIngredient != null)
        {
            requiredIngredients.Remove(requiredIngredient);
            if(requiredIngredients.Count == 0)
            {
                //TODO Cash in the plate for money!
            }
        }
        else
        {
            //TODO Oops they messed up punish the player and remove the plate!
        }
    }

    private Ingredient GetMatchingIngredient(Ingredient ingredient)
    {
        foreach (var requiredIngredient in requiredIngredients)
        {
            //This might not work, might need to compare strings..
            if (ingredient == requiredIngredient) {
                return requiredIngredient;
            }
        }
        return null;
    }
}
