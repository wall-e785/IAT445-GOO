using UnityEngine;
using System.Collections;


public class MatchPos : MonoBehaviour
{
    public Transform myTransform;
    public Transform locomotion;
    public Transform move;
    public Transform turn;
    public Transform gravity;
    public Transform jump;


    // Update is called once per frame
    void Update()
    {
        //Vector3 targetPos = myTransform.localPosition;
        //locomotion.SetLocalPositionAndRotation(targetPos, locomotion.localRotation);
        //move.SetLocalPositionAndRotation(targetPos, move.localRotation);
        //turn.SetLocalPositionAndRotation(targetPos, turn.localRotation);
        //gravity.SetLocalPositionAndRotation(targetPos, gravity.localRotation);
        //jump.SetLocalPositionAndRotation(targetPos, jump.localRotation);

    }
}
