using UnityEngine;

public class PlayingState : State
{
    public override void UpdateState()
    {
        base.UpdateState();

        // Player, enemies, etc will be updated through here later on,
        // along with the input necessary to change to other states such
        // as paused, settings, main menu etc.
    }
}
