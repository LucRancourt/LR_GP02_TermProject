using UnityEngine;

public class ProjectileManager : BasePoolManager
{
    // Variables
    private float _damage;
    private float _speed;


    // Constructor 
    public ProjectileManager(int numberOfProjectiles, Projectile projectilePrefab, float damage, float speed) : base(numberOfProjectiles, projectilePrefab.gameObject) 
    { 
        _damage = damage;
        _speed = speed;
    }

    
    // Functions
    public void Fire(Vector3 spawnPosition, Quaternion direction)
    {
        GameObject projectileToFire = GetAvailable();

        if (!projectileToFire)
            return;

        projectileToFire.GetComponent<Projectile>().SetupProjectile(spawnPosition, direction, _damage, _speed);
    }
}
