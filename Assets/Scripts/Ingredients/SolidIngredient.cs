using UnityEngine;

public class SolidIngredient : Ingredient {

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

    public SolidType GetSolidType {
        get {
            return solidType;
        }
    }

    public CookState GetCookState {
        get {
            return cookState;
        }
    }

    public enum SolidType
    {
        Broccoli,
        Carrot,
        Chicken,
        Fish
    }
    [SerializeField]
    private SolidType solidType;
    public enum CookState {
        Raw,
        Fried,
        Steamed,
        Ruined
    }
    [SerializeField]
    private CookState cookState;

    private void OnEnable()
    {
        cookState = CookState.Raw;
        isCut = false;
    }

    public void Cut() {
        if (IsCut) {
            return;
        }
        isCut = true;
    }

    public void Fry() {
        if (GetCookState != CookState.Raw) {
            Debug.LogError("You should not be seeing this! Trying to Fry object that you shouldnt be able to.");
            return;
        }
        cookState = CookState.Fried;
    }

    public void Steam() {
        if (GetCookState != CookState.Raw) {
            Debug.LogError("You should not be seeing this! Trying to Steam object you shouldnt be able to.");
            return;
        }
        cookState = CookState.Steamed;
    }

    public void Ruin()
    {
        cookState = CookState.Ruined;
    }

    public SolidIngredient(SolidType st)
    {
        solidType = st;
    }
}
