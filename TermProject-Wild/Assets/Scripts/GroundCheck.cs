using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Variables
    [SerializeField] private PlayerMovementConfig _movementConfig;
    
    public bool IsGrounded { get; private set; }
    [SerializeField] private LayerMask _groundLayer;

    
    // Functions
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        // OPTION 1 - Physics.Raycast - Casts a single line downwards.
        //              Good for precision but can miss ground on edges if the origin is not well placed.



        // OPTION 2 - Physics.SphereCast / Physics.CapsuleCast: Sweeps a shape downwards.
        //              More robust than a single ray for uneven surfaces or larger character bases as it checks a volume.
        //          Can fail if the Starting Shape is already clipping the surface below (ex: if on a Slope)
        
        //if (Physics.SphereCast(transform.position, .25f, Vector3.down, out RaycastHit hit, _movementConfig.groundCheckDistance, _groundLayer))
        //    IsGrounded = true;


        // OPTION 3 - Physics.CheckX: Checks for any overlapping colliders
        //              within a small shape positioned just below the character feet. Simple true/false check,
        //              efficient but provides less information (like distance or surface normal) than casts.

        if (Physics.CheckSphere(transform.position + Vector3.down * _movementConfig.groundCheckDistance, .25f, _groundLayer))
            IsGrounded = true;


        // No Ground
        else
            IsGrounded = false;
    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .25f);
        */

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * _movementConfig.groundCheckDistance, .25f);
    }
}
