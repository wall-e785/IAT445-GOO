using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;

    [Header("Jump & Gravity")]
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;

    [Header("Scaling")]
    public float scaleValue = 0.3f;
    public float objectScaleStep = 0.1f; // how much object grows/shrinks per key press

    [Header("Interaction")]
    private bool cursorLocked = true;  // true = mouse look enabled

    [Header("Grab")]
    public float grabRange = 3f;
    public Transform grabPoint; // Empty GameObject in front of camera
    private GameObject grabbedObject;
    public float grabDistance = 2f; // starting grab distance
    public float minGrabDistance = 1f; // closest object pull distance
    public float maxGrabDistance = 5f; // farthest object pull distance
    public float scrollSpeed = 2f; // scroll sensitivity


    [Header("References")]
    public Camera playerCamera;

    void Update()
    {
        // Toggle cursor lock
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLocked = !cursorLocked;
            Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !cursorLocked;
        }


        HandleMovement();
        HandleLook();
        HandleScaling();
        HandleJumpAndGravity();

        if (cursorLocked && Input.GetMouseButtonDown(0))
        {
            if (grabbedObject == null)
                TryGrab();
            else
                DropObject();
            //PlaceObject();

            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f))
            {
                // check if what we hit is gum
                StickyGum gum = hit.collider.GetComponent<StickyGum>();

                if (gum != null)
                {
                    // If the gum is stuck, unstick it before grabbing
                    if (gum.IsStuck())
                    {
                        gum.Unstick();
                    }
                }

                // now grab the object as usual
                TryGrab();
            }

        }

     


        if (grabbedObject != null)
        {
            float rotationSpeed = 100f; // degrees per second

            //Scale up held object
                if (Input.GetKeyDown(KeyCode.R))
                grabbedObject.transform.localScale += Vector3.one * objectScaleStep;

            // Scale down held object
            if (Input.GetKeyDown(KeyCode.F))
            {
                grabbedObject.transform.localScale -= Vector3.one * objectScaleStep;
                // prevent negative or zero scale
                grabbedObject.transform.localScale = Vector3.Max(grabbedObject.transform.localScale, Vector3.one * 0.1f);
            }

            // Rotate around Y axis (up)
            if (Input.GetKey(KeyCode.U))
                grabbedObject.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
            if (Input.GetKey(KeyCode.I))
                grabbedObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

            // Rotate around X axis (pitch)
            if (Input.GetKey(KeyCode.J))
                grabbedObject.transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime, Space.World);
            if (Input.GetKey(KeyCode.K))
                grabbedObject.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.World);

            // Rotate around Z axis (roll)
            if (Input.GetKey(KeyCode.Z))
                grabbedObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.World);
            if (Input.GetKey(KeyCode.C))
                grabbedObject.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime, Space.World);

            Vector3 targetPos = grabPoint.position + grabPoint.forward * grabDistance;
            grabbedObject.transform.position = Vector3.Lerp(
                grabbedObject.transform.position,
                targetPos,
                Time.deltaTime * 10f
            );

            // Calculate where in front of the camera the object should be
            Vector3 newPosition = playerCamera.transform.position + playerCamera.transform.forward * grabDistance;

            // Move the object smoothly to that position
            grabbedObject.transform.position = Vector3.Lerp(
                grabbedObject.transform.position,
                newPosition,
                Time.deltaTime * 10f
            );

            //// Optional: keep the object upright
            //grabbedObject.transform.rotation = Quaternion.Lerp(
            //    grabbedObject.transform.rotation,
            //    Quaternion.identity,
            //    Time.deltaTime * 10f
            //);
        }

        // start of the distance scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            grabDistance -= scroll * scrollSpeed;
            grabDistance = Mathf.Clamp(grabDistance, minGrabDistance, maxGrabDistance);
        }
        //end of distance scroll



        


    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void HandleLook()
    {
        if (!cursorLocked) return; // skip look if cursor is visible

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.Rotate(Vector3.left * mouseY);
    }


    void HandleScaling()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Grow();
        if (Input.GetKeyDown(KeyCode.Q))
            Shrink();
    }

    void HandleJumpAndGravity()
    {
        // Jump input
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            verticalVelocity = jumpForce;

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;
        transform.position += Vector3.up * verticalVelocity * Time.deltaTime;

        // Keep player on ground if falling below
        StayOnGround();
    }

    bool IsGrounded()
    {
        // Raycast from center downward to check ground, adjust for scale
        float rayLength = (transform.localScale.y * 0.5f) + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, rayLength);
    }

    void StayOnGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f))
        {
            float playerBottomY = transform.position.y - (transform.localScale.y * 0.5f);
            float groundY = hit.point.y;

            if (playerBottomY < groundY)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    groundY + transform.localScale.y * 0.5f,
                    transform.position.z
                );
                verticalVelocity = 0f;
            }
        }
    }

    void Grow()
    {
        transform.localScale += Vector3.one * scaleValue;
        Debug.Log("GROWING: " + transform.localScale);

        //if (grabbedObject != null)
        //    grabbedObject.transform.localScale = originalObjectScale * transform.localScale.x;
        //// assumes uniform scaling on X,Y,Z
    }

    void Shrink()
    {
        transform.localScale -= Vector3.one * scaleValue;
        transform.localScale = Vector3.Max(transform.localScale, Vector3.one * 0.1f); // prevent 0 scale
        Debug.Log("SHRINKING: " + transform.localScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            Grow();
        }
    }

    void TryGrab()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * grabRange, Color.red, 0.1f);

        if (Physics.Raycast(ray, out RaycastHit hit, grabRange))
        {
            if (hit.collider.CompareTag("Grab") || hit.collider.CompareTag("Food"))
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.SetParent(grabPoint);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    //void DropObject()
    //{
    //    grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
    //    grabbedObject.transform.SetParent(null);
    //    grabbedObject = null;
    //}

    void DropObject()
    {
        if (grabbedObject != null)
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();

            if (grabbedObject.CompareTag("Food"))
            {
                //food drop normally
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            else
            {
                //other scalables stay in place
                rb.isKinematic = true;
                rb.useGravity = false;
            }
                
          

            // Reset velocity to stop flying/rolling away
            //rb.linearVelocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;

            grabbedObject.transform.parent = null;
            grabbedObject = null;
        }
    }




    //void PlaceObject()
    //{
    //    Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
    //    rb.isKinematic = true;
    //    rb.useGravity = false;
    //}

}
