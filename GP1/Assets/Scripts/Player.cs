using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _revolverExtraAmmo;
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

    public void AddBattery()
    {
        _flashlight.AddBattery();
    }

    public void AddAmmo()
    {
        
    }

    public void AddHealth(int healthAmount = 50)
    {
        _health += healthAmount;
        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public void RemoveHealth(int damage_taken)
    {
        _health -= damage_taken;
        /*
         * if (_health <= 0) { Scenemanger.LoadScene("Menu"); }
         */
    }

    public string CurrentAmmunition()
    {
        return _revolver.Ammo();
    }
}
