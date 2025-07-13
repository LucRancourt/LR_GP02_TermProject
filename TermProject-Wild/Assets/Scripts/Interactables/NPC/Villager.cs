using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Villager : MonoBehaviour, IDamageable
{
    // Variables
    [SerializeField] private float health = 100.0f;
    
    // Functions
    public void PrintMessage(string message)
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
