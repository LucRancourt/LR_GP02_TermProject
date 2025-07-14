using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TransformTargetWithSelf : MonoBehaviour
{
    // Variables
    private Collider _collider;

    private Collider _gameObjectCollidedLast;

    private Vector3 _currentPosition;
    private Vector3 _previousPosition;


    // Functions
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;

        _currentPosition = transform.position;
        _previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_gameObjectCollidedLast != null) return;

        _gameObjectCollidedLast = other;
    }

    private void OnTriggerExit(Collider other)
    {
        _gameObjectCollidedLast = null;
    }


    private void FixedUpdate()
    {
        if (_gameObjectCollidedLast != null)
        {
            _currentPosition = transform.position;
            Vector3 displacement = _currentPosition - _previousPosition;
            _previousPosition = _currentPosition;

            if (_gameObjectCollidedLast.TryGetComponent(out CharacterController player))
                player.Move(displacement);// * Time.fixedDeltaTime);
        }
    }


    /*
    public void SetTarget(Transform target)
    {
        _transformTarget = target;
    }

    public void ClearTarget()
    {
        _transformTarget = null;
    }
    */
}
