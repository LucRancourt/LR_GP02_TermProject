using UnityEngine;

public class ProjectileManager : BasePoolManager
{
    // Constructor 
    public ProjectileManager(int numberOfBullets, Projectile projectilePrefab) : base(numberOfBullets, projectilePrefab.gameObject) { }
    
    
    // Functions
    public void Fire(Vector3 spawnPosition, Quaternion direction)
    {
        GameObject projectileToFire = GetAvailable();

        if (!projectileToFire)
            return;

        projectileToFire.GetComponent<Projectile>().SetupProjectile(spawnPosition, direction);
    }
}
