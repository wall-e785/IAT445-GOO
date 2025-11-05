using UnityEngine;

public class BoxScale : MonoBehaviour
{

    //This script was adapted from Wallace Chau's Object Scaling Script
    public Transform boxSize;

    void Start()
    {
        if(boxSize == null) boxSize = this.transform;
    }
    //function for box to grow and reize
    public void BeginGrow()
    {
        float x = 0;
        float y = 0;
        float z = 0;

        // Only shrink if above less then maximum thresholds
        if (boxSize.localScale.x < 1f) x = 0.003f;
        if (boxSize.localScale.y < 2f) y = 0.003f;
        if (boxSize.localScale.z < 1f) z = 0.003f;

        // Clamp to prevent scaling above maximum size
        Vector3 newScale = boxSize.localScale + new Vector3(x, y, z);//save vectors based on the scale plus the new size
        newScale.x = Mathf.Min(newScale.x, 1f);
        newScale.y = Mathf.Min(newScale.y, 2f);
        newScale.z = Mathf.Min(newScale.z, 1f);

        boxSize.localScale = newScale;
    }
    //function for box to shrink and resize 
    public void BeginShrink()
    {
        float x = 0;
        float y = 0;
        float z = 0;

        // Only shrink if above minimum thresholds
        if (boxSize.localScale.x > 0.1f) x = -0.003f;
        if (boxSize.localScale.y > 0.1f) y = -0.003f;
        if (boxSize.localScale.z > 0.1f) z = -0.003f;

        Vector3 newScale = boxSize.localScale + new Vector3(x, y, z);//save vectors based on the scale plus the new size

        // Clamp to prevent shrinking below minimum size
        newScale.x = Mathf.Max(newScale.x, 0.1f);
        newScale.y = Mathf.Max(newScale.y, 0.1f);
        newScale.z = Mathf.Max(newScale.z, 0.1f);

        boxSize.localScale = newScale;//set box scale to new scale saved in vector
    }
}
