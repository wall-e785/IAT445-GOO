using UnityEngine;

public class ScrewShrink : MonoBehaviour
{
    public Transform screwSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (screwSize == null) screwSize = this.transform;
    }

    public void StartShrink()
    {
        float x = 0;
        float z = 0;

        if (screwSize.localScale.x > 0.09f)
        {
            x = 0.003f;
            z = 0.003f;
        }
        else
        {
            int soundToPlay = (int)Random.Range(0f, 2f);
            Debug.Log("Sound chosen: " + soundToPlay);
            if (soundToPlay == 1) AudioManager.Instance.PlaySound("Screw1");
            else AudioManager.Instance.PlaySound("Screw2");
            Destroy(this.gameObject);
        }

        Vector3 newScale = screwSize.localScale - new Vector3(x, 0, z);

        screwSize.localScale = newScale;
    }
}
