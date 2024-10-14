using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedBlockData
{
    [SerializeField] string m_blockID;
    [SerializeField] float m_x;
    [SerializeField] float m_y;
    [SerializeField] float m_z;

    public string blockID { get { return m_blockID; } set { m_blockID = value; } }
    public float x { get { return m_x; } set { m_x = value; } }
    public float y { get { return m_y; } set { m_y = value; } }
    public float z { get { return m_z; } set { m_z = value; } }

    public PlacedBlockData(string blockID, float x, float y, float z)
    {
        m_blockID = blockID;
        m_x = x;
        m_y = y;
        m_z = z;
    }
}
