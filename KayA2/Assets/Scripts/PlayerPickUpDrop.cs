using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;//for transforming the raycast position to the camera instead of player origin
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    

    private void Grab()
    {
        Debug.Log("E key pressed via Input System");

        float pickupDistance = 5f;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit hit, pickupDistance, pickUpLayerMask))
        {
            Debug.Log(hit.transform);
            if (hit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable))
            {
                objectGrabbable.Grab(objectGrabPointTransform);
                Debug.Log(objectGrabbable);
            }
        }
    }

}
