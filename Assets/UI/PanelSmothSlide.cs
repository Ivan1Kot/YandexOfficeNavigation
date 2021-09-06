using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PanelSmothSlide : MonoBehaviour
{
    [SerializeField] private float _slideTime = 0.15f;

    [SerializeField] private Vector2 _extraSlideFinalPosition;
    [SerializeField] private RectTransform _contentPanel;

    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _text;


    private RectTransform _transform;
    private Vector2 _finalPosition;
    private Vector2 _zeroPosition;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _finalPosition = _transform.anchoredPosition;
        _zeroPosition = new Vector2(_transform.anchoredPosition.x, -_transform.sizeDelta.y/2);
    }

    private void OnEnable()
    {
        Debug.Log(_transform.anchoredPosition);
        _transform.anchoredPosition = _transform.anchorMin.y < 1 ? _zeroPosition : -_zeroPosition;
        //StartCoroutine(Slide());
    }

    public void Init(string label, string text)
    {
        _label.text = label;
        _text.text = text;
    }

    public void CallExtraSlide()
    {
        StopAllCoroutines();
        StartCoroutine(ExtraSlide());
    }

    public void CallHide()
    {
        StopAllCoroutines();
        StartCoroutine(Hide());
    }

    public void CallSlide()
    {
        StopAllCoroutines();
        _transform.gameObject.SetActive(true);
        StartCoroutine(Slide());
    }

    private IEnumerator Slide()
    {
        while(_transform.anchoredPosition != _finalPosition)
        {
            _transform.anchoredPosition = Vector2.Lerp(_transform.anchoredPosition, _finalPosition, _slideTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Hide()
    {
        while (_transform.anchoredPosition != _zeroPosition)
        {
            _transform.anchoredPosition = Vector2.Lerp(_transform.anchoredPosition, _zeroPosition, _slideTime);
            yield return new WaitForEndOfFrame();
        }
        _transform.gameObject.SetActive(false);
    }

    private IEnumerator ExtraSlide()
    {
        var _finalPos = new Vector2(_transform.anchoredPosition.x, _transform.sizeDelta.y/2);
        while(_contentPanel.anchoredPosition != _extraSlideFinalPosition)
        {
            _contentPanel.anchoredPosition = Vector2.Lerp(_contentPanel.anchoredPosition, _extraSlideFinalPosition, _slideTime/1.5f);
            yield return new WaitForEndOfFrame();
        }
    }
}