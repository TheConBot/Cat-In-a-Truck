using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientContainer : MonoBehaviour {

  [SerializeField]
  private GameObject ingredient;

  public GameObject PickUpIngredient() {
    return ingredient;
  }

}
