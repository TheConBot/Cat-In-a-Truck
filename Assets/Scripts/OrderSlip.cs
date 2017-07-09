using UnityEngine;

public class OrderSlip : MonoBehaviour {
    [HideInInspector]
    public Recipie recipie;
    public GameObject foodBoat;

    private void Awake() {
        foodBoat.SetActive(false);
    }

    public void TakeOrder() {
        int rand = Random.Range(0, Manager.instance.recipies.Count);
        Debug.Log(rand);
        recipie = Manager.instance.recipies[rand];
        foodBoat.GetComponent<PlateContainer>().Recipie = recipie;
        foodBoat.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
