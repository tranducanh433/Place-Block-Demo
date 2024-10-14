using System.Collections;
using System.Collections.Generic;
using UnityCommunity.UnitySingleton;
using UnityEngine;

public class InventoryUI : MonoSingleton<InventoryUI>
{
    [SerializeField] SlotUI[] slotUIs;

    InventorManager inventoryData;



    public void UpdateInventorySlots()
    {
        if(inventoryData == null)
            inventoryData = GameManager.Instance.inventory;

        for (int i = 0; i < slotUIs.Length; i++)
        {
            Slot slotData = inventoryData.slots[i];
            slotUIs[i].SetDisplay(slotData);
        }
    }

    public void RemoveItem(BlockData block, int amount)
    {
        inventoryData.RemoveItem(block, amount);
        UpdateInventorySlots();
    }
    public void AddItem(BlockData block, int amount)
    {
        inventoryData.AddItem(block, amount);
        UpdateInventorySlots();
    }
}
