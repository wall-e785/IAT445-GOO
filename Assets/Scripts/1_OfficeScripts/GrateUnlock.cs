using UnityEngine;

public class GrateUnlock : MonoBehaviour
{
    public GameObject screw1;
    public GameObject screw2;
    public GameObject screw3;
    public GameObject screw4;
    public GameObject sceneTrigger;
    
    private bool unlocked = false;

    void Update()
    {
        if(screw1 == null && screw2 == null && screw3 == null && screw4 == null && !unlocked)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.None;
            }

            sceneTrigger.SetActive(true);
            unlocked = true;
        }
    }
}
