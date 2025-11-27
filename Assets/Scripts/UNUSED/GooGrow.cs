using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

//This is used to grow the small goo cube. This is the method I initially thought of to scale the player, but it worked for the cube and not the player.
public class GooGrow : MonoBehaviour
{
    public Transform gooSize;
    [SerializeField] private float xMax;
    [SerializeField] private float yMax;
    [SerializeField] private float zMax;
    public bool done = false;
    public GameObject lockPos;


    void Start()
    {
        if (gooSize == null) gooSize = this.transform;
    }

    public void StartGrow()
    {
        float x = 0;
        float y = 0;
        float z = 0;

        if (!done)
        {
            if (gooSize.localScale.x < xMax) x = 0.003f;
            else gooSize.localScale = new Vector3(xMax, gooSize.localScale.y, gooSize.localScale.z);
            if (gooSize.localScale.y < yMax) y = 0.006f;
            else gooSize.localScale = new Vector3(gooSize.localScale.x, yMax, gooSize.localScale.z);
            if (gooSize.localScale.z < zMax) z = 0.003f;
            else gooSize.localScale = new Vector3(gooSize.localScale.x, gooSize.localScale.y, zMax);

            Vector3 newScale = gooSize.localScale + new Vector3(x, y, z);
            newScale.x = Mathf.Min(newScale.x, xMax);
            newScale.y = Mathf.Min(newScale.y, yMax);
            newScale.z = Mathf.Min(newScale.z, zMax);

            gooSize.localScale = newScale;

            if(gooSize.localScale == new Vector3(xMax, yMax, zMax))
            {
                done = true;
                lockPos.GetComponent<XRSocketInteractor>().socketActive = true;
                AudioManager.Instance.PlaySound("Good Job");
            }
        }
        
    }
}
