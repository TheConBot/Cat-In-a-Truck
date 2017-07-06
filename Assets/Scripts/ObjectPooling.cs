using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

  [SerializeField]
  private Ingredient ingredientToSpawn = null;
  private List<Ingredient> objectPool;
  [SerializeField]
  private int numberToSpawn = 5;

  public List<Ingredient> ObjectPool {
      get {
          return objectPool;
      }
  }

    private void Awake () {
      Pool();
	}

  private void Pool() {
    objectPool = new List<Ingredient>();
    for (int i = 0; i < numberToSpawn; i++) {
      Ingredient obj = Instantiate(ingredientToSpawn);
      obj.gameObject.SetActive(false);
      obj.transform.SetParent(transform);
      objectPool.Add(obj);
    }
  }
}
