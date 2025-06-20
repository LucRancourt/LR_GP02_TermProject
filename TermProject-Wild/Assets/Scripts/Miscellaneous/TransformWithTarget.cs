using UnityEngine;

public class TransformWithTarget : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform transformTarget;


    // Functions
    private void FixedUpdate()
    {
        transform.position = transformTarget.position;
        transform.rotation = transformTarget.rotation;
    }
}
