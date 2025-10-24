using UnityEngine;

public class StickyGum : MonoBehaviour
{
    private Rigidbody rb;
    private bool isStuck = true; // Gum starts stuck
    private bool hasBeenGrabbed = false; // checks interaction
    private Transform stuckTo;  // surface

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StickInPlace(); // Start stuck under table
    }

    void StickInPlace()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void Unstick()
    {
        // flag first grab, apply physics
        if (!hasBeenGrabbed)
        {
            hasBeenGrabbed = true;
        }

        isStuck = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        transform.SetParent(null);
    }

    public bool IsStuck() => isStuck;

    // Called once on Start (the gum is under the table)
    public void AttachToSurface(Transform surface)
    {
        stuckTo = surface;
        transform.SetParent(surface);
    }
}
