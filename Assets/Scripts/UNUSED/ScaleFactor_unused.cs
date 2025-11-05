using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScaleFactor_unused : MonoBehaviour
{

    public float scaleMultiplier = 1.0f;

    // Update is called once per frame
    [ContextMenu("Scale")]
    public void Scale()
    {
        transform.localScale *= scaleMultiplier;
    }
   
}
