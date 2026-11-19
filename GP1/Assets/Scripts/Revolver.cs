using UnityEngine;
using UnityEngine.InputSystem;

public class Revolver : MonoBehaviour, IWeapon
{
    private enum WeaponState
    {
        ReadyToFire,
        Reloading,
    }

    private readonly int _maxAmmo = 6;
    private int _currentAmmo;
    private float _timeToReloadBullet = 1.0f;
    private float _elapsedTimeReloading = 0.0f;
    private WeaponState _weaponState = WeaponState.ReadyToFire;
    string IWeapon.Name => "Revolver";

    public IDamageable currentDamageable;
    Vector3? hitPos = null;

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

    public void FireWeapon()
    {
        // If gun is reloading or ammo is 0
        if (_weaponState == WeaponState.Reloading || _currentAmmo == 0)
            return;

        if (Physics.Raycast(Camera.main.transform.position,
            Camera.main.transform.forward,
            out RaycastHit hit))
        {
            currentDamageable = hit.transform.gameObject.GetComponent<IDamageable>();
            hitPos = hit.point;

            currentDamageable?.TakeDamage(10);

            Debug.Log("Inside fire weapon");

            _currentAmmo--;
        }
    }

    public void Reload()
    {
        if (_currentAmmo != _maxAmmo)
            _weaponState = WeaponState.Reloading;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // this needs to be set as initialize potentially, or just set this in the variables above.wwwwwwww
        _currentAmmo = _maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputActions.Player.Reload.WasPressedThisFrame())
        {
            _weaponState = WeaponState.Reloading;
            Debug.Log("Reload weapon changed");
        }

        switch (_weaponState)
        {
            case WeaponState.ReadyToFire:
                if (_inputActions.Player.Shoot.WasPressedThisFrame())
                {
                    Debug.Log("Fire weapon called");
                    FireWeapon();
                }
                break;
            case WeaponState.Reloading:
                _elapsedTimeReloading += Time.deltaTime;
                if (_elapsedTimeReloading > _timeToReloadBullet && _currentAmmo < 6)
                {
                    _elapsedTimeReloading -= _timeToReloadBullet;
                    _currentAmmo++;
                }
                else if (_currentAmmo == 6)
                    _weaponState = WeaponState.ReadyToFire;
                break;

        }
    }
}
