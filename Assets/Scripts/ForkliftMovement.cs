using UnityEngine;

//used to move the fork upon activation
//modified from https://www.youtube.com/watch?v=_pApJDiFxV4
public class ForkliftMovement : MonoBehaviour
{
    private Vector3 position;
    public float speed = -.007f;
    private bool moving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = this.transform.localPosition;
    }

    public void StartMovement()
    {
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y > position.y - 1 && moving)
        {
            this.transform.localPosition += new Vector3(0, speed, 0);
        }
    }
}
