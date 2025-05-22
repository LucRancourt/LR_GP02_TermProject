using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    // Variables
    [Header("Default Weapon Details")]
    [SerializeField] protected int maxAmmo = 50;

    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private bool isAutomatic = false;
    private bool _autoActive;

    private bool _onCooldown;
    private WaitForSeconds _cooldownWait;
    protected bool _canFire = true;

    [SerializeField] protected Transform muzzle;
    
    
    // Functions
    private void Awake()
    {
        _cooldownWait = new WaitForSeconds(fireRate);
    }

    private void Update()
    {
        if (_autoActive)
            Fire();
    }
    
    public virtual void Fire()
    {
        if (_onCooldown)
        {
            _canFire = false;
            return;
        }
        
        _canFire = true;
        
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

    IEnumerator FireCooldown()
    {
        _onCooldown = true;
        
        yield return _cooldownWait;
        
        _onCooldown = false;
    }
}
