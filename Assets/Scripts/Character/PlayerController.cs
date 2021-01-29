using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player States")]
    public bool isFiring;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;

    [Header("Crosshair")]
    [SerializeField]
    private CrosshairScript CrosshairComponent;
    public CrosshairScript Crosshair => CrosshairComponent;
}
