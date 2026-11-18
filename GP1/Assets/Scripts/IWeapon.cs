using UnityEngine;

public interface IWeapon
{
    string Name { get; }
    public abstract void FireWeapon();
    public abstract void Reload();
}
