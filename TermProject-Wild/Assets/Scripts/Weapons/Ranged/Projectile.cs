using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    // Variables
    protected Rigidbody rb;

    private float _damage;
    protected float speed = 1.0f;
    
    [SerializeField] private float lifeTime = 5.0f;

    
    
    
    // Functions
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        SetupRigidbody();
    }

    protected virtual void SetupRigidbody()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void SetupProjectile(Vector3 startPos, Quaternion direction, float damage, float projSpeed)
    {
        transform.position = startPos;
        transform.rotation = direction;

        _damage = damage;
        speed = projSpeed;

        gameObject.SetActive(true);
    }
    
    protected virtual void OnEnable()
    {
        if (!rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
        StartCoroutine(StartLifetime());
    }
    
    IEnumerator StartLifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        
        if (gameObject.activeInHierarchy) 
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        // Spawn PFX
        // Play SFX
    }

    protected virtual void Move()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}
