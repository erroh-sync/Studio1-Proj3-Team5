using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Singleton Declaration

    public static PlayerController playerController;

    #endregion

    #region External References

    public Camera camera;

    #endregion

    public float horizontalMouseSensitivity = 10.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis    

    public GameObject[] muzzles;
    int currentMuzzleIndex = 0;
    
    GameManager gameManager;
    private Transform myTransform;
    private Vector3 playerPosition;
    public float walkSpeed = 3.0f;
    public float originalWalkSpeed = 3.0f;
    public float runSpeed = 5.0f;

    public float distanceToGround;
    public float jumpForce = 200.0f;
    
    public float liftTime = 2.0f;
    public bool hasJumped = false;    
    
    bool paused = false;
    
    CameraRotate camRot;

    BWEffect bWEffect;

    LeftElevatorDoor leftDoor;

    public GameObject target;

    int xpos = (Screen.width) / 2;
    int ypos = (Screen.height) / 2;

    // Use this for initialization
    void Start()
    {
        playerController = this;

        Cursor.visible = false;

        camRot = GetComponentInChildren<CameraRotate>();

        leftDoor = GameObject.FindGameObjectWithTag("Leftdoor").GetComponent<LeftElevatorDoor>();

        bWEffect = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BWEffect>();

        myTransform = this.transform;

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    bool invertMouse = false;

    // Update is called once per frame
    void Update()
    {
        //if the elevator doors are open then movement is activated
        if (leftDoor.activateMovement == true)
        {
            Movement();
        }

        //calls camera looking
        CameraRotation();

        //raycast for objects
        if (Input.GetKey("e"))
            CheckForInteraction();          
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

    void Movement()
    {
        //move Left
        if (Input.GetKey("a"))
        {
            if (CheckGrounded())
            {
                transform.position -= transform.right * walkSpeed * Time.deltaTime;
            }

            else transform.position -= transform.right * walkSpeed * Time.deltaTime;            
        }

        //move right
        if (Input.GetKey("d"))
        {
            if (CheckGrounded())
            {
               transform.position += transform.right * walkSpeed * Time.deltaTime;
            }

            else transform.position += transform.right * walkSpeed * Time.deltaTime;            
        }

        //move backwards
        if (Input.GetKey("s"))
        {
            if (CheckGrounded())
            {
                transform.position -= transform.forward * walkSpeed * Time.deltaTime;
            }

            else transform.position -= transform.forward * walkSpeed * Time.deltaTime;
        }

        //move forward
        if (Input.GetKey("w"))
        {
            if (CheckGrounded())
            {
                transform.position += transform.forward * walkSpeed * Time.deltaTime;
            }
            
            else transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }
        
        //jump
        if (Input.GetKeyDown("space") && CheckGrounded())
        {
            myTransform.GetComponent<Rigidbody>().AddForce((myTransform.up * jumpForce));            
            hasJumped = true;
        }        

        //start sprinting
        if (Input.GetKey("left shift") && CheckGrounded())
        {
            walkSpeed = runSpeed;
        }

        //stop sprinting
        if (Input.GetKeyUp("left shift") && CheckGrounded())
        {
            walkSpeed = originalWalkSpeed;
        }
    }

    public void CheckForInteraction()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.transform.tag == "Interactable")
            {
                hit.transform.gameObject.GetComponent<Interactable>().OnUsed();
            }
        }
    }
     
    //Checking if grounded for jumping
    public bool CheckGrounded()
    {
        if(Physics.Raycast(myTransform.position, -Vector3.up, 2f))
        {
            hasJumped = false;
            return true;
        }
        else return false;
    }    

   /*
    //DOES NOT WORK PLS FIX FOR THE SCRUBS THE THAT NEED IT?    
    bool invertingMouse()
    {
        if(camRot.verticalMouseSensitivity > 0)
        {
            camRot.verticalMouseSensitivity -= camRot.verticalMouseSensitivity - camRot.verticalMouseSensitivity;
            return (false);
        }

        else
        {
            camRot.verticalMouseSensitivity += camRot.verticalMouseSensitivity + camRot.verticalMouseSensitivity;
            return (true);
        }
    }
    */
}


    

