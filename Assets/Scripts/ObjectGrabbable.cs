using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();

    }
    //after grabbing transform the object points to the extended camera point
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
    }

    private void FixedUpdate()
    {
        if(objectGrabPointTransform != null)
        {
            objectRigidbody.MovePosition(objectGrabPointTransform.position);
        }
    }
    
}
