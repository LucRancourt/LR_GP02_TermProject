using UnityEngine;

public class HitscanWeapon : Weapon
{
    // Variables
    [SerializeField] private float range = 100.0f;
    
    [SerializeField] private LayerMask targetLayer;
    
    
    // Functions
    public override void Fire()
    {
        if (!CanFire()) return;

        base.Fire();

        Debug.DrawRay(muzzle.transform.position, muzzle.transform.forward * range, Color.blue, 5.0f);
    }
}
