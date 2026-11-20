using Unity.VisualScripting;
using UnityEngine;

public class FearManager : MonoBehaviour
{
    public static FearManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private float _fearLevel = 0.5f;

    [SerializeField] private float _fearAccuracyModifier = 2.0f;

    [SerializeField] private GameObject _crosshair;

    private Vector3 _accuracyOffset;
    private float _randomOffsetFloat;

    [SerializeField] private float _offsetDirectionCooldown = 0.25f;
    [SerializeField] private float _timeSinceOffsetDirectionSwitch = 0.0f;
    private Vector3 _center;

    private Vector3 _next, _prev;

    

    public void Start()
    {
        _center = _crosshair.transform.position;
        _prev = _crosshair.transform.position;
        _next = _crosshair.transform.position;
    }

    public void Update()
    {
        _timeSinceOffsetDirectionSwitch += Time.deltaTime;
        if (_timeSinceOffsetDirectionSwitch > _offsetDirectionCooldown)
        {
            _timeSinceOffsetDirectionSwitch -= _offsetDirectionCooldown;

            _randomOffsetFloat = Random.Range(0.0f, 360.0f);

            float angle = _randomOffsetFloat * Mathf.Deg2Rad;

            _accuracyOffset = new Vector3(Mathf.Cos(angle) * _fearLevel * _fearAccuracyModifier,
                                          Mathf.Sin(angle) * _fearLevel * _fearAccuracyModifier, 
                                          0);

            _prev = _next;
            _next = _center + _accuracyOffset;
        }

        UpdateCrosshairPosition();
    }

    private void UpdateCrosshairPosition()
    {
        _crosshair.transform.position = new Vector3(Mathf.Lerp(_prev.x, _next.x, _timeSinceOffsetDirectionSwitch / _offsetDirectionCooldown), 
                                                    Mathf.Lerp(_prev.y, _next.y, _timeSinceOffsetDirectionSwitch / _offsetDirectionCooldown), 
                                                    0);
    }

    public GameObject GetCrosshair()
    {
        // offset = random direction with length of fear modifier
        
        return _crosshair;
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
