using UnityEngine;

public class Bullet : Projectile
{
    // Variables
    
    
    // Functions
    void Update()
    {
        if (gameObject.activeInHierarchy)
            transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
    }
}
