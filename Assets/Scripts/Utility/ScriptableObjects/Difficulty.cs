using UnityEngine;

[CreateAssetMenu()]
public class Difficulty : ScriptableObject {
    public int maxRecipieIngrediants = 2;
    public int recipieAmount = 5;
    public float ticketTime= 45;
    public float scoreMultiplier = 1;
}
