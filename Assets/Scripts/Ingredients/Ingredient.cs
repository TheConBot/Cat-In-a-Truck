using UnityEngine;

public abstract class Ingredient : MonoBehaviour {

    virtual public string DisplayName {
        get {
            return "No Name Found!";
        }
    }
}
