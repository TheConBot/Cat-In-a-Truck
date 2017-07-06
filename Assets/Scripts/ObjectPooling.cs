using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

  [SerializeField]
  private Ingredient ingredientToSpawn = null;
  private List<GameObject> objectPool;
  [SerializeField]
  private int numberToSpawn = 5;

  public List<GameObject> ObjectPool {
      get {
          return objectPool;
      }
  }

    private void Awake () {
      Pool();
	}

  private void Pool() {
    objectPool = new List<GameObject>();
    for (int i = 0; i < numberToSpawn; i++) {
      GameObject obj = Instantiate(ingredientToSpawn).gameObject;
      obj.SetActive(false);
      obj.transform.SetParent(transform);
      objectPool.Add(obj);
    }
  }
}
