using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    // Variables
    private Sequence _mySequence;
    [SerializeField] private float movementAmount = 50.0f;
    [SerializeField] private float movementTime = 5.0f;
    [SerializeField] private float scaleAmount = 3.0f;
    [SerializeField] private float intervalDuration = 3.0f;

    // Functions
    private void Start()
    {
        _mySequence = DOTween.Sequence();


        _mySequence.Append(transform.DOMoveX(movementAmount, movementTime));
        _mySequence.Join(transform.DOScale(scaleAmount, movementTime));

        _mySequence.AppendInterval(intervalDuration);


        _mySequence.Append(transform.DOMoveZ(movementAmount, movementTime));
        _mySequence.Join(transform.DOScale(1.0f, movementTime));

        _mySequence.AppendInterval(intervalDuration);


        _mySequence.Append(transform.DOMoveX(-movementAmount, movementTime));
        _mySequence.Join(transform.DOScale(scaleAmount, movementTime));

        _mySequence.AppendInterval(intervalDuration);


        _mySequence.Append(transform.DOMoveZ(-movementAmount, movementTime));
        _mySequence.Join(transform.DOScale(1.0f, movementTime));

        _mySequence.AppendInterval(intervalDuration);


        _mySequence.SetLoops(-1, LoopType.Yoyo);


        _mySequence.Play();
    }
}
