using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

  [SerializeField]
  private GameObject objectToSpawn;
  private List<GameObject> objectsPooled;
  [SerializeField]
  private int numberToSpawn;

	private void Start () {
    Pool();
	}

  private void Pool() {
    objectsPooled = new List<GameObject>();
    for (int i = 0; i < numberToSpawn; i++) {
      GameObject obj = (GameObject)Instantiate(objectToSpawn);
      obj.SetActive(false);
      objectsPooled.Add(obj);
    }
  }
}
