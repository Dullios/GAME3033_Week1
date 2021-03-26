using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipScriptable : ItemScriptable
{
    public bool Equipped
    {
        get => m_Equipped;
        set
        {
            m_Equipped = value;
        }
    }
    private bool m_Equipped;

    public override void UseItem(PlayerController controller)
    {
        m_Equipped = !m_Equipped;
    }
}