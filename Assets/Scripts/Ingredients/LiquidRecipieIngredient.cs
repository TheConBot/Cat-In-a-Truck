public class LiquidRecipieIngredient : RecipieIngredient {

    public LiquidIngredient.LiquidType liquidType;
    public LiquidIngredient.LiquidType GetLiquidType {
        get {
            return liquidType;
        }
    }

    public override string DisplayName {
        get {
            return liquidType + " Sause";
        }
    }

    public LiquidRecipieIngredient(LiquidIngredient.LiquidType lt) {
        liquidType = lt;
    }
}
