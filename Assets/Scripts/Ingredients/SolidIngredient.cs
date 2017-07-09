using UnityEngine;

public class SolidIngredient : Ingredient {

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
    //[HideInInspector]
    public CookState cookState = CookState.Raw;

    public virtual void Cut() {
        if (IsCut) {
            return;
        }
        isCut = true;
    }

    public virtual void Fry() {
        if (cookState != CookState.Raw) {
            Debug.LogError("You should not be seeing this! Trying to Fry object that you shouldnt be able to.");
            return;
        }
        cookState = CookState.Fried;
    }

    public virtual void Steam() {
        if (cookState != CookState.Raw) {
            Debug.LogError("You should not be seeing this! Trying to Steam object you shouldnt be able to.");
            return;
        }
        cookState = CookState.Steamed;
    }

    private void OnEnable() {
        cookState = CookState.Raw;
        isCut = false;
    }
}
