using UnityEngine;

public class RangedWeapon : Weapon
{
    // Variables
    [SerializeField] protected RangedWeaponConfig rangedWeaponConfig;
    private bool _autoActive;
    private int _currentAmmo = 0;
    
    
    // Functions
    protected override void Awake()
    {
        base.Awake();
        
        _currentAmmo = rangedWeaponConfig.maxAmmo;
    }
    
    private void Update()
    {
        if (_autoActive && CanFire())
            Use();
    }
    
    public override void Use()
    {
        base.Use();
        
        _currentAmmo = Mathf.Clamp(_currentAmmo -= rangedWeaponConfig.ammoRequired, 0, rangedWeaponConfig.maxAmmo);
        
        if (rangedWeaponConfig.isAutomatic)
            _autoActive = true;

        
        // Play sound effect
        // Spawn particle at muzzle location
    }
    
    public override void StopUsing()
    {
        if (rangedWeaponConfig.isAutomatic)
            _autoActive = false;
    }
    
    

    public virtual void Reload(int ammoToAdd)
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo + ammoToAdd, 0, rangedWeaponConfig.maxAmmo);
    }

    protected bool CanFire()
    {
        return rangedWeaponConfig.ammoRequired <= _currentAmmo && CanUse();
    }
}
