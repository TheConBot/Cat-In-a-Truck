using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ingrediant : MonoBehaviour {

    private int cookQuality;
    private string displayName;

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
