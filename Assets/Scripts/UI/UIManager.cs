using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

    public GameObject indicateObject;
    public LayerMask layerMask;
    // public int currentSelectedStructure = 0;
    public Collider currentSelectedCollider;
    
    [SerializeField] private InputReader mInputReader = default;
    private bool mbBottomConstructButtonPressed = false;



    private void OnEnable()
    {
        mInputReader.attackEvent += SetStructurePosOnClick;
        mInputReader.construct1KeyPressEvent += OnConstruct1KeyPressed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // on click, input system is new
        if (Mouse.current.leftButton.wasPressedThisFrame) // TODO: replace to Gameinput's event
        {
            Debug.Log("Left mouse button was pressed this frame");
            SetStructurePosOnClick();
        }

    }

    // TODO: replace to Gameinput's event
    private void OnBottomConstructClicked()
    {
        // fire event when 1 to 9 key is pressed?

        // currentSelectedStructure = 0;
        Debug.Log("Bottom construct clicked");

    }

    private void OnConstruct1KeyPressed()
    {
        Debug.Log("Construct 1 key pressed, previously pressed? " + mbBottomConstructButtonPressed);
        if(mbBottomConstructButtonPressed)
        {
            OnCancelConstructKeyPressed();
        }
        else
        {
            mbBottomConstructButtonPressed = true;
            StartCoroutine(SetConstructTemporaryPosition());
        }
        

    }
    private void OnCancelConstructKeyPressed()
    {
        Debug.Log("[OnCancelConstructKeyPressed]");
        mbBottomConstructButtonPressed = false;
    }

    private IEnumerator SetConstructTemporaryPosition()
    {
        // set the position of the temporary structure to the mouse position
        // Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        while(mbBottomConstructButtonPressed)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = Camera.main.gameObject.transform.position.y; // axis changed
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            mouseWorldPos.y = 0.5f;
            Vector3 currentSnapPos = SnapToIntegerArea(mouseWorldPos);
            currentSnapPos.y = 0.5f;
            //Debug.Log("Mouse position: " + mousePosition
            //    + " current snap position: " + currentSnapPos
            //    + " mouseWorldPos: " + mouseWorldPos);

            indicateObject.transform.position = currentSnapPos;

            yield return null;
        }
        Debug.Log("[SetConstructTemporaryPosition] coroutine ended");

    }

    private void SetStructurePosOnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        // ray cast with layermask filtered
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Raycast hit: " + hit.collider.name
                + " hit pos is " + hit.point);
        }

        // Physics.Raycast(ray, out RaycastHit hit,);

        //if (Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    Debug.Log("Raycast hit: " + hit.collider.name
        //        + " hit pos is " + hit.point);

        //}

        // get the position of the mouse

        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Camera.main.gameObject.transform.position.y; // axis changed
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPos.y = 0.5f;
        // Vector3 alteredPos = new Vector3(mousePosition.x, mousePosition.z, 0);

        Vector3 currentSnapPos = SnapToIntegerArea(mouseWorldPos);
        Vector3 hitPos = SnapToIntegerArea(hit.point);
        currentSnapPos.y = 0.5f;
        hitPos.y = 0.5f;
        Debug.Log("Mouse position: " + mousePosition
            + " current snap position: " + currentSnapPos
            + " mouseWorldPos: " + mouseWorldPos
            + " hitPos: " + hitPos);

        // indicateObject.transform.position = hitPos;
        indicateObject.transform.position = currentSnapPos;

    }

    private void CheckColliderCollideOthers()
    {
        // check if the collider is colliding with other colliders
        Collider[] hitColliders = Physics.OverlapBox(currentSelectedCollider.bounds.center, currentSelectedCollider.bounds.extents, currentSelectedCollider.transform.rotation, layerMask);
        if (hitColliders.Length > 1)
        {
            Debug.Log("Collider is colliding with other colliders");
        }

    }

    private Vector3 SnapToIntegerArea(Vector3 currentVector)
    {
        // snap to integer area
        Vector3 snappedVector = new Vector3(Mathf.Round(currentVector.x), Mathf.Round(currentVector.y), Mathf.Round(currentVector.z));
        return snappedVector;

    }
}
