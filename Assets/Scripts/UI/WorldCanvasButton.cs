using UnityEngine;
using UnityEngine.UI;

public class WorldCanvasButton : MonoBehaviour
{
    [SerializeField] private RectTransform _panelPrefab;
    [SerializeField] private string _label;
    [SerializeField] private string _text;

    private Button _button;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();

        var buttonInstance = Instantiate(_panelPrefab, GameObject.FindGameObjectWithTag("MainCanvas").transform);
        var panelSlideScript = buttonInstance.gameObject.GetComponentInChildren<PanelSmothSlide>();

        panelSlideScript.Init(_label, _text);
        buttonInstance.name = "Info Panel (" + _label + ")";

        _button.onClick.AddListener(panelSlideScript.CallSlide);
    }
}