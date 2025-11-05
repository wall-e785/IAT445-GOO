using UnityEngine;

//This is used to grow the small goo cube. This is the method I initially thought of to scale the player, but it worked for the cube and not the player.
public class GooGrow : MonoBehaviour
{
    public Transform gooSize;

    void Start()
    {
        if (gooSize == null) gooSize = this.transform;
    }

    public void StartGrow()
    {
        float x = 0;
        float y = 0;
        float z = 0;

        if (gooSize.localScale.x < 1f) x = 0.003f;
        if (gooSize.localScale.y < 2.6f) y = 0.003f;
        if (gooSize.localScale.z < 1f) z = 0.003f;

        Vector3 newScale = gooSize.localScale + new Vector3(x, y, z);
        newScale.x = Mathf.Min(newScale.x, 1f);
        newScale.y = Mathf.Min(newScale.y, 2.6f);
        newScale.z = Mathf.Min(newScale.z, 1f);

        gooSize.localScale = newScale;
    }
}
