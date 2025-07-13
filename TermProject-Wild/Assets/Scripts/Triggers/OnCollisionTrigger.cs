using System.Collections;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class OnCollisionTrigger : Trigger
{
    // Variables
    [SerializeField] private LayerMask triggerMask;

    [SerializeField] private UnityEvent OnStay;
    [SerializeField] private float onStayInterval = 3.0f;
    private bool _canTriggerAgain = true;


    // Functions
    protected override void Awake()
    {
        base.Awake();

        pCollider.includeLayers = triggerMask;
        pCollider.excludeLayers = ~triggerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnStay.Invoke();

        if (!CanBeTriggered()) return;

        DecrementTriggerCount();

        OnTrigger.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_canTriggerAgain) return;

        StartCoroutine(OnStayEffect());
    }

    IEnumerator OnStayEffect()
    {
        _canTriggerAgain = false;

        yield return new WaitForSeconds(onStayInterval);

        OnStay.Invoke();
        _canTriggerAgain = true;
    }
}
