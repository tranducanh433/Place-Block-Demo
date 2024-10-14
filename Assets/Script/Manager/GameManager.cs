using System.Collections;
using System.Collections.Generic;
using UnityCommunity.UnitySingleton;
using UnityEngine;

public class GameManager : PersistentMonoSingleton<GameManager>
{
    [SerializeField] BlockData[] m_blockDatas;
    [SerializeField] InventorManager m_inventory;
    [SerializeField] List<GameObject> m_placedBlock;

    PlayerData m_playerData = new PlayerData();
    public PlayerData playerData { get { return m_playerData; } }
    public InventorManager inventory { get { return m_inventory; } }


    private void Start()
    {
        LoadSaveFile();
    }

    private void LoadSaveFile()
    {
        PlayerData playerData = SaveSystem.Load<PlayerData>("playerdata");

        if (playerData != null)
        {
            // Load Inventory
            for (int i = 0; i < playerData.slotDatas.Count; i++)
            {
                m_inventory.SetAmount(GetBlockData(playerData.slotDatas[i].id)
                                                    , playerData.slotDatas[i].amount);
            }

            //Place Block
            for (int i = 0; i < playerData.placedBlock.Count; i++)
            {
                GameObject block = PlacingControl.Instance.GetBlock();
                if (block == null)
                    break;

                BlockData blockData = GetBlockData(playerData.placedBlock[i].blockID);
                Block _blockComponent = block.GetComponent<Block>();
                _blockComponent.Init(blockData);
                _blockComponent.SetHitbox(true);

                block.transform.position = new Vector3(playerData.placedBlock[i].x
                                                        , playerData.placedBlock[i].y
                                                        , playerData.placedBlock[i].z);

                PlaceBlock(block);
            }
        }

        InventoryUI.Instance.UpdateInventorySlots();
    }

    public BlockData GetBlockData(string id)
    {
        for (int i = 0; i < m_blockDatas.Length; i++)
        {
            if (id == m_blockDatas[i].blockID)
            {
                return m_blockDatas[i];
            }
        }
        return null;
    }
    public void PlaceBlock(GameObject block)
    {
        m_placedBlock.Add(block);
    }
    public void RemoveBlock(GameObject block)
    {
        m_placedBlock.Remove(block);
    }

    public void SaveGame()
    {
        m_playerData.slotDatas.Clear();
        for (int i = 0; i < m_inventory.slots.Count; i++)
        {
            string id = m_inventory.slots[i].block.blockID;
            int amount = m_inventory.slots[i].amount;
            m_playerData.slotDatas.Add(new SlotData(id, amount));
        }

        m_playerData.placedBlock.Clear();
        for (int i = 0; i < m_placedBlock.Count; i++)
        {
            string id = m_placedBlock[i].GetComponent<Block>().blockData.blockID;
            m_playerData.placedBlock.Add(new PlacedBlockData(id
                                                            , m_placedBlock[i].transform.position.x
                                                            , m_placedBlock[i].transform.position.y
                                                            , m_placedBlock[i].transform.position.z));
        }

        SaveSystem.Save(m_playerData, "playerdata");
    }
}
