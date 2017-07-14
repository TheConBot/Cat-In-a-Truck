using UnityEngine;

public class PawMovement : MonoBehaviour {
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
    public Transform childLocation;

    public Ingredient heldIngredient { get; private set; }
    public bool HoldingIngredient {
        get {
            return heldIngredient != null;
        }
    }

    private void Start() {
        pawTransform = gameObject.GetComponent<Transform>();
        cam = Camera.main;
        pawZ = startingPawZ;
    }

    private void Update() {
        if (Manager.Instance.CurrentState == Manager.GameState.Playing) {
            // if the left mouse button is being held down
            // move the paw foward to the object it is over
            // if it's not, move the paw back to neutral
            float getPawZ = GetPawZ();
            pawZ = (Input.GetMouseButton(0)) ? getPawZ : getPawZ - neutralPawZOffset;

            // this matches the paw to the mouse cursor (generally)
            SetWorldPawPosition();

            pawArm.eulerAngles = new Vector3(-GetPawRotation('y'), GetPawRotation('x'), pawArm.rotation.z);

            // get item clicked
            if (Input.GetMouseButtonDown(0) && (getPawZ != startingPawZ)) {
                RaycastHit clickedCollider = GetClickedObject();
                if (clickedCollider.collider != null) {
                    Container clickedContainer = clickedCollider.transform.gameObject.GetComponent<Container>();
                    OrderSlip orderSlip = null;
                    if (clickedContainer != null) {
                        InteractWithContainer(clickedContainer);
                    }
                    else if((orderSlip = clickedCollider.transform.GetComponent<OrderSlip>()) != null) {
                        orderSlip.TakeOrder();
                    }
                    // click action things here!!!
                }
            }
            if (heldIngredient != null) {
              heldIngredient.gameObject.transform.position = pawTransform.position;
            }
        }
    }

    private void InteractWithContainer(Container container) {
        if (!HoldingIngredient && !container.IsContainerEmpty) {
            heldIngredient = container.TakeFromContainer();
            if (childLocation == null) {
                heldIngredient.transform.SetParent(transform);
            }
            else {
                heldIngredient.transform.SetParent(childLocation);
            }
            heldIngredient.transform.localPosition = Vector3.zero;
        }
        else if(!container.IsContainerEmpty) {
            Debug.LogWarning("Already holding " + heldIngredient.DisplayName + " and the " + container.DisplayName + " already contains " + container.GetIngredientInContainer.DisplayName + ".");
        }
        else if(HoldingIngredient) {
            if (container is CookingContainer) {
                var cookingContainer = container as CookingContainer;
                if (heldIngredient is SolidIngredient) {
                    var solidIngredient = heldIngredient as SolidIngredient;
                    if (cookingContainer.CanUseCookingContainer(solidIngredient)) {
                        cookingContainer.AddToContainer(solidIngredient);
                        heldIngredient = null;
                    }
                }
                else {
                    Debug.LogWarning("Cannot cook liquid ingredients.");
                }
            }
            else if(!(container is StorageContainer)) {
                container.AddToContainer(heldIngredient);
                heldIngredient = null;
            }
        }
    }

    private void SetWorldPawPosition() {
        // get input mouse position
        camPawPos.x = Input.mousePosition.x;
        camPawPos.y = Input.mousePosition.y;

        // this converts the mouse position to the equivalent world pos
        // the z is relative to the camera, so you have to offset it by the z position of the cam
        worldPawPos = cam.ScreenToWorldPoint(new Vector3(camPawPos.x, camPawPos.y, (pawZ + -cam.transform.position.z)));

        transform.position = worldPawPos;

      }

    private float GetPawZ() {
        RaycastHit clicked = GetClickedObject();
        if (clicked.collider != null) {
            // hey you hit something!
            return clicked.point.z;
        }
        else {
            // didn't hit anything, so just return the neutral paw Z
            return startingPawZ;
        }
    }

    private RaycastHit GetClickedObject() {
        // get the ray from the mouse to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // if the mouse is hovering over anything (within 100 units)
        if (Physics.Raycast(ray, out hit, 100)) {
            // hey you hit something!
            return hit;
        }
        else {
            return hit;
        }

    }

    private float GetPawRotation(char axis) {
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
        float o = 0;
        if (axis == 'x') {
            o = transform.position.x - pawAnchor.transform.position.x;
        } else if (axis == 'y') {
            o = transform.position.y - pawAnchor.transform.position.y;
        }

        float x = Mathf.Sin(o / h);
        x *= 100;

        return x;
    }
}
