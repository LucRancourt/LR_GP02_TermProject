using UnityEngine;

public class RangedWeapon : Weapon
{
    // Variables
    [Header("Ranged Details")]
    [SerializeField] private bool isAutomatic = false;
    private bool _autoActive;
    
    [SerializeField] protected Transform muzzle;
    
    [Header("Ammo Details")]
    [SerializeField] protected int maxAmmo = 50;
    private int _currentAmmo = 0;
    [SerializeField] private int ammoRequired = 1;
    
    
    // Functions
    protected override void Awake()
    {
        base.Awake();
        
        _currentAmmo = maxAmmo;
    }
    
    private void Update()
    {
        if (_autoActive && CanFire())
            Use();
    }
    
    public override void Use()
    {
        base.Use();
        
        _currentAmmo = Mathf.Clamp(_currentAmmo -= ammoRequired, 0, maxAmmo);
        
        if (isAutomatic)
            _autoActive = true;

        
        // Play sound effect
        // Spawn particle at muzzle location
    }
    
    public override void StopUsing()
    {
        if (isAutomatic)
            _autoActive = false;
    }
    
    

    public virtual void Reload(int ammoToAdd)
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo + ammoToAdd, 0, maxAmmo);
    }

    protected bool CanFire()
    {
        return ammoRequired <= _currentAmmo && CanUse();
    }
}
