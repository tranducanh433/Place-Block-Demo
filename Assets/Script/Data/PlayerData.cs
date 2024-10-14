using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] List<SlotData> m_slotDatas = new List<SlotData>();
    [SerializeField] List<PlacedBlockData> m_placedBlock = new List<PlacedBlockData>();

    public List<SlotData> slotDatas { get { return m_slotDatas; } }
    public List<PlacedBlockData> placedBlock { get { return m_placedBlock; } }
}
