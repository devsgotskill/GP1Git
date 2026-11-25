using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayingState : State
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

    // Will need a list of enemies, the refernce to the player

    private List<GameObject> _enemies;

    [SerializeField] private Player _player;


    public override void UpdateState()
    {
        base.UpdateState();

        _player.UpdatePlayer();

        if (_inputActions.Player.Pause.WasPressedThisFrame())
        {
            GameManager.Instance.SwitchState<PauseState>();
        }

        // Player, enemies, etc will be updated through here later on,
        // along with the input necessary to change to other states such
        // as paused, settings, main menu etc.
    }
}
