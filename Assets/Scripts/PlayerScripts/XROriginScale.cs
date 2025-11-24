using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class XROriginScale : MonoBehaviour
{
    public Transform xrOriginTransform;
    public Transform camera;
    public MouthTrigger mouth;

    private bool grow = false;
    private bool shrink = false;
    private float minHeight = 0.5f;
    private float maxHeight = 3f;

    //used to spawn droplets when shrunk
    public GameObject gooDropletPrefab;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        int currScene = SceneManager.GetActiveScene().buildIndex;
        if (grow)
        {
            if (currScene == 2)
            {
                GrowPlayer(.03f);
            }
            else if (currScene == 3)
            {
                GrowPlayer(.015f);
            }else if (currScene == 4) {
                GrowPlayer(.02f);
            }else
            {
                GrowPlayer(.02f);
            }
        }

        if (shrink)
        {
            if(currScene == 3)
            {
                ShrinkPlayer(.02f);
            }
        }
    }

    public void startGrow(Vector3 scaleIncrement, float delay)
    {
        grow = true;
        StartCoroutine(StopGrow(delay));
    }

    public void startShrink(Vector3 scaleIncrement, float delay)
    {
        shrink = true;
        StartCoroutine(StopShrink(delay));

        dropsToSpawn();

    }

    //check how many goo drops should spawn based on the player's eat count
    private void dropsToSpawn()
    {
        int eatCount = mouth.getEatCount();
        int maxDeduction = 3;
        int toDeduct = 0;
        while(maxDeduction > 0 && eatCount > 0)
        {
            maxDeduction--;
            toDeduct++;
        }

        Debug.Log("Removing from eat count: " + toDeduct);

        if (toDeduct >= 1)
        {
            Instantiate(gooDropletPrefab, pos1.position, pos1.rotation);
        }
        if (toDeduct >= 2)
        {
            Instantiate(gooDropletPrefab, pos2.position, pos2.rotation);
        }
        if(toDeduct == 3)
        {
            Instantiate(gooDropletPrefab, pos3.position, pos3.rotation);
        }

        mouth.subtractEatCount(toDeduct);
    }

    IEnumerator StopGrow(float delay)
    {
        yield return new WaitForSeconds(delay);
        grow = false;
    }

    IEnumerator StopShrink(float delay)
    {
        yield return new WaitForSeconds(delay);
        shrink = false;
    }

    private void GrowPlayer(float amnt)
    {

        // Adjust CharacterController, adjust the height and radius of body manually based off the shrink factor
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null && cc.height < maxHeight)
        {
            cc.height = cc.height+ amnt;//multiplies the character controller height by shrink factor
            cc.radius = cc.radius+ .001f;//multiplies the character controller body radius by shrink factor
            cc.center = new Vector3(cc.center.x, cc.height, cc.center.z);
            //Debug.Log($"CharacterController updated: height = {cc.height}, radius = {cc.radius}, center = {cc.center}");
        }
        else
        {
            Debug.LogWarning("CharacterController not found");
        }

        // Adjust Camera Offset height, moves the players view down as origin and the controls shrink. makes it appear smaller
        Transform cameraOffset = transform.Find("Camera Offset");
        if (cameraOffset != null && cc.height < maxHeight)
        {
            Vector3 offsetPos = cameraOffset.localPosition;
            offsetPos.y += amnt;//multiply the verticle position of the camera by the shrink factor
            //offsetPos.y = Mathf.Min(offsetPos.y, 2); // Clamp to avoid going underground
            cameraOffset.localPosition = offsetPos;
            //Debug.Log("Camera Offset height adjusted to: " + offsetPos.y);
        }
        else
        {
            Debug.LogWarning("Camera Offset not found");
        }
    }

    private void ShrinkPlayer(float amnt)
    {

        // Adjust CharacterController, adjust the height and radius of body manually based off the shrink factor
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null && cc.height > minHeight)
        {
            cc.height = cc.height - amnt;//multiplies the character controller height by shrink factor
            cc.radius = cc.radius -.001f;//multiplies the character controller body radius by shrink factor
            cc.center = new Vector3(cc.center.x, cc.height, cc.center.z);
            //Debug.Log($"CharacterController updated: height = {cc.height}, radius = {cc.radius}, center = {cc.center}");
        }
        else
        {
            Debug.LogWarning("CharacterController not found");
        }

        // Adjust Camera Offset height, moves the players view down as origin and the controls shrink. makes it appear smaller
        Transform cameraOffset = transform.Find("Camera Offset");
        if (cameraOffset != null && cc.height > minHeight)
        {
            Vector3 offsetPos = cameraOffset.localPosition;
            offsetPos.y -= amnt;//multiply the verticle position of the camera by the shrink factor
            //offsetPos.y = Mathf.Min(offsetPos.y, 2); // Clamp to avoid going underground
            cameraOffset.localPosition = offsetPos;
            //Debug.Log("Camera Offset height adjusted to: " + offsetPos.y);
        }
        else
        {
            Debug.LogWarning("Camera Offset not found");
        }
    }
}
