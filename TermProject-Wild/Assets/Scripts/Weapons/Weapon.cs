using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    // Variables
    [Header("Default Weapon Details")]
    [SerializeField] protected int maxAmmo = 50;
    private int _currentAmmo = 0;
    [SerializeField] private int ammoRequired = 1;

    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private bool isAutomatic = false;
    private bool _autoActive;

    private bool _onCooldown;
    private WaitForSeconds _cooldownWait;

    [SerializeField] protected Transform muzzle;
    
    
    // Functions
    private void Awake()
    {
        _currentAmmo = maxAmmo;
        _cooldownWait = new WaitForSeconds(fireRate);
    }

    private void Update()
    {
        if (_autoActive && CanFire())
            Fire();
    }
    
    public virtual void Fire()
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo -= ammoRequired, 0, maxAmmo);
        
        StartCoroutine(FireCooldown());

        if (isAutomatic)
            _autoActive = true;

        // Start shoot cooldown
        // Play sound effect
        // Spawn particle at muzzle location
    }

    public void StopFire()
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
        return ammoRequired <= _currentAmmo && !_onCooldown;
    }
    
    IEnumerator FireCooldown()
    {
        _onCooldown = true;
        
        yield return _cooldownWait;
        
        _onCooldown = false;
    }
}
