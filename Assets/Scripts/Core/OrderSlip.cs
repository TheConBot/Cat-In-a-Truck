using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OrderSlip : MonoBehaviour {
    [HideInInspector]
    public Recipie recipie;
    public PlateContainer plateContainer;
    public System.Random rand = new System.Random();

    private TicketUIView ticketUIView;
    private MeshRenderer slipMesh;
    private BoxCollider slipCollider;
    private float ticketTimer;
    private AudioSource catSoundSource;
    private IEnumerator countdown;

    public SpriteRenderer cat;

    private void Start() {
        countdown = StartSlipTimer();
        catSoundSource = GetComponent<AudioSource>();
        plateContainer.LinkedSlip = this;
        plateContainer.gameObject.SetActive(false);
        slipMesh = GetComponent<MeshRenderer>();
        slipCollider = GetComponent<BoxCollider>();
        SetCatSprite(true);
    }

    public void TakeOrder() {
        recipie = Manager.Instance.Recipies[Random.Range(0, Manager.Instance.Recipies.Count)];
        plateContainer.RequiredIngredients = recipie.ingredients;
        plateContainer.gameObject.SetActive(true);
        ticketUIView = plateContainer.GetComponentInChildren<TicketUIView>();
        ticketUIView.SetTitle(recipie.displayName);
        SetActiveSoft(false);
        StartCoroutine(countdown);
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

    public void ResetSlip() {
        StopCoroutine(countdown);
        StartCoroutine(RespawnSlip());
    }

    public void PlayCatSound(AudioClip clip) {
        catSoundSource.PlayOneShot(clip);
    }

    public IEnumerator RespawnSlip() {
        SetCatSprite(false);
        yield return new WaitForSeconds(5);
        PlayCatSound(Manager.Instance.meowingSounds[rand.Next(0, Manager.Instance.meowingSounds.Length)]);
        SetActiveSoft(true);
    }

    public IEnumerator StartSlipTimer() {
        float initialTime = Manager.Instance.DifficultySetting.ticketTime;
        ticketTimer = initialTime;
        while(ticketTimer > 0) {
            yield return new WaitForSeconds(Time.deltaTime);
            ticketUIView.SetTimer(ticketTimer, initialTime);
            ticketTimer -= Time.deltaTime;
        }
        PlayCatSound(Manager.Instance.hissingSounds[rand.Next(0, Manager.Instance.hissingSounds.Length)]);
        plateContainer.RemoveChildrenFromPlate();
        plateContainer.gameObject.SetActive(false);
        ResetSlip();
    }
}
