using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Door : MonoBehaviour
{
    // Variables
    private bool _isOpen = false;
    [SerializeField] private bool canBeReOpened = false;
    private bool _hasBeenOpened = false;

    private Sequence _openingSequence;
    private Sequence _closingSequence;
    [SerializeField] private float doorUpwardAmount = 10.0f;
    [SerializeField] private float doorMovementDuration = 4.0f;
    [SerializeField] private float shakeStrength = 1.0f;
    [SerializeField] private float shakeDuration = 2.0f;
    private Vector3 shakeStrengthVector = Vector3.zero;

    [SerializeField] private UnityEvent OnAnimComplete;


    // Functions
    private void Start()
    {
        shakeStrengthVector.x = shakeStrength;
        shakeStrengthVector.z = shakeStrength;


        _openingSequence = DOTween.Sequence();
        _closingSequence = DOTween.Sequence();

        DOTween.defaultAutoPlay = AutoPlay.None;


        // Shake
        _openingSequence.Append(transform.DOShakePosition(shakeDuration, shakeStrengthVector));
        _openingSequence.AppendInterval(shakeDuration - 0.25f);

        _closingSequence.Append(transform.DOShakePosition(shakeDuration, shakeStrengthVector));
        _closingSequence.AppendInterval(shakeDuration - 0.25f);


        // Open
        _openingSequence.Append(transform.DOMoveY(transform.position.y + doorUpwardAmount, doorMovementDuration));

        // Close
        _closingSequence.Append(transform.DOMoveY(transform.position.y, doorMovementDuration));



        // For Lever
        _openingSequence.OnComplete(() => { OnAnimComplete.Invoke(); }); ;
        _closingSequence.OnComplete(() => { OnAnimComplete.Invoke(); }); ;
    }



    // Toggle Method
    public void ToggleDoor()
    {
        if (_closingSequence.IsPlaying() || _openingSequence.IsPlaying()) return;

        // Close Door
        if (_isOpen)
        {
            _isOpen = false;

            _closingSequence.Restart();
        }
        // Open Door
        else
        {
            if (_hasBeenOpened && !canBeReOpened) return;

            _isOpen = true;
            _hasBeenOpened = true;

            _openingSequence.Restart();
        }
    }


    // Separate Method
    public void OpenDoor()
    {
        if (_closingSequence.IsPlaying() || _openingSequence.IsPlaying()) return;
        if (_hasBeenOpened && !canBeReOpened) return;

        _isOpen = true;
        _hasBeenOpened = true;

        _openingSequence.Restart();
    }

    public void CloseDoor()
    {
        if (_closingSequence.IsPlaying() || _openingSequence.IsPlaying()) return;

        _isOpen = false;

        _closingSequence.Restart();
    }
}
