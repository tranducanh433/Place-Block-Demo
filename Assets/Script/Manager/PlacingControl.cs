using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityCommunity.UnitySingleton;
using UnityEngine;

public class PlacingControl : MonoSingleton<PlacingControl>
{
    [SerializeField] LayerMask placableMask;
    [SerializeField] LayerMask blockMask;
    [SerializeField] List<GameObject> m_blocks;

    public Action<SlotUI> OnSelectSlot;
    public Action<SlotUI> OnStartDrag;
    public Action<SlotUI> OnDragging;
    public Action<SlotUI> OnEndDrag;

    private Slot m_selectedSlot;
    private Transform m_dragingObject;


    private void Start()
    {
        AddEvent();
    }

    private void Update()
    {
        CollectBlockInput();
        ExitInput();
    }

    public GameObject GetBlock()
    {
        GameObject block = m_blocks[0].gameObject;
        block.SetActive(true);
        m_blocks.RemoveAt(0);
        return block;
    }

    private void CollectBlockInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, blockMask))
            {
                BlockData _blockData = hit.transform.GetComponent<Block>().blockData;
                InventoryUI.Instance.AddItem(_blockData, 1);

                GameObject _blockGO = hit.transform.gameObject;
                _blockGO.SetActive(false);
                m_blocks.Add( _blockGO );

                GameManager.Instance.RemoveBlock(_blockGO);

            }
        }
    }

    private void ExitInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.SaveGame();
            Application.Quit();
        }
    }

    private void AddEvent()
    {
        OnSelectSlot += SelectSlot;
        OnStartDrag += StartDrag;
        OnDragging += Dragging;
        OnEndDrag += EndDrag;
    }

    private void SelectSlot(SlotUI slot)
    {
        if (slot.slotData.amount <= 0)
            return;

        m_selectedSlot = slot.slotData;
    }
    private void StartDrag(SlotUI slot)
    {
        if (m_selectedSlot == null || m_blocks.Count <= 0)
            return;

        // Get block gameObject in the block pool
        GameObject block = GetBlock();
        m_dragingObject = block.transform;
        block.GetComponent<Block>().Init(m_selectedSlot.block);
    }
    private void Dragging(SlotUI slot)
    {
        if (m_dragingObject == null)
            return;

        m_dragingObject.position = GetMousePosition();
    }
    private void EndDrag(SlotUI slot)
    {
        if (m_dragingObject == null)
            return;

        InventoryUI.Instance.RemoveItem(m_selectedSlot.block, 1);
        GameManager.Instance.PlaceBlock(m_dragingObject.gameObject);

        m_dragingObject.GetComponent<Block>().SetHitbox(true);
        m_dragingObject = null;
        m_selectedSlot = null;
    }


    private Vector3 GetMousePosition()
    {
        Vector3 result = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, placableMask))
        {
                result = new Vector3(RoundNumber(hit.point.x)
                                , RoundNumber(hit.point.y)
                                , RoundNumber(hit.point.z));
        }
        return result;
    }

    private float RoundNumber(float num)
    {
        // Get decimal fraction in a number
        float a = num % 1;

        if (Mathf.Abs(a) > 0.5f)
            return num >= 0 ? (int)num + 1 : (int)num - 1;
        return (int)num;
    }
}
