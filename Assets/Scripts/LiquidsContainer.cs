using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidsContainer : MonoBehaviour {

    private float currentStirTime;
    public float GetStirTime {
        get {
            return currentStirTime;
        }
    }
    public enum StirState {
        Stirred,
        Ruined
    }
    [HideInInspector]
    public StirState stirState;
    [SerializeField]
    private float stirTime = 30;

    public virtual void Stir() {
        currentStirTime = stirTime;
    }
}
