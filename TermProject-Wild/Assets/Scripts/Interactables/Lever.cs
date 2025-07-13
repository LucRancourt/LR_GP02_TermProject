using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;

public class Lever : MonoBehaviour, IInteractable
{
    // Variables
    [SerializeField] private bool multipleUses = false;
    private bool _hasBeenUsed = false;
    private bool _isDown = false;

    private Sequence _leverSequence;
    [SerializeField] private float leverRotation = 90.0f;
    private Vector3 rotationAmount = Vector3.zero;
    [SerializeField] private float leverRotationDuration = 4.0f;

    [SerializeField] private UnityEvent OnActive, OnDeactive;
    private bool _canToggle = true;


    // Functions
    private void Start()
    {
        rotationAmount.z = leverRotation;

        _leverSequence = DOTween.Sequence();

        Transform pivotParent = transform.parent.transform;

        _leverSequence.Append(pivotParent.DOLocalRotate(pivotParent.rotation.eulerAngles + rotationAmount, leverRotationDuration));
    }

    IEnumerator SwitchActive(bool isActive)
    {
        yield return new WaitForSeconds(leverRotationDuration);

        if (isActive)
            OnActive.Invoke();
        else
            OnDeactive.Invoke();
    }

    public void Interact()
    {
        if (!_canToggle) return;
        if (_leverSequence.IsPlaying()) return;
        if (!multipleUses && _hasBeenUsed) return;


        _hasBeenUsed = true;

        if (_isDown)
        {
            StartCoroutine(SwitchActive(false));
            _leverSequence.PlayBackwards();
        }
        else
        {
            StartCoroutine(SwitchActive(true));
            _leverSequence.PlayForward();
        }

        _isDown = !_isDown;

        _canToggle = false;
    }

    public void ResetToggle()
    {
        _canToggle = true;
    }
}
