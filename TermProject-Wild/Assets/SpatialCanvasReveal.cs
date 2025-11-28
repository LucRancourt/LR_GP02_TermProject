using DG.Tweening;
using UnityEngine;

public class SpatialCanvasReveal : MonoBehaviour
{
    [SerializeField] private CanvasGroup namePopup;
    private bool _isActive = false;

    private Tween _tween;


    private void Start()
    {
        namePopup.alpha = 0.0f;
        namePopup.transform.gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (namePopup != null)
        {
            _tween?.Kill();

            namePopup.transform.gameObject.SetActive(true); 
            _isActive = true;

            _tween = namePopup.DOFade(1.0f, 3.0f);
            _tween.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (namePopup != null)
        {
            _tween?.Kill();

            _isActive = false;

            _tween = namePopup.DOFade(0.0f, 1.5f).OnComplete(() => namePopup.transform.gameObject.SetActive(false));
            _tween.Play();
        }
    }

    private void Update()
    {
        if (namePopup != null && _isActive)
        {
            //namePopup.transform.LookAt(Camera.main.transform.position);   // But does backwards text since the forwards have to match
            Quaternion lookDir = Quaternion.LookRotation((Camera.main.transform.position - transform.position) * -1.0f);
            namePopup.transform.rotation = (lookDir);// Camera.main.transform.position -  transform.position);
        }
        Debug.Log(namePopup.alpha);
    }
}
