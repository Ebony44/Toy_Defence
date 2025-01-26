using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public GameObject mIndicateObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // on click, input system is new
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Left mouse button was pressed this frame");
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Raycast hit: " + hit.collider.name
                    + " hit pos is " + hit.point);

            }
            // get the position of the mouse
            
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 alteredPos = new Vector3(mousePosition.x, mousePosition.z, 0);

            

            Vector3 currentSnapPos = SnapToIntegerArea(alteredPos);
            Vector3 hitPos = SnapToIntegerArea(hit.point);
            currentSnapPos.y = 0.5f;
            hitPos.y = 0.5f;
            Debug.Log("Mouse position: " + mousePosition
                + " current snap position: " + currentSnapPos
                + " mouseWorldPos: " + mouseWorldPos
                + " hitPos: " + hitPos
                + " alteredPos: " + alteredPos);
            
            mIndicateObject.transform.position = hitPos;
        }

    }

    private Vector3 SnapToIntegerArea(Vector3 currentVector)
    {
        // snap to integer area
        Vector3 snappedVector = new Vector3(Mathf.Round(currentVector.x), Mathf.Round(currentVector.y), Mathf.Round(currentVector.z));
        return snappedVector;

    }
}
