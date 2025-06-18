using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimationControl : MonoBehaviour
{
    // Variables
    private Animator _animator;


    // Functions
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
