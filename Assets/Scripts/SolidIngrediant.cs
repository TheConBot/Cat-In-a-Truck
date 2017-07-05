public class SolidIngrediant : Ingrediant {

    public bool isCut;

    public enum CookState {
        Raw,
        Fried,
        Steamed,
        Ruined
    }

    private CookState cookState;

    public virtual void Cut() {
        if (isCut) {
            return;
        }
        isCut = true;
    }

    public virtual void Fry() {
        if (cookState != CookState.Raw) {
            return;
        }
        cookState = CookState.Fried;
    }

    public virtual void Steam() {
        if (cookState != CookState.Raw) {
            return;
        }
        cookState = CookState.Steamed;
    }
}
