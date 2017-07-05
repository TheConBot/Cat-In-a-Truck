using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour {

  [SerializeField]
  private Transform pawAnchor;
  [SerializeField]
  private Transform pawArm;
  private Transform pawTransform;
  private Camera cam;
  private Vector2 camPawPos;
  private Vector3 worldPawPos;
  private float pawZ;
  [SerializeField]
  private float startingPawZ;
  [SerializeField]
  private float neutralPawZOffset;

  private void Start() {
    pawTransform = gameObject.GetComponent<Transform>();
    cam = Camera.main;
    pawZ = startingPawZ;
  }

  private void Update() {
    // if the left mouse button is being held down
    // move the paw foward to the object it is over
    // if it's not, move the paw back to neutral
    pawZ = (Input.GetMouseButton(0)) ? GetPawZ() : GetPawZ() - neutralPawZOffset;

    // this matches the paw to the mouse cursor (generally)
    GetWorldPawPosition();

    // ArmRotateY();
    pawArm.eulerAngles = new Vector3(ArmRotateY(), ArmRotateX(), pawArm.rotation.z);
  }

  private void GetWorldPawPosition() {
    // get input mouse position
    camPawPos.x = Input.mousePosition.x;
    camPawPos.y = Input.mousePosition.y;

    // this converts the mouse position to the equivalent world pos
    // i literally have no idea what that 10 is for, but uh leave it
    worldPawPos = cam.ScreenToWorldPoint(new Vector3(camPawPos.x, camPawPos.y, (pawZ + -cam.transform.position.z)));

    // this sets the world position of the paw
    // pawZ is being set in GetPawZ, or neutral
    // pawTransform.position = new Vector3(worldPawPos.x, worldPawPos.y, pawZ);
    pawTransform.position = worldPawPos;
  }

  private float GetPawZ() {
    // get the ray from the mouse to world
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    // ignore the eighth layer ("PawIgnore")
    int layerMask = 1 << 8;
    layerMask = ~layerMask;

    // if the mouse is clicking on anything (within 100 units)
    if (Physics.Raycast(ray, out hit, 100, layerMask)) {
      // hey you hit something! this is where most of the selection code will go
      Debug.Log(hit.transform.gameObject.name + ", point z " + hit.point.z);
      return hit.point.z;
    } else {
      // didn't hit anything, so just return the neutral paw Z
      return startingPawZ;
    }
  }

  // this might be removed later if i can find a better way to do this
  // for now, this sets the arm position to be centered between the arm anchor (behind camera)
  // and the paw itself! then, scales it to be that length. THEN has to tile the texture properly.
  private float ArmRotateY() {
    //
    //     /|
    //    /x|
    // h /  | a
    //  /___|
    //    o
    //
    // c = distance from paw to anchor (vector3)
    // o = distance between paw and anchor on the y
    // a = distance between paw and anchor on the z
    // x = angle that the arm has to be rotated

    float h = Vector3.Distance(transform.position, pawAnchor.position);
    float o = transform.position.y - pawAnchor.position.y;
    // float a = Mathf.Abs(transform.position.z - pawAnchor.position.z);

    float x = Mathf.Sin(o / h);
    x *= 100;
    // x = 90 - x;

    return -x;
  }

  private float ArmRotateX() {
    float h = Vector3.Distance(transform.position, pawAnchor.position);
    float o = transform.position.x - pawAnchor.position.x;
    // float a = Mathf.Abs(transform.position.z - pawAnchor.position.z);

    float x = Mathf.Sin(o / h);
    x *= 100;
    // x = 90 - x;

    return x;
  }
}
