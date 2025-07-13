using UnityEngine;

public class OnInteractTrigger : Trigger, IInteractable
{
    // Functions
    protected override void Awake()
    {
        base.Awake();

        transform.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact()
    {
        if (!CanBeTriggered()) return;

        DecrementTriggerCount();

        OnTrigger.Invoke();
    }
}
