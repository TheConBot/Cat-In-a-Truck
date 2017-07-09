using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrderSlip : MonoBehaviour {
    [HideInInspector]
    public Recipie recipie;
    public PlateContainer foodBoat;

    private TicketUIView ticketUIView;

    private MeshRenderer slipMesh;
    private BoxCollider slipCollider;
    private float ticketTimer;

    private void Awake() {
        foodBoat.LinkedSlip = this;
        foodBoat.gameObject.SetActive(false);
        slipMesh = GetComponent<MeshRenderer>();
        slipCollider = GetComponent<BoxCollider>();
        ticketUIView.title = foodBoat.GetComponentInChildren<Text>();
        ticketUIView.timer = foodBoat.GetComponentInChildren<Image>();
  }

    public void TakeOrder() {
        recipie = Manager.instance.recipies[Random.Range(0, Manager.instance.recipies.Count)];
        foodBoat.Recipie = recipie;
        ticketUIView.SetTitle(recipie.displayName);
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
        float initialTime = Manager.instance.DifficultySetting.ticketTime;
        ticketTimer = initialTime;
        while(ticketTimer > 0) {
            yield return new WaitForSeconds(Time.deltaTime);
            ticketUIView.SetTimer(ticketTimer, initialTime);
            ticketTimer -= Time.deltaTime;
        }
        foodBoat.RemoveChildrenFromPlate();
        foodBoat.gameObject.SetActive(false);
    }
}
