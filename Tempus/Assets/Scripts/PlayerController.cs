using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Singleton Declaration

    public static PlayerController playerController;

    #endregion

    #region Serialized Variables

    [SerializeField]
    private float movementSpeed = 4.5f;
    [SerializeField]
    private float horizontalMouseSensitivity = 10.0f;
    [SerializeField]
    private float jumpForce = 200.0f;
    [SerializeField]
    private float useDistance = 5.0f;

    #endregion

    #region Private Variables

    private float rotY = 0.0f; // The rotation around the Y/Horizontal axis
    private float rotX = 0.0f; // The rotation around the X/Horizontal axis 
    private bool hasJumped = true; // Stores whether or not we have jumped
    private bool mouseLocked = true; // Is the mouse currently locked to the screen?

    #endregion

    #region External References

    public Camera camera;

    #endregion

    // Use this for initialization
    void Start()
    {
        playerController = this;

        Cursor.visible = false;

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    bool invertMouse = false;

    // Update is called once per frame
    void Update()
    {
        //calls camera looking
        CameraRotation();

        //raycast for objects
        if (Input.GetKey("e"))
            CheckForInteraction();

        Movement();

        MouseLock();
    }

    //looking left and right
    void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * horizontalMouseSensitivity;

        Quaternion localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
        transform.rotation = localRotation;        
    }

    private void Movement()
    {
        //move Left
        if (Input.GetKey("a"))
        {
            if (CheckGrounded())
            {
                transform.position -= transform.right * movementSpeed * Time.deltaTime;
            }

            else transform.position -= transform.right * movementSpeed * Time.deltaTime;            
        }

        //move right
        if (Input.GetKey("d"))
        {
            if (CheckGrounded())
            {
               transform.position += transform.right * movementSpeed * Time.deltaTime;
            }

            else transform.position += transform.right * movementSpeed * Time.deltaTime;            
        }

        //move backwards
        if (Input.GetKey("s"))
        {
            if (CheckGrounded())
            {
                transform.position -= transform.forward * movementSpeed * Time.deltaTime;
            }

            else transform.position -= transform.forward * movementSpeed * Time.deltaTime;
        }

        //move forward
        if (Input.GetKey("w"))
        {
            if (CheckGrounded())
            {
                transform.position += transform.forward * movementSpeed * Time.deltaTime;
            }
            
            else transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
        
        //jump
        if (Input.GetKeyDown("space") && CheckGrounded())
        {
            this.GetComponent<Rigidbody>().AddForce((this.transform.up * jumpForce));            
            hasJumped = true;
        }    
    }

    // Performs a check to see if we can interact with the object we're looking at
    private void CheckForInteraction()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, useDistance))
        {
            if (hit.transform.tag == "Interactable")
            {
                hit.transform.gameObject.GetComponent<Interactable>().OnUsed();
            }
        }
    }
     
    // Checking if grounded for jumping
    private bool CheckGrounded()
    {
        if(Physics.Raycast(this.transform.position, -Vector3.up, 2f))
        {
            hasJumped = false;
            return true;
        }
        else return false;
    }    

    // Performs mouse locking, used mosetly for in editor testing
    private void MouseLock()
    {
        if (mouseLocked) // If the mouse is currently locked
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock it to the screen
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the mouse and unhide the cursor
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Backspace)) // Toggle locking if we press backspace
        {
            mouseLocked = !mouseLocked;
        }
    }
}


    

