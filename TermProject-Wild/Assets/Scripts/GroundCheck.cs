using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Variables
    public bool IsGrounded { get; private set; }
    private float _sphereRadius;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _groundLayer;
    
    public GameObject GroundHit { get; private set; }
    
    
    
    // Functions
    private void Start()
    {
        //Renderer meshRenderer = GetComponent<Renderer>();

        _sphereRadius = 0.6f;//meshRenderer.bounds.size.x;
        //meshRenderer.enabled = false;
        Debug.Log(_sphereRadius);
    }
    
    private void Update()
    {
        // OPTION 1 - Physics.Raycast - Casts a single line downwards.
        //              Good for precision but can miss ground on edges if the origin is not well placed.
        
        
        
        // OPTION 2 - Physics.SphereCast / Physics.CapsuleCast: Sweeps a shape downwards.
        //              More robust than a single ray for uneven surfaces or larger character bases as it checks a volume.

        RaycastHit hit;
        
        if (Physics.SphereCast(transform.position, _sphereRadius, Vector3.down, out hit, _raycastDistance, _groundLayer))
        {
            Debug.Log("Hey");
            IsGrounded = true;
            GroundHit = hit.transform.gameObject;
        }
        else
        {
            Debug.Log("dwadwadwadad");
            IsGrounded = false;
            GroundHit = null;
        }
        
        
        // OPTION 3 - Physics.CheckSphere / Physics.CheckBox: Checks for any overlapping colliders
        //              within a small shape positioned just below the character feet. Simple true/false check,
        //              efficient but provides less information (like distance or surface normal) than casts.
        
        
    }
}
