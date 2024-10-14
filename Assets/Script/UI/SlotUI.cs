using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image m_icon;
    [SerializeField] TextMeshProUGUI m_amount;

    private Slot m_slotData;

    public Slot slotData { get { return m_slotData; } }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PlacingControl.Instance.OnStartDrag(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        PlacingControl.Instance.OnDragging(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PlacingControl.Instance.OnEndDrag(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlacingControl.Instance.OnSelectSlot(this);
    }

    public void SetDisplay(Slot slotData)
    {
        if(slotData.block == null)
        {
            m_icon.gameObject.SetActive(false);
            m_amount.gameObject.SetActive(false);
        }
        else
        {
            m_icon.gameObject.SetActive(true);
            m_amount.gameObject.SetActive(true);

            m_slotData = slotData;
            m_icon.sprite = slotData.block.uiIcon;
            m_amount.text = slotData.amount.ToString();
        }
    }
}
