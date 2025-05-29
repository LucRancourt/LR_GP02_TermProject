using UnityEngine;
using System.Linq;

public abstract class BasePoolManager
{
    // Variables
    private readonly GameObject[] _pool;
    private readonly int _poolSize;
    
    
    
    // Constructor
    protected BasePoolManager(int numberOfObjects, GameObject objectPrefab)
    {
        if (!objectPrefab)
        {
            Debug.LogError("Should not hit!");
            return;
        }
        
        
        // Setup Pool
        _poolSize = numberOfObjects;
        
        _pool = new GameObject[_poolSize];

        for (int i = 0; i < _poolSize; i++)
        {
            _pool[i] = Object.Instantiate(objectPrefab);
        }


        ReturnAllObjectsToPool();
    }
    
    
    
    // Functions
    protected GameObject GetAvailable()
    {
        // Return first Inactive from Pool
        return _pool.FirstOrDefault(poolObject => !poolObject.activeInHierarchy);
    }

    public bool AnyAvailable()
    {
        // Check for any Inactive from Pool
        return _pool.Any(poolObject => !poolObject.activeInHierarchy);
    }

    public void ReturnAllObjectsToPool()
    {
        foreach (GameObject poolObject in _pool)
        {
            poolObject.SetActive(false);
        }
    }
}
