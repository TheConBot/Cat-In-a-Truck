using UnityEngine;

public abstract class Ingredient : MonoBehaviour {

    protected int cookQuality;
    [SerializeField]
    private string displayName;

    public string DisplayName {
        get {
            return displayName;
        }
    }

    public int CookQuality {
        get {
            return cookQuality;
        }

        set {
            cookQuality = value;
        }
    }
}
