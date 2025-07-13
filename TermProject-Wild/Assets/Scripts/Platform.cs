using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    // Variables
    private Sequence _mySequence;
    [SerializeField] private Vector3 movementAmount = Vector3.zero;
    [SerializeField] private float movementTime = 5.0f;
    [SerializeField] private float scaleAmount = 3.0f;
    [SerializeField] private float intervalDuration = 3.0f;

    // Functions
    private void Start()
    {
        _mySequence = DOTween.Sequence();


        _mySequence.Append(transform.DOMove(transform.position + movementAmount, movementTime));
        _mySequence.AppendInterval(intervalDuration);


        _mySequence.Append(transform.DOMove(transform.position - movementAmount, movementTime));
        _mySequence.AppendInterval(intervalDuration);


        _mySequence.SetLoops(-1, LoopType.Yoyo);


        _mySequence.Play();
    }
}
