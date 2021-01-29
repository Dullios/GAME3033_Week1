using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("Weapon To Spawn"), SerializeField]
    private GameObject weaponToSpawn;

    [SerializeField]
    private Transform weaponSocketLocation;

    private Transform gripIKLocation;

    // Components
    private PlayerController playerController;
    private CrosshairScript playerCrosshair;
    private Animator playerAnimator;

    public readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");
    public readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
    public readonly int IsFiringHash = Animator.StringToHash("IsFiring");
    public readonly int IsReloadingHash = Animator.StringToHash("IsReloading");

    // Ref
    private Camera viewCamera;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        if(playerController)
        {
            playerCrosshair = playerController.Crosshair;
        }

        viewCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.position, weaponSocketLocation.rotation, weaponSocketLocation);
        if(spawnedWeapon)
        {
            WeaponComponent weapon = spawnedWeapon.GetComponent<WeaponComponent>();
            if(weapon)
            {
                gripIKLocation = weapon.GripLocation;
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKLocation.position);
    }

    public void OnReload(InputValue pressed)
    {
        playerAnimator.SetBool(IsReloadingHash, pressed.isPressed);
    }

    public void OnFire(InputValue pressed)
    {
        playerAnimator.SetBool(IsFiringHash, pressed.isPressed);
    }

    public void OnLook(InputValue delta)
    {
        Vector2 independentMousePosition = viewCamera.ScreenToViewportPoint(playerCrosshair.CurrentAimPosition);
        //Debug.Log(independentMousePosition);

        playerAnimator.SetFloat(AimHorizontalHash, independentMousePosition.x);
        playerAnimator.SetFloat(AimVerticalHash, independentMousePosition.y);
    }
}
