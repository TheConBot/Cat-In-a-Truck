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
    [SerializeField]
    private GameObject wholeObject, cutObject;
    public Texture wholeRawTex, wholeFriedTex, wholeSteamedTex;
    public Texture cutRawTex, cutFriedTex, cutSteamedTex;
    public GameObject[] wholeTexChange, cutTexChange;

    private void OnEnable()
    {
        cookState = CookState.Raw;
        isCut = false;
        wholeObject.SetActive(true);
        cutObject.SetActive(false);
        SetTextures();
        SetColors(Color.white);
    }

    private void SetTextures() {
        Texture whole = null;
        Texture cut = null;
        switch (cookState) {
            case CookState.Fried:
                whole = wholeFriedTex;
                cut = cutFriedTex;
                break;
            case CookState.Steamed:
                whole = wholeSteamedTex;
                cut = cutSteamedTex;
                break;
            default:
                whole = wholeRawTex;
                cut = cutRawTex;
                break;
        }

        foreach (GameObject go in wholeTexChange) {
            go.GetComponent<Renderer>().material.mainTexture = whole;
        }
        foreach(GameObject go in cutTexChange) {
            go.GetComponent<Renderer>().material.mainTexture = cut;
        }
    }
    
    private void SetColors(Color color) {
        foreach (GameObject go in wholeTexChange) {
            go.GetComponent<Renderer>().material.color = color;
        }
        foreach (GameObject go in cutTexChange) {
            go.GetComponent<Renderer>().material.color = color;
        }
    }

    public void Cut() {
        if (IsCut) {
            return;
        }
        isCut = true;
        wholeObject.SetActive(false);
        cutObject.SetActive(true);
    }

    public void Fry() {
        if (GetCookState != CookState.Raw) {
            Debug.LogError("You should not be seeing this! Trying to Fry object that you shouldnt be able to.");
            return;
        }
        cookState = CookState.Fried;
        SetTextures();
    }

    public void Steam() {
        if (GetCookState != CookState.Raw) {
            Debug.LogError("You should not be seeing this! Trying to Steam object you shouldnt be able to.");
            return;
        }
        cookState = CookState.Steamed;
        SetTextures();
    }

    public void Ruin()
    {
        cookState = CookState.Ruined;
        SetColors(Color.black);
    }

    public SolidIngredient(SolidType st)
    {
        solidType = st;
    }
}
