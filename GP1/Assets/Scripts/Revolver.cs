using UnityEngine;

public class Revolver : IWeapon
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

    public string Name
    {
        get => "Revolver";
    }

    string IWeapon.Name => throw new System.NotImplementedException();

    void IWeapon.FireWeapon()
    {
        // implement raycasting 
    }

    void IWeapon.Reload()
    {
        if (_currentAmmo != _maxAmmo)
            _weaponState = WeaponState.Reloading;
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // this needs to be set as initialize potentially, or just set this in the variables above.

        _currentAmmo = _maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_weaponState)
        {
            case WeaponState.ReadyToFire:
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
