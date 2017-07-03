using UnityEngine;
using System;

[CreateAssetMenu()]
public class Recipie : ScriptableObject
{
    public string displayName;
    [Range(1, 5)]
    public int occuranceFrequency;
    //Change to Ingrediant Object once it's made
    private const int INGREDIANT_MAX_AMMOUNT = 4;
    public UnityEngine.Object[] ingrediants = new UnityEngine.Object[INGREDIANT_MAX_AMMOUNT];

    void OnValidate(){
        if (ingrediants.Length != INGREDIANT_MAX_AMMOUNT) {
         Debug.LogWarning("Don't change the 'ingrediants' field's array size!");
         Array.Resize(ref ingrediants, INGREDIANT_MAX_AMMOUNT);
        }
    }
}