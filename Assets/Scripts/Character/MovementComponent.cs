using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float jumpForce;

    // Components
    private PlayerController playerController;
    private Animator playerAnimator;
    private Rigidbody playerRigidbody;

    // References
    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;

    //Animator Hashes
    public readonly int MovementXHash = Animator.StringToHash("MovementX");
    public readonly int MovementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        playerAnimator.SetFloat(MovementXHash, inputVector.x);
        playerAnimator.SetFloat(MovementYHash, inputVector.y);
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, value.isPressed);
    }

    public void OnJump(InputValue value)
    {
        playerController.isJumping = value.isPressed;
        playerAnimator.SetBool(isJumpingHash, value.isPressed);

        playerRigidbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
    }

    private void Update()
    {
        if(playerController.isJumping)
            return;
        

        if(!(inputVector.magnitude > 0))
            moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;

        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") && playerController.isJumping)
        {
            playerController.isJumping = false;
            playerAnimator.SetBool(isJumpingHash, false);
        }
    }

    #region SubscribeToActions
    //PlayerInputActions playerActions;
    //private void Awake()
    //{
    //    playerActions = new PlayerInputActions();
    //}
    //private void Movement(InputAction.CallbackContext value)
    //{
    //    Debug.Log(value.ReadValue<Vector2>());
    //}
    //private void OnEnable()
    //{
    //    playerActions.Enable();
    //    playerActions.PlayerActionMap.Movement.performed += Movement;
    //}
    //private void OnDisable()
    //{
    //    playerActions.Disable();
    //    playerActions.PlayerActionMap.Movement.performed -= Movement;
    //}
    #endregion
}
