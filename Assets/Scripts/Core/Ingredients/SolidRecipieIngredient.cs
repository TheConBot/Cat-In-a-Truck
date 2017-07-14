public class SolidRecipieIngredient : RecipieIngredient {

    public SolidIngredient.SolidType solidType;
    public SolidIngredient.CookState cookState;
    public SolidIngredient.SolidType GetSolidType {
        get {
            return solidType;
        }
    }

    public SolidIngredient.CookState GetCookState {
        get {
            return cookState;
        }
    }
    private bool isCut;
    public bool IsCut {
        get {
            return isCut;
        }
    }

    public override string DisplayName {
        get {
            return solidType.ToString();
        }
    }

    public SolidRecipieIngredient(SolidIngredient.SolidType st, SolidIngredient.CookState cs, bool ic)
    {
        solidType = st;
        cookState = cs;
        isCut = ic;
    }
}
