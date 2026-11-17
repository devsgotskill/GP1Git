using UnityEngine;
public class PlayerInteractor : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius = 0.5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    private IInteractable currentInteractable;
    private void Update()
    {
        CheckForInteractables();
        // If we are looking at an interactable and press the key
        if (currentInteractable != null && Input.GetKeyDown(interactKey))
        {
            currentInteractable.Interact(this);
        }
    }

    private void CheckForInteractables()
    {
        Collider[] hits = Physics.OverlapSphere(interactionPoint.position, interactionRadius, interactableLayer);
        if (hits.Length > 0)
        {
            if (hits[0].TryGetComponent(out IInteractable interactable))
            {
                currentInteractable = interactable;
                // TODO: Display interactable.InteractionPrompt on your UI here
                return;
            }
        }
        currentInteractable = null;
        // TODO: Hide UI prompt here
    }
    private void OnDrawGizmosSelected()
    {
        if (interactionPoint == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}