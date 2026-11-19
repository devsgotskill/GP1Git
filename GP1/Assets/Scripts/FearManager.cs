using Unity.VisualScripting;
using UnityEngine;

public class FearManager : MonoBehaviour
{
    private float _fearLevel = 0.0f;

    public static FearManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // To be used in a update method where smaller amounts of fear are applied in a constant manner over time
    public void IncreaseFearOverTime(float multiplier)
    {
        if (_fearLevel  < 1.0f) 
            _fearLevel += 0.01f * Time.deltaTime * multiplier;
    }

    // To be used in a update method where smaller amounts of fear are removed in a constant manner over time
    public void DecreaseFearOverTime(float multiplier)
    {
        if (_fearLevel > 0.0f) 
            _fearLevel -= 0.01f * Time.deltaTime * multiplier;
    }

    // Used when a single amount of fear is added from an action that causes an instant amount of fear
    public void IncreaseFearByAmount(float amount)
    {
        if (_fearLevel < 1.0f)
            _fearLevel += amount;
    }

    // Used when a single amount of fear is removed from an action that removes an instant amount of fear
    public void DecreaseFearByAmount(float amount)
    {
        if (_fearLevel > 0.0f)
            _fearLevel -= amount;
    }

    // Returns current fear amount
    public float GetFearAmount()
        { return _fearLevel; }
}
