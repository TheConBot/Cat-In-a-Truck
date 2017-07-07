using UnityEngine;

public abstract class Ingredient : MonoBehaviour {

    [SerializeField]
    private string displayName;
    public string DisplayName {
        get {
            return displayName;
        }
    }
}
