using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float _currentBatteryLife = 1.0f;
    [SerializeField] private float _maxBatteryLife = 1.0f;
    [SerializeField] private float _batteryPickupAmount = 0.5f;
    [SerializeField] private float _batteryDrainSpeed = 1.0f;
    [SerializeField] private float _fearReductionSpeedMultiplier = 25.0f;
    [SerializeField] private float _fearIncreaseSpeedMultiplier = 25.0f;

    [SerializeField] private Light _lightSource;

    private FlashlightActive _lightSourceActive;
    private enum FlashlightActive
    {
        On,
        Off,
    }

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    void Start()
    {
        _currentBatteryLife = _maxBatteryLife;
        _lightSourceActive = FlashlightActive.Off;
        _lightSource.enabled = false;
    }

    public void AddBattery()
    {
        _currentBatteryLife += _batteryPickupAmount;
        if (_currentBatteryLife > _maxBatteryLife)
            _currentBatteryLife = _maxBatteryLife;
    }

    public void UpdateFlashlight()
    {
        if (_lightSource.isActiveAndEnabled)
        {
            if (_currentBatteryLife > 0.0f)
            {
                _currentBatteryLife -= _batteryDrainSpeed * Time.deltaTime;
                FearManager.Instance.DecreaseFearOverTime(_fearReductionSpeedMultiplier);
            }
            else
            {
                _lightSourceActive = FlashlightActive.Off;
                _lightSource.enabled = false;

            }
        }
        else
            FearManager.Instance.IncreaseFearOverTime(_fearIncreaseSpeedMultiplier);
    }

    public void ToggleFlashlight()
    {
        if (_lightSourceActive == FlashlightActive.On)
        {
            _lightSourceActive = FlashlightActive.Off;
            _lightSource.enabled = false;
        }

        else
        {
            _lightSourceActive = FlashlightActive.On;
            _lightSource.enabled = true;
        }
    }
}
