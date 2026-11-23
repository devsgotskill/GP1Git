using UnityEngine;
using System.Collections.Generic;


public class GameManager : StateMachine
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SwitchState<PlayingState>();
    }

    void Update()
    {
        UpdateStateMachine();
    }
}
