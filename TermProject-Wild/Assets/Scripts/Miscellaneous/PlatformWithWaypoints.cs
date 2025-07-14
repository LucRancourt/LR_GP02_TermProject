using DG.Tweening;
using UnityEngine;

public class PlatformWithWaypoints : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform[] waypoints;

    private Sequence _moveSequence;
    [SerializeField] private bool autoStart = true;
    [SerializeField] private float moveDuration = 5.0f;
    [SerializeField] private float delay = 3.0f;


    // Functions
    private void Start()
    {
        if (waypoints.Length == 0)
            Destroy(gameObject);


        _moveSequence = DOTween.Sequence();

        foreach (Transform point in waypoints)
        {
            _moveSequence.Append(transform.DOMove(point.position, moveDuration));
            _moveSequence.AppendInterval(delay);
        }


        _moveSequence.SetLoops(-1);


        if (autoStart)
            _moveSequence.Restart();
    }

    public void ActivatePlatform()
    {
        if (_moveSequence.IsPlaying()) return;

        _moveSequence.Restart();
    }
}
