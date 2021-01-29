using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    public Transform GripLocation => gripIKLocation;
    [SerializeField]
    private Transform gripIKLocation;
}
