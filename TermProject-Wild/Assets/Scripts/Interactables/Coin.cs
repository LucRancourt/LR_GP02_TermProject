using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour
{
    // Variables
    [SerializeField] private int pointValue = 5;
    private Collider _collider;

    // Functions
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            GameManager.Instance.AddScore(pointValue);
            Destroy(gameObject);
        }
    }
}
