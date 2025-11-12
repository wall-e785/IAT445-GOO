using UnityEngine;

public class WarehouseManager : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.PlaySound("warehouse-1");
        UIManager.Instance.setText("Escape the Garbage Room");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
