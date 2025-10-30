using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public float rotationSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, rotationSpeed, 0);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Destroy the collectible
            Destroy(gameObject);

            //Player grows a bit

        }


    }

}
