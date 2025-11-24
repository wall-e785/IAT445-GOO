using UnityEngine;

public class WarehouseManager : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.PlaySound("warehouse-1");
        UIManager.Instance.setText("Find the Key Card to the Security Room.");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
