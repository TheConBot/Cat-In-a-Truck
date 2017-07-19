using System.Collections.Generic;
using UnityEngine;

public class PlateContainer : Container {

    private OrderSlip linkedSlip;
    private List<RecipieIngredient> requiredIngredients;
    private int scoreToGive;
    public const int SCORE_PER_INGREDIENT = 100;
    public AudioSource addNoise;

    public OrderSlip LinkedSlip {
        set {
            linkedSlip = value;
        }
    }

    public List<RecipieIngredient> RequiredIngredients {
        set {
            requiredIngredients = value;
            if (requiredIngredients != null) {
                requiredIngredients.RemoveAll(s => s == null);
            }
        }
    }

    //private void OnEnable() {   
    //    foreach (RecipieIngredient ingredient in requiredIngredients) {
    //        if (ingredient is SolidRecipieIngredient) {
    //            SolidRecipieIngredient solidIngredient = ingredient as SolidRecipieIngredient;
    //            Debug.Log("Solid Type: " + solidIngredient.GetSolidType + ", Cook State: " + solidIngredient.GetCookState + ", Is Cut: " + solidIngredient.IsCut);
    //        }
    //        else if (ingredient is LiquidRecipieIngredient) {
    //            LiquidRecipieIngredient liquidIngredient = ingredient as LiquidRecipieIngredient;
    //            Debug.Log("Liquid Type: " + liquidIngredient.GetLiquidType);
    //        }
    //    }
    //}

    public void RemoveChildrenFromPlate() {
        foreach (Transform child in childLocation) {
                child.gameObject.SetActive(false);
                child.SetParent(null);
        }
    }

    public void ResetPlate() {
        scoreToGive = 0;
    }

    override public void AddToContainer(Ingredient ingredient) {
        var requiredIngredient = GetMatchingRequiredIngredient(ingredient);
        addNoise.Play();
        ChildIngredient(ingredient);
        if (requiredIngredient != null) {
            requiredIngredients.Remove(requiredIngredient);
            if (ingredient is LiquidIngredient) {
                ingredient.gameObject.SetActive(false);
            }
            scoreToGive += Mathf.RoundToInt((SCORE_PER_INGREDIENT + linkedSlip.TimeLeftInTicket) * Manager.Instance.DifficultySetting.scoreMultiplier);
            if (requiredIngredients.Count == 0) {
                linkedSlip.PlayCatSound(Manager.Instance.eatingSounds[linkedSlip.rand.Next(0, Manager.Instance.eatingSounds.Length)]);
                Manager.Instance.RoundScore += scoreToGive;
                RemoveChildrenFromPlate();
                linkedSlip.ResetSlip();
                gameObject.SetActive(false);
            }
        }
        else {
            linkedSlip.PlayCatSound(Manager.Instance.hissingSounds[linkedSlip.rand.Next(0, Manager.Instance.hissingSounds.Length)]);
            RemoveChildrenFromPlate();
            linkedSlip.ResetSlip();
            gameObject.SetActive(false);
        }
    }

    private RecipieIngredient GetMatchingRequiredIngredient(Ingredient ingredient) {
        foreach (var requiredIngredient in requiredIngredients) {
            if (ingredient is SolidIngredient && requiredIngredient is SolidRecipieIngredient) {
                SolidIngredient solidIngredient = ingredient as SolidIngredient;
                SolidRecipieIngredient requiredSolidIngredient = requiredIngredient as SolidRecipieIngredient;
                if (solidIngredient.GetSolidType == requiredSolidIngredient.GetSolidType && solidIngredient.GetCookState == requiredSolidIngredient.GetCookState && solidIngredient.IsCut == requiredSolidIngredient.IsCut) {
                    return requiredIngredient;
                }
            }
            else if (ingredient is LiquidIngredient && requiredIngredient is LiquidRecipieIngredient) {
                LiquidIngredient liquidIngredient = ingredient as LiquidIngredient;
                LiquidRecipieIngredient requiredLiquidIngredient = requiredIngredient as LiquidRecipieIngredient;
                if (liquidIngredient.GetLiquidType == requiredLiquidIngredient.GetLiquidType) {
                    return requiredIngredient;
                }
            }
        }
        return null;
    }
}
