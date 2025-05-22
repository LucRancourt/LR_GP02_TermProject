using UnityEngine;

public class ProjectileWeapon : Weapon
{
    // Variables
    private ProjectileManager _projectileManager;
    [SerializeField] private Projectile projectile;
    
    
    // Functions
    private void Start()
    {
        _projectileManager = new ProjectileManager(maxAmmo, projectile);
    }
    
    public override void Fire()
    {
        if (!_projectileManager.AnyAvailable()) return;
        
        if (!CanFire()) return;
        
        base.Fire();

        _projectileManager.Fire(muzzle.transform.position, muzzle.transform.rotation);
    }
}
