using UnityEngine;

//used for the blockage of the door in the spawn room before player gets big enough
public class SizeCheck : MonoBehaviour
{
    public Transform sizeCheck;

    // Update is called once per frame
    void Update()
    {
        if (sizeCheck.localScale.y > 1.1) Destroy(this.gameObject);

    }
}
