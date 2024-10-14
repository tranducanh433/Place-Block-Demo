using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorManager
{
    [SerializeField] List<Slot> m_slots;

    public List<Slot> slots { get { return m_slots; } }

    public void RemoveItem(BlockData block, int amount)
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            if (m_slots[i].block == block)
            {
                m_slots[i].amount -= amount;
                if (m_slots[i].amount < 0)
                    m_slots[i].amount = 0;
                break;
            }
        }
    }

    public void AddItem(BlockData block, int amount)
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            if (m_slots[i].block == block)
            {
                m_slots[i].amount += amount;
                break;
            }
        }
    }

    public void SetAmount(BlockData block, int amount)
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            if (m_slots[i].block.blockID == block.blockID)
            {
                m_slots[i].amount = amount;
                break;
            }
        }
    }
}
