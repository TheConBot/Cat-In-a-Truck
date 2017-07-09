using UnityEngine;

public class TrashContainer : Container {

    public override void AddToContainer(Ingredient ingredient) {
        ingredient.transform.SetParent(transform);
        ingredient.transform.localPosition = Vector3.zero;
        ingredient.gameObject.SetActive(false);
        Debug.Log("You threw away the " + ingredient.DisplayName + ".");
    }
}
