using UnityEngine;


[RequireComponent(typeof(Collider))]
public class OnCollisionTrigger : Trigger
{
    // Variables
    [SerializeField] private LayerMask triggerMask;


    // Functions
    protected override void Awake()
    {
        base.Awake();

        pCollider.includeLayers = triggerMask;
        pCollider.excludeLayers = ~triggerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CanBeTriggered()) return;

        DecrementTriggerCount();

        OnTrigger.Invoke();
    }
}
