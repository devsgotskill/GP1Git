using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _revolverAmmo;
    [SerializeField] private int _maxRevolverAmmo;

    [SerializeField] private Revolver _revolver;
    [SerializeField] private Flashlight _flashlight;

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


    public void UpdatePlayer()
    {
        if (_inputActions.Player.Reload.WasPressedThisFrame())
        {
            _revolver.Reload();
            Debug.Log("Reloading weapon");
        }

        if (_inputActions.Player.Flashlight.WasPressedThisFrame())
        {
            _flashlight.ToggleFlashlight();
            Debug.Log("Toggle flashlight");
        }

        if (_inputActions.Player.Shoot.WasPressedThisFrame())
        {
            _revolver.FireWeapon();
            Debug.Log("SHOTS FIRED SHOTS FIRED");
        }

        _revolver.UpdateRevolver();
        _flashlight.UpdateFlashlight();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
