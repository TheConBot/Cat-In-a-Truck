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

    public SpriteRenderer cat;

    public AudioClip[] eatingSounds;
    public AudioClip[] hissingSounds;
    public AudioClip[] meowingSounds;

    [HideInInspector]
    public AudioSource audioSource;

    public System.Random rand = new System.Random();

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        foodBoat.LinkedSlip = this;
        foodBoat.gameObject.SetActive(false);
        slipMesh = GetComponent<MeshRenderer>();
        slipCollider = GetComponent<BoxCollider>();
        SetCatSprite(true);
    }

    public void TakeOrder() {
        recipie = Manager.Instance.Recipies[Random.Range(0, Manager.Instance.Recipies.Count)];
        foodBoat.Recipie = recipie;
        foodBoat.gameObject.SetActive(true);
        ticketUIView = foodBoat.GetComponentInChildren<TicketUIView>();
        ticketUIView.SetTitle(recipie.displayName);
        SetActiveSoft(false);
        StartCoroutine(TicketCountdown());
    }

    private void SetActiveSoft(bool setState) {
        slipMesh.enabled = setState;
        slipCollider.enabled = setState;
        GetComponent<GlowObjectCmd>().SetVisible(setState);
        if (setState) {
            SetCatSprite(setState);
        }
    }

    private void SetCatSprite(bool setMode) {
        if (setMode) {
            cat.sprite = Manager.Instance.catSprites[Random.Range(0, Manager.Instance.catSprites.Length)];
        }
        cat.enabled = setMode;
    }

    public IEnumerator TicketRespawn() {
        SetCatSprite(false);
        yield return new WaitForSeconds(5);
        audioSource.clip = meowingSounds[rand.Next(0, meowingSounds.Length)];
        audioSource.Play();
        SetActiveSoft(true);
    }

    public IEnumerator TicketCountdown() {
        float initialTime = Manager.Instance.DifficultySetting.ticketTime;
        ticketTimer = initialTime;
        while(ticketTimer > 0) {
            yield return new WaitForSeconds(Time.deltaTime);
            ticketUIView.SetTimer(ticketTimer, initialTime);
            ticketTimer -= Time.deltaTime;
        }

        audioSource.clip = hissingSounds[rand.Next(0, hissingSounds.Length)];
        audioSource.Play();
        foodBoat.RemoveChildrenFromPlate();
        foodBoat.gameObject.SetActive(false);
        StartCoroutine(TicketRespawn());
    }
}
