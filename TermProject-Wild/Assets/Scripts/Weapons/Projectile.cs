using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    // Variables
    protected Rigidbody rigidbody;
    
    [SerializeField] protected float speed = 1.0f;
    
    [SerializeField] private float lifeTime = 5.0f;
    
    
    
    // Functions
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        SetupRigidbody();
    }

    protected virtual void SetupRigidbody()
    {
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    public void SetupProjectile(Vector3 startPos, Quaternion direction)
    {
        transform.position = startPos;
        transform.rotation = direction;
        
        gameObject.SetActive(true);
    }
    
    protected virtual void OnEnable()
    {
        if (!rigidbody.isKinematic)
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
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
}
