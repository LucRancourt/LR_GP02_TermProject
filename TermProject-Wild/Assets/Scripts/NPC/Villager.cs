using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Villager : MonoBehaviour, IInteractable, IDamageable
{
    // Variables
    [SerializeField] private string message = "Empty Message";
    [SerializeField] private float health = 100.0f;
    
    // Functions
    public void Interact()
    {
        Debug.Log(message);
    }

    public void TakeDamage(float damage, GameObject caller)
    {
        health -= damage;

        if (health <= 0.0f)
            Destroy(this.gameObject);
    }
}
