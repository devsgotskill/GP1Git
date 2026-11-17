using UnityEngine;

public interface IInteractable
{
    string InteractionPrompt { get; }
    public void Interact(PlayerInteractor interactor);
}
