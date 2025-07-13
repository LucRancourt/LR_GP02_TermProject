using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TriggerBox : MonoBehaviour
{
    // Variables
    private BoxCollider _boxCollider;

    [SerializeField] private LayerMask triggerMask;
    
    [Tooltip("Value of -1 means an infite amount of times.")]
    [SerializeField] private int timesCanTrigger;


    // Functions
    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
        _boxCollider.includeLayers = triggerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (timesCanTrigger != 0)
        {
            timesCanTrigger--;
            TriggerBehavior();
        }
    }

    protected void TriggerBehavior()
    {
        Debug.Log("Hit me!");
    }
}
