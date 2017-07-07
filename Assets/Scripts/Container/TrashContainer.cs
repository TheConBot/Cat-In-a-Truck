using UnityEngine;

public class TrashContainer : Container {

    public override void AddToContainer(Ingredient ingredient) {
        ingredient.gameObject.SetActive(false);
        Debug.Log("You threw away the " + ingredient.DisplayName + ".");
    }
}
