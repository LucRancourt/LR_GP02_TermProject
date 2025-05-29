using UnityEngine;

public class ProjectileWeapon : RangedWeapon
{
    // Variables
    private ProjectileManager _projectileManager;
    [SerializeField] private Projectile projectile;
    
    
    // Functions
    private void Start()
    {
        _projectileManager = new ProjectileManager(maxAmmo, projectile);
    }
    
    public override void Use()
    {
        if (!_projectileManager.AnyAvailable()) return;
        
        if (!CanFire()) return;
        
        base.Use();

        _projectileManager.Fire(muzzle.transform.position, muzzle.transform.rotation);
    }
}
