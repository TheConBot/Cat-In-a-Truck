using UnityEngine;

public abstract class Ingrediant : MonoBehaviour {

    protected int cookQuality;
    [SerializeField]
    public string displayName;

    public string DisplayName {
        get {
            return displayName;
        }

        set {
            displayName = value;
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
