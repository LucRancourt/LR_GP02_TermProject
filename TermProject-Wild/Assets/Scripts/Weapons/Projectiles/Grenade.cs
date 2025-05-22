using UnityEngine;

public class Grenade : Projectile
{
    // Variables
    [SerializeField] private int totalNumberOfBounces = 3;
    private int _currentNumberOfBounces = 0;
    
    
    // Functions
    protected override void SetupRigidbody()
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        _currentNumberOfBounces = totalNumberOfBounces;
        rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentNumberOfBounces == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        
        _currentNumberOfBounces--;
    }
}
