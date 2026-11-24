using Unity.VisualScripting;
using UnityEngine;

public class FearManager : MonoBehaviour
{
    public static FearManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject _crosshair;

    [Header("Fear variables")] 
    [SerializeField] private float _fearLevel = 0.5f;
    [SerializeField] private float _fearAccuracyModifier = 2.0f;


    [Header("Recoil variables")]
    [SerializeField] private float _recoilUpTime, _recoilDownTime;
    private float _recoilTime;
    private Vector3 _recoilOffset, _currentRecoilOffset, _zeroRecoilOffset;
    private bool _recoilUp;
    private bool _recoiling;


    [Header("Fear crosshair offset variables")]
    [SerializeField] private float _randomOffsetFloat;
    [SerializeField] private float _offsetDirectionCooldown = 0.25f;
    private float _timeSinceOffsetDirectionSwitch = 0.0f;
    private Vector3 _accuracyOffset;
    private Vector3 _center;
    private Vector3 _next, _prev;



    public void Start()
    {
        _center = _crosshair.transform.position;
        _prev = _crosshair.transform.position;
        _next = _crosshair.transform.position;

        _recoilOffset = new Vector3(0, 0, 0);
        _currentRecoilOffset = new Vector3(0, 0, 0);
        _zeroRecoilOffset = new Vector3(0, 0, 0);

        _recoiling = false;
        _recoilUp = true;
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

    public void TriggerRecoil()
    {
        _recoiling = true;
        _recoilOffset = new Vector3(0, 100, 0);
        _recoilUp = true;
    }

    private void HandleRecoil()
    {
        //recoil code


        if (_recoiling && _recoilUp)
        {

            _recoilTime += Time.deltaTime;
            _currentRecoilOffset = new Vector3(Mathf.Lerp(_zeroRecoilOffset.x, _recoilOffset.x, _recoilTime / _recoilUpTime),
                                               Mathf.Lerp(_zeroRecoilOffset.y, _recoilOffset.y, _recoilTime / _recoilUpTime),
                                               0);
            if (_recoilTime > _recoilUpTime)
            {
                _recoilUp = false;

                _recoilTime -= _recoilUpTime;
            }

        }
        else if (_recoiling && !_recoilUp)
        {

            _recoilTime += Time.deltaTime;
            _currentRecoilOffset = new Vector3(Mathf.Lerp(_recoilOffset.x, _zeroRecoilOffset.x, _recoilTime / _recoilDownTime),
                                               Mathf.Lerp(_recoilOffset.y, _zeroRecoilOffset.y, _recoilTime / _recoilDownTime),
                                               0);

            if (_recoilTime > _recoilDownTime)
            {
                _recoilUp = true;
                _recoiling = false;
                _recoilTime = 0.0f;
            }
        }
    }

    private void UpdateCrosshairPosition()
    {
        HandleRecoil();

        // handle fear sway
        _crosshair.transform.position = new Vector3(Mathf.Lerp(_prev.x, _next.x, _timeSinceOffsetDirectionSwitch / _offsetDirectionCooldown), 
                                                    Mathf.Lerp(_prev.y, _next.y, _timeSinceOffsetDirectionSwitch / _offsetDirectionCooldown), 
                                                    0);

        _crosshair.transform.position += new Vector3(_currentRecoilOffset.x, _currentRecoilOffset.y, 0);
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
    // Yo this is devs here, added this function to get the current recoil offset for the camera shake. 
    public Vector3 GetCurrentRecoilOffset()
    {
        return _currentRecoilOffset;
    }
}
