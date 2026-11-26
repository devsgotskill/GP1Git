
using UnityEngine;

public class Look : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float topClamp = 90f;
    [SerializeField] private float bottomClamp = -90f;

    [Header("Head Bob Settings")]
    [SerializeField] private bool enableHeadBob = true;

    [Header("Breathing")]
    [SerializeField] private float breathAmplitude;
    [SerializeField] private float breathFrequency;
    [SerializeField] private float maxBreathAmplitude = 0.07f;
    [SerializeField] private float maxBreathFrequency = 22f;

    [Header("Walking Bob")]
    [SerializeField] private float walkAmplitude;
    [SerializeField] private float walkFrequency;

    [Header("Running Shake / Bob")]
    [SerializeField] private float runAmplitude;
    [SerializeField] private float runFrequency;
    [SerializeField] private float runShakeIntensity;

    [Header("Camera Tilt")]
    [SerializeField] private float horizontalTilt = 5f;
    [SerializeField] private float verticalTilt = 3f;
    [SerializeField] private float tiltSmoothness = 5f;

    [Header("Camera Recoil")]
    [SerializeField] private float recoilCameraMultiplier = 0.4f;
    [SerializeField] private float maxRecoilCameraMultiplier = 0.5f;
    [SerializeField] private float recoilCameraSmoothness = 13f;

    private PlayerInputActions inputActions;
    private float cameraPitch = 0f;
    private Vector3 cameraDefaultLocalPos;
    private float bobTimer = 0f;
    private float currentTiltZ;
    private float currentTiltX;
    private float currentCameraRecoil;

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

        transform.Rotate(Vector3.up * mouseX);

        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        float targetTiltZ = -moveInput.x * horizontalTilt;
        float targetTiltX = moveInput.y * verticalTilt;

        currentTiltZ = Mathf.Lerp(currentTiltZ, targetTiltZ, Time.deltaTime * tiltSmoothness);
        currentTiltX = Mathf.Lerp(currentTiltX, targetTiltX, Time.deltaTime * tiltSmoothness);

        Quaternion tiltRotation = Quaternion.Euler(currentTiltX, 0f, currentTiltZ);

        float fear = FearManager.Instance.GetFearAmount();

        float currentRecoilMultiplier = Mathf.Lerp(recoilCameraMultiplier, maxRecoilCameraMultiplier, fear);

        float recoilOffset = FearManager.Instance.GetCurrentRecoilOffset().y;
        float targetRecoil = -recoilOffset * currentRecoilMultiplier;

        currentCameraRecoil = Mathf.Lerp(currentCameraRecoil, targetRecoil, Time.deltaTime * recoilCameraSmoothness);

        cameraTransform.localRotation = Quaternion.Euler(cameraPitch + currentCameraRecoil, 0f, 0f) * tiltRotation;

        if (enableHeadBob && cameraTransform != null)
        {
            bool isMoving = moveInput.magnitude > 0.1f;
            bool isCrouching = playerMovement != null && playerMovement.IsCrouching;
            bool isSprinting = isMoving && !isCrouching && inputActions.Player.Sprint.IsPressed();

            Vector3 targetPos = cameraDefaultLocalPos;

            if (!isMoving)
            {
                float currentAmplitude = Mathf.Lerp(0.05f, maxBreathAmplitude, fear);
                float currentFrequency = Mathf.Lerp(1.5f, maxBreathFrequency, fear);

                float breathOffset = Mathf.Sin(Time.time * currentFrequency) * currentAmplitude;

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
