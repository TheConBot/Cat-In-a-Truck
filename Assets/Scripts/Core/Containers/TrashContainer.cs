using UnityEngine;

public class TrashContainer : Container {

    public AudioSource addNoise;

    public override void AddToContainer(Ingredient ingredient) {
        ingredient.transform.SetParent(transform);
        ingredient.transform.localPosition = Vector3.zero;
        ingredient.gameObject.SetActive(false);
        addNoise.Play();
        Debug.Log("You threw away the " + ingredient.DisplayName + ".");
    }
}
