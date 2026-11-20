using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float _currentBatteryLife = 1.0f;
    [SerializeField] private float _maxBatteryLife = 1.0f;
    [SerializeField] private float _batteryDrainSpeed = 1.0f;

    [SerializeField] private Light _lightSource;

    void Start()
    {
        
    }

    void Update()
    {
        if (_lightSource.isActiveAndEnabled)
        {
            if (_currentBatteryLife > 0.0f)
            {
                _currentBatteryLife -= _batteryDrainSpeed * Time.deltaTime;
            }
            else
            {
                _lightSource.enabled = false;
            }
        }
    }
}
