using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookwareContainer : MonoBehaviour {

  public GameObject placedIngredient { get; private set; }

  private int TryToPlaceIngredient(GameObject obj) {
    if (placedIngredient != null) {
      PlaceIngredient(obj);
      return 1;
    } else {
      Debug.Log("the Ingredient item " + placedIngredient.name + " is already on " + gameObject.name);
      return 0;
    }
  }

  private void PlaceIngredient(GameObject obj) {
    placedIngredient = obj;
    Vector3 pos = gameObject.transform.position;
    placedIngredient.transform.position = new Vector3(pos.x, pos.y + 1, pos.z);
  }

}
