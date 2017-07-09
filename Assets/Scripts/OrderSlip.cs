using UnityEngine;
using System.Collections;

public class OrderSlip : MonoBehaviour {
    [HideInInspector]
    public Recipie recipie;
    public PlateContainer foodBoat;

    private MeshRenderer slipMesh;
    private BoxCollider slipCollider;
    private float ticketTimer;

    private void Awake() {
        foodBoat.LinkedSlip = this;
        foodBoat.gameObject.SetActive(false);
        slipMesh = GetComponent<MeshRenderer>();
        slipCollider = GetComponent<BoxCollider>();
    }

    public void TakeOrder() {
        recipie = Manager.instance.recipies[Random.Range(0, Manager.instance.recipies.Count)];
        foodBoat.Recipie = recipie;
        foodBoat.gameObject.SetActive(true);
        SetActiveSoft(false);
        StartCoroutine(TicketCountdown());
    }

    private void SetActiveSoft(bool setState) {
        slipMesh.enabled = setState;
        slipCollider.enabled = setState;
    }

    public IEnumerator TicketRespawn() {
        yield return new WaitForSeconds(5);
        SetActiveSoft(true);
    }

    public IEnumerator TicketCountdown() {
        ticketTimer = 30;
        while(ticketTimer > 0) {
            yield return new WaitForSeconds(Time.deltaTime);
            ticketTimer -= Time.deltaTime;
        }
        foodBoat.RemoveChildrenFromPlate();
        foodBoat.gameObject.SetActive(false);
    }
}
