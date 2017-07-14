using UnityEngine;

public class LiquidIngredient : Ingredient {
    public enum LiquidType {
        Sweet,
        Spicy
    }
    [SerializeField]
    private LiquidType liquidType;

    public LiquidType GetLiquidType {
        get {
            return liquidType;
        }
    }

    public override string DisplayName {
        get {
            return liquidType + " Sause";
        }
    }
}
