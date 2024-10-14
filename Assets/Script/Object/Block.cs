using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Collider hitbox;

    private BlockData m_blockData;

    public BlockData blockData {  get { return m_blockData; } }


    public void Init(BlockData blockData)
    {
        m_blockData = blockData;
        GetComponent<MeshRenderer>().material = blockData.blockMaterial;
    }

    public void SetHitbox(bool enable)
    {
        hitbox.enabled = enable;
    }
}
