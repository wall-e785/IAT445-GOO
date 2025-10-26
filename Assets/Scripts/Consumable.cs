using UnityEngine;

public class Consumable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    [ContextMenu("Consume")]
    public void Consume()
    {
        Destroy(gameObject);
    }
}
