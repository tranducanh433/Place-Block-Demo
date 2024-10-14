using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotData
{
    [SerializeField] string m_id;
    [SerializeField] int m_amount;

    public string id { get { return m_id; } set { m_id = value; } }
    public int amount { get { return m_amount; } set { m_amount = value; } }

    public SlotData(string id, int amount)
    {
        m_id = id;
        m_amount = amount;
    }
}
