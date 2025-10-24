using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public float shrinkFactor = 0.25f;//shrink by 25%
    public Vector3 minScale = new Vector3(0.0001f, 0.0001f, 0.0001f);//minimum size that player has to be to be shrinkable

    public float hitPoint;//hit tracker
    public float maxHit = 20;//set amount of times a player is able to get hit before dying no matter the size

    public bool canSeePlayer;
    private bool hasShrunk = false;//used to check if player has been shrunk once within being detected, limits 1 shrink per time detected


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        // hitPoint = 0;
        StartCoroutine(FOVRoutine());//constantly checks and updates
    }
        

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }


    private void FieldOfViewCheck()//checks cone infront of it for any collisions within range
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    Debug.Log("Player seen ");
                    canSeePlayer = true;
                    if (!hasShrunk && playerRef.transform.localScale.x > minScale.x && playerRef.transform.localScale.y > minScale.y && playerRef.transform.localScale.z > minScale.z)//check if the player has been shrunk before and is above min scale during initial detection, also check how many times player has been hit (if less than max number)
                    {
                        ShrinkPlayer();
                        hasShrunk = true;//set to true so not to not continiously shrunk when being continiously detected, resets when goes undetected and spotted again
                        Debug.Log("Player detected ");
                        if (hitPoint < maxHit)
                        {
                            hitPoint++;
                        }

                    }
                    else if (hitPoint >= maxHit)//else if the hit tracker number is greater than the max amount of hits, player dies
                        Debug.Log("Player died");

                }
                else
                    canSeePlayer = false;
                    hasShrunk = false;
            }
            else
                canSeePlayer = false;
                hasShrunk = false;
        }
        else if (canSeePlayer)//if can see player but is not within range
            canSeePlayer = false;
            hasShrunk = false;
    }
    
    private void ShrinkPlayer()
    {
        
        //Scale the XR Origin, controls all child scaling
        Vector3 newScale = playerRef.transform.localScale * shrinkFactor;//create new scale
        newScale = Vector3.Max(newScale, minScale); // Clamp to minimum scale
        playerRef.transform.localScale = newScale;
        Debug.Log("XR Origin scaled to: " + newScale);//check what if and how much it scaled down to

        // Adjust CharacterController, adjust the height and radius of body manually based off the shrink factor
        CharacterController cc = playerRef.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.height *= shrinkFactor;//multiplies the character controller height by shrink factor
            cc.radius *= shrinkFactor;//multiplies the character controller body radius by shrink factor
            cc.center = new Vector3(cc.center.x, cc.height / 4f, cc.center.z);
            Debug.Log($"CharacterController updated: height = {cc.height}, radius = {cc.radius}, center = {cc.center}");
        }
        else
        {
            Debug.LogWarning("CharacterController not found");
        }

        // Adjust Camera Offset height, moves the players view down as origin and the controls shrink. makes it appear smaller
        Transform cameraOffset = playerRef.transform.Find("Camera Offset");
        if (cameraOffset != null)
        {
            Vector3 offsetPos = cameraOffset.localPosition;
            offsetPos.y *= shrinkFactor;//multiply the verticle position of the camera by the shrink factor
            offsetPos.y = Mathf.Max(offsetPos.y, minScale.y); // Clamp to avoid going underground
            cameraOffset.localPosition = offsetPos;
            Debug.Log("Camera Offset height adjusted to: " + offsetPos.y);
        }
        else
        {
            Debug.LogWarning("Camera Offset not found");
        }
    }

}
