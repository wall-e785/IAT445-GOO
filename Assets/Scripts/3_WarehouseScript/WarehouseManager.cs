using UnityEngine;

public class WarehouseManager : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.PlaySound("warehouse-1");
        UIManager.Instance.setText("Find the Mira-Goo boxes to climb the truck and find the Warehouse Keycard");
    }
}
