using UnityEngine;

public class SolidIngrediant : Ingrediant {

    private bool isCut;
    public bool IsCut {
        get {
            return isCut;
        }
    }
    public enum CookState {
        Raw,
        Fried,
        Steamed,
        Ruined
    }
    [HideInInspector]
    public CookState cookState;

    public virtual void Cut() {
        if (IsCut) {
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
