using UnityEngine;

public class TransformWithTargetAtDistance : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform targetTransform;
    private float _distanceFromTarget;
    private float _verticalDistanceFromTarget;
    private Vector3 newPos;


    // Functions
    private void Awake()
    {
        Vector3 thisPos = transform.position;
        Vector3 targetPos = targetTransform.position;

        _verticalDistanceFromTarget = HelpfulFunctions.Abs(thisPos.y - targetPos.y);

        thisPos.y = 0.0f;
        targetPos.y = 0.0f;

        _distanceFromTarget = Vector3.Distance(thisPos, targetPos);
    }

    private void FixedUpdate()
    {
        newPos = targetTransform.position + (targetTransform.forward * _distanceFromTarget);
        newPos.y += _verticalDistanceFromTarget;
        transform.position = newPos;
    }
}
