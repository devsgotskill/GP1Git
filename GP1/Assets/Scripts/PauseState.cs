using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PauseState : State
{
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
    public override void UpdateState()
    {
        base.UpdateState();

        if (_inputActions.Player.Pause.WasPressedThisFrame())
        {
            GameManager.Instance.SwitchState<PlayingState>();
        }
    }
}
