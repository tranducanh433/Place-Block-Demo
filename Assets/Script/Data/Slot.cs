using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    [SerializeField] BlockData m_block;
    [SerializeField] int m_amount;

    public BlockData block { get { return m_block; } set { m_block = value; } }
    public int amount { get { return m_amount;} set { m_amount = value; } }
}
