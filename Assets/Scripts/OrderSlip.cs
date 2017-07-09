using UnityEngine;

public class OrderSlip : MonoBehaviour {
    [HideInInspector]
    public Recipie recipie;
    public GameObject foodBoat;

    private void Awake() {
        foodBoat.SetActive(false);
    }

    public void TakeOrder() {
        recipie = Manager.instance.recipies[Random.Range(0, Manager.instance.recipies.Count)];
        foodBoat.GetComponent<PlateContainer>().Recipie = recipie;
        foodBoat.gameObject.SetActive(true);
    }
}
