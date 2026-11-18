using UnityEngine;

public class PlayingState : State
{
    // Will need a list of enemies, the refernce to the player
    // 



    public override void UpdateState()
    {
        base.UpdateState();

        // Player, enemies, etc will be updated through here later on,
        // along with the input necessary to change to other states such
        // as paused, settings, main menu etc.
    }
}
