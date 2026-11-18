using UnityEngine;

public interface IWeapon
{
    string Name { get; }
    public void FireWeapon();
    public void Reload();
}
