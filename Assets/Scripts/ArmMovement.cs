using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour {

  private Transform armAnchor;
  private Camera cam;
  private Vector2 camPawPos;
  private Vector3 worldPawPos;
  private float pawZ;
  [SerializeField]
  private float startingPawZ;
  [SerializeField]
  private float neutralPawZOffset;

  private void Start() {
    armAnchor = gameObject.GetComponent<Transform>();
    cam = Camera.main;
    pawZ = startingPawZ;
  }

  private void Update() {
    pawZ = (Input.GetMouseButton(0)) ? GetPawZ() : GetPawZ() - neutralPawZOffset;

    GetWorldPawPosition();
    // armAnchor.position = pawPos;
  }

  private void GetWorldPawPosition() {
    camPawPos.x = Input.mousePosition.x;
    camPawPos.y = Input.mousePosition.y;

    worldPawPos = cam.ScreenToWorldPoint(new Vector3(camPawPos.x, camPawPos.y, 10));

    armAnchor.position = new Vector3(worldPawPos.x, worldPawPos.y, pawZ);
  }

  private float GetPawZ() {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    int layerMask = 1 << 8;
    layerMask = ~layerMask;

    if (Physics.Raycast(ray, out hit, 100, layerMask)) {
      Debug.Log(hit.transform.gameObject.name + ", point z " + hit.point.z);
      return hit.point.z;
    } else {
      return startingPawZ;
    }
  }
}
