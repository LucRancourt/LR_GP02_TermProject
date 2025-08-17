using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour
{
    // Variables
    [SerializeField] private int pointValue = 5;
    private Collider _collider;

    [SerializeField] private List<AudioClip> collectionSFX;

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
            AudioManager.Instance.PlayRandomSoundEffect(collectionSFX);
            Destroy(gameObject);
        }
    }
}
