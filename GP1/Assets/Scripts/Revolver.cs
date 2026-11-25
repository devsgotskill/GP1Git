using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;

public class Revolver : MonoBehaviour, IWeapon
{
    private enum WeaponState
    {
        ReadyToFire,
        ShotCooldown,
        Reloading,
    }

    [SerializeField] private int _extraAmmo, _maxExtraAmmo;
    private readonly int _maxAmmo = 6;
    private int _currentAmmo;
    [SerializeField] private float _timeToReloadBullet = 1.0f;
    [SerializeField] private float _shotCooldownLength = 1.2f;
    [SerializeField] private int _shotDamage = 10;
    private float _elapsedTimeReloading = 0.0f;
    private float _elapsedTimeCoolingDownShot = 0.0f;
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
        if (!(_weaponState == WeaponState.ReadyToFire) || _currentAmmo == 0)
            return;

        FearManager.Instance.TriggerRecoil();

        RectTransform crosshairRect = FearManager.Instance.GetCrosshair().GetComponent<RectTransform>();

        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(null, crosshairRect.position);

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray,
            out RaycastHit hit))
        {
            currentDamageable = hit.transform.gameObject.GetComponent<IDamageable>();
            hitPos = hit.point;

            currentDamageable?.TakeDamage(_shotDamage);

            _currentAmmo--;
        }

        _weaponState = WeaponState.ShotCooldown;
    }

    public void Reload()
    {
        if (_currentAmmo != _maxAmmo)
            _weaponState = WeaponState.Reloading;
    }

    public void AddAmmo(int amount = 6)
    {
        _extraAmmo += amount;
        if (_extraAmmo > _maxExtraAmmo)
            _extraAmmo = _maxExtraAmmo;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // this needs to be set as initialize potentially, or just set this in the variables above.
        _currentAmmo = _maxAmmo;
    }

    public void UpdateRevolver()
    {
        if (_weaponState == WeaponState.Reloading)
        {
            if (_extraAmmo == 0)
            {
                _weaponState = WeaponState.ReadyToFire;
                return;
            }
            _elapsedTimeReloading += Time.deltaTime;
            if (_elapsedTimeReloading > _timeToReloadBullet + FearManager.Instance.RevolverReloadOffset() 
                && _currentAmmo < 6)
            {
                _elapsedTimeReloading = 0.0f;
                _currentAmmo++;
                _extraAmmo--;
            }
            else if (_currentAmmo == 6)
                _weaponState = WeaponState.ReadyToFire;
        }
        else if (_weaponState == WeaponState.ShotCooldown)
        {
            _elapsedTimeCoolingDownShot += Time.deltaTime;
            if (_elapsedTimeCoolingDownShot > _shotCooldownLength)
            {
                _weaponState = WeaponState.ReadyToFire;
                _elapsedTimeCoolingDownShot = 0.0f;
            }

        }
    }

    public string Ammo()
    {
        return _currentAmmo + " / " + _extraAmmo;
    }
}
