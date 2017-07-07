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

    private Ingredient lastHeldIngredient;
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
        // if the left mouse button is being held down
        // move the paw foward to the object it is over
        // if it's not, move the paw back to neutral
        float getPawZ = GetPawZ();
        pawZ = (Input.GetMouseButton(0)) ? getPawZ : getPawZ - neutralPawZOffset;

        // this matches the paw to the mouse cursor (generally)
        SetWorldPawPosition();

        // ArmRotateY();
        pawArm.eulerAngles = new Vector3(ArmRotateY(), ArmRotateX(), pawArm.rotation.z);

        // get item clicked
        if (Input.GetMouseButtonDown(0) && (getPawZ != startingPawZ)) {
            RaycastHit clickedCollider = GetClickedObject();
            Container clickedContainer = clickedCollider.transform.gameObject.GetComponent<Container>();
            if (clickedContainer != null) {
                InteractWithContainer(clickedContainer);
            }
            // click action things here!!!
        }


        if (heldIngredient != lastHeldIngredient) {
            if (heldIngredient != null) {
                heldIngredient.gameObject.SetActive(true);
                heldIngredient.gameObject.transform.position = pawTransform.position;
            }
        }
    }

    private void InteractWithContainer(Container container) {
        //Interactions with Storage Container
        if (container is StorageContainer) {
            StorageContainer storageContainer = container.GetComponent<StorageContainer>();
            if (!HoldingIngredient) {
                heldIngredient = storageContainer.TakeFromStorageContainer();
                Debug.Log("Took the " + heldIngredient.DisplayName + " from the " + container.DisplayName);
            }
            else {
                Debug.LogWarning("Already holding " + heldIngredient.DisplayName + ".");
            }
        }
        //Interactions with Cooking Container
        else if (container is CookingContainer) {
            CookingContainer cookingContainer = container.GetComponent<CookingContainer>();
            if (HoldingIngredient) {
                if (heldIngredient is SolidIngredient) {
                    SolidIngredient solidIngredient = heldIngredient.GetComponent<SolidIngredient>();
                    int canCookReturnCode = cookingContainer.CanUseCookingContainer(solidIngredient);
                    switch (canCookReturnCode) {
                        case 0:
                            cookingContainer.AddToCookingContainer(solidIngredient);
                            heldIngredient = null;
                            break;
                        case 1:
                            Debug.LogWarning(container.DisplayName + " already contains " + container.GetIngredientInContainer.DisplayName + ".");
                            break;
                        case 2:
                            Debug.LogWarning(solidIngredient.DisplayName + " has already been cut.");
                            break;
                        case 3:
                            Debug.LogWarning(solidIngredient.DisplayName + " has already been " + solidIngredient.cookState + ".");
                            break;
                    }
                }
                else {
                    Debug.LogWarning("Cannot cook liquid ingredients.");
                }
            }
            else {
                heldIngredient = cookingContainer.TakeFromCookingContainer();
                if (HoldingIngredient) {
                    Debug.Log("Took the " + heldIngredient.DisplayName + " from the " + container.DisplayName + ".");
                }
                else {
                    Debug.LogWarning(container.DisplayName + " is empty. Cannot take any ingredients.");
                }
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

        // this sets the world position of the paw
        // pawZ is being set in GetPawZ, or neutral
        // pawTransform.position = new Vector3(worldPawPos.x, worldPawPos.y, pawZ);
        pawTransform.position = worldPawPos;
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

        // ignore the eighth layer ("PawIgnore")
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        // if the mouse is hovering over anything (within 100 units)
        if (Physics.Raycast(ray, out hit, 100, layerMask)) {
            // hey you hit something!
            return hit;
        }
        else {
            return hit;
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
