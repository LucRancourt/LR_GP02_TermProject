using UnityEngine;

public class HitscanWeapon : Weapon
{
    // Variables
    [SerializeField] private float range = 100.0f;
    
    [SerializeField] private LayerMask targetLayer;
    
    
    // Functions
    public override void Fire()
    {
        base.Fire();

        if (!_canFire) return;
        
        Debug.DrawRay(muzzle.transform.position, muzzle.transform.forward * range, Color.blue, 5.0f);
    }
}
