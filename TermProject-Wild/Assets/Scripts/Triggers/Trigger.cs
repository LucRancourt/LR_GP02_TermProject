using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class Trigger : MonoBehaviour
{
    // Variables
    protected Collider pCollider;
    private MeshRenderer _meshRenderer;

    [SerializeField] private bool disableMesh = true;

    [Tooltip("Value of -1 means an infite amount of times.")]
    [SerializeField] private int timesCanTrigger = 1;

    [SerializeField] protected UnityEvent OnTrigger;

    // Functions
    protected virtual void Awake()
    {
        pCollider = GetComponent<Collider>();
        pCollider.isTrigger = true;

        _meshRenderer = GetComponent<MeshRenderer>();

        if (disableMesh)
            _meshRenderer.enabled = false;
    }

    protected bool CanBeTriggered()
    {
        return timesCanTrigger != 0;
    }

    protected void DecrementTriggerCount()
    {
        timesCanTrigger--;
    }
}
