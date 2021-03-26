using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPausable
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

    // Components
    public HealthComponent Health => healthComponent;
    private HealthComponent healthComponent;

    public WeaponHolder WeaponHolder => weaponHolder;
    private WeaponHolder weaponHolder;

    private GameUIController UIController;
    private PlayerInput playerInput;

    private void Awake()
    {
        if (healthComponent == null)
            healthComponent = GetComponent<HealthComponent>();
        if (weaponHolder == null)
            weaponHolder = GetComponent<WeaponHolder>();

        UIController = FindObjectOfType<GameUIController>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnPauseGame(InputValue value)
    {
        Debug.Log("Pause Game");
        PauseManager.instance.PauseGame();
    }

    public void OnUnpauseGame(InputValue value)
    {
        Debug.Log("Unpause Game");
        PauseManager.instance.UnpauseGame();
    }

    public void PauseMenu()
    {
        UIController.EnablePauseMenu();

        playerInput.SwitchCurrentActionMap("PauseActionMap");
    }

    public void UnpauseMenu()
    {
        UIController.EnableGameMenu();

        playerInput.SwitchCurrentActionMap("PlayerActionMap");
    }
}
