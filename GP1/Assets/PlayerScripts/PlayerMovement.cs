
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float sprintSpeed = 5f;
    [SerializeField] private float crouchSpeed = 1.4f;
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpHeight = 1.4f;

    [Header("Crouching Settings")]
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float crouchCameraOffset = 1.5f;
    [SerializeField] private float crouchSmoothness = 15f;

    [Header("Advanced Jump Juice")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.15f;

    [Header("Movement Acceleration")]
    [SerializeField] private float acceleration = 9f;
    [SerializeField] private float deceleration = 14f;
    private Vector3 currentMoveVelocity;
    private CharacterController controller;
    private PlayerInputActions inputActions;

    private Vector3 velocity;
    private Vector3 standingCenter;
    private Vector3 cameraDefaultPosition;

    private float standingHeight;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private bool isGrounded;
    private bool isCrouching;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerInputActions();
        standingHeight = controller.height;
        standingCenter = controller.center;
        if (cameraTransform != null)
        {
            cameraDefaultPosition = cameraTransform.localPosition;
        }
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
    private void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (inputActions.Player.Crouch.WasPressedThisFrame())
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                controller.height = crouchHeight;
                controller.center = new Vector3(standingCenter.x, standingCenter.y - (standingHeight - crouchHeight) / 2f, standingCenter.z);
            }
            else
            {
                controller.height = standingHeight;
                controller.center = standingCenter;
            }
        }

        if (cameraTransform != null)
        {
            Vector3 targetCameraPosition = cameraDefaultPosition;
            if (isCrouching)
            {
                targetCameraPosition.y -= crouchCameraOffset;
            }
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetCameraPosition, Time.deltaTime * crouchSmoothness);
        }
        bool isSprinting = inputActions.Player.Sprint.IsPressed();
        float currentSpeed;
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else if (isSprinting)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        move = Vector3.ClampMagnitude(move, 1f);
        Vector3 targetVelocity = move * currentSpeed;
        float currentAcceleration;
        if (moveInput.magnitude > 0.1f)
        {
            currentAcceleration = acceleration;
        }
        else
        {
            currentAcceleration = deceleration;
        }
        currentMoveVelocity = Vector3.MoveTowards(currentMoveVelocity, targetVelocity, currentAcceleration * Time.deltaTime);
        controller.Move(currentMoveVelocity * Time.deltaTime);
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (inputActions.Player.Jump.WasPressedThisFrame())
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (!isCrouching && coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
