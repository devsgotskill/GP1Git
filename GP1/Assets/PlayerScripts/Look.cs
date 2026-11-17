using UnityEngine;

public class Look : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float topClamp = 90f;
    [SerializeField] private float bottomClamp = -90f;
    [Header("Head Bob Settings")]
    [SerializeField] private bool enableHeadBob = true;
    [Header("Idle Breathing")]
    [SerializeField] private float breathAmplitude;
    [SerializeField] private float breathFrequency;
    [Header("Walking Bob")]
    [SerializeField] private float walkAmplitude;
    [SerializeField] private float walkFrequency;
    [Header("Running Shake / Bob")]
    [SerializeField] private float runAmplitude;
    [SerializeField] private float runFrequency;
    [SerializeField] private float runShakeIntensity;
    private PlayerInputActions inputActions;
    private float cameraPitch = 0f;
    private Vector3 cameraDefaultLocalPos;
    private float bobTimer = 0f;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (cameraTransform != null)
        {
            cameraDefaultLocalPos = cameraTransform.localPosition;
        }
    }
    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();
    private void LateUpdate()
    {
        Vector2 lookInput = inputActions.Player.Look.ReadValue<Vector2>();
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, bottomClamp, topClamp);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        if (enableHeadBob && cameraTransform != null)
        {
            Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
            bool isMoving = moveInput.magnitude > 0.1f;
            bool isSprinting = isMoving && inputActions.Player.Sprint.IsPressed();
            Vector3 targetPos = cameraDefaultLocalPos;
            if (!isMoving)
            {
                float breathOffset = Mathf.Sin(Time.time * breathFrequency) * breathAmplitude;
                targetPos += new Vector3(0f, breathOffset, 0f);
            }
            else
            {
                float currentFreq = isSprinting ? runFrequency : walkFrequency;
                float currentAmp = isSprinting ? runAmplitude : walkAmplitude;
                bobTimer += Time.deltaTime * currentFreq;
                float bobY = Mathf.Sin(bobTimer) * currentAmp;
                float bobX = Mathf.Cos(bobTimer * 0.5f) * currentAmp * 0.5f;
                targetPos += new Vector3(bobX, bobY, 0f);
                if (isSprinting)
                {
                    float shakeX = (Random.value - 0.5f) * runShakeIntensity;
                    float shakeY = (Random.value - 0.5f) * runShakeIntensity;
                    targetPos += new Vector3(shakeX, shakeY, 0f);
                }
            }
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetPos, Time.deltaTime * 12f);
        }
    }
}