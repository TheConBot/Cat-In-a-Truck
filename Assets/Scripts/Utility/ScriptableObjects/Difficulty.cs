using UnityEngine;

[CreateAssetMenu()]
public class Difficulty : ScriptableObject {
    public float roundTime = 2;
    public int maxRecipieIngrediants = 2;
    public int recipieAmount = 5;
    public float ticketTime= 60;
}
