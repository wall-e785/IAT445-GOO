using UnityEngine;

public class Consumer : MonoBehaviour
{

    Collider collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Consumable consumable = other.GetComponent<Consumable>();
        if(consumable != null)
        {
            consumable.Consume();
        }
    }
}
