using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "My Demo/BlockData")]
public class BlockData : ScriptableObject
{
    [SerializeField] string m_blockID = "block000";
    [SerializeField] Sprite m_uiIcon;
    [SerializeField] Material m_blockMaterial;

    public string blockID { get { return m_blockID; } }
    public Sprite uiIcon { get { return m_uiIcon; } }
    public Material blockMaterial { get {  return m_blockMaterial; } }
}
