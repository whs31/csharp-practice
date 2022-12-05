using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour
{
    protected enum StatusBarMode
    {
        OneSided,
        TwoSided
    }
    protected enum UseValueChangeEffect
    {
        No,
        UseColorFade
    }

    protected enum ValueDisplay
    {
        Hide,
        OnlyText,
        TextWithPostfix,
        TextWithIcon,
        OnlyIcon,
        All
    }

    [Header("Value bar settings")]
    [SerializeField] private string textPostfix = "Units";
    [SerializeField] private float valueToPixelRatio = 1f;
    [SerializeField] private StatusBarMode statusBarMode = StatusBarMode.OneSided;
    [SerializeField] private UseValueChangeEffect useValueChangeEffect = UseValueChangeEffect.No;
    [SerializeField] private float fadeDelay = 1f;
    [SerializeField] private float fadeSpeed = 0.0005f;
    [Space(10)]

    [Header("Assigned UI elements")]
    [SerializeField] private RectTransform fillerImageElement;
    [SerializeField] private RectTransform faderElement;
    [SerializeField] private TMPro.TextMeshProUGUI textElement;
    private RectTransform baseElement;

    private float _textPrefferedWidth = 10f;
    private float _timer = 0f;
    private bool _fading = false;

    private float value = 0f;
    private float maxValue = 0f;
    public void SetValue(float val) { value = val; } public float GetValue() { return value; }
    public void SetMaxValue(float val) { maxValue = val; } public float GetMaxValue() { return maxValue; }  

    private float _oldValue = 0f;


    private void Start()
    {
        baseElement = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if(_fading && _timer < fadeDelay)
        {
            _timer += Time.smoothDeltaTime;
        }

        if (_fading && _timer >= fadeDelay)
        {
            UpdateTimer();
        }
    }

    public void OnValueChanged()
    {
        if(_oldValue >= value)
        {
            Fade();
        }
        else
        {
            faderElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (value * baseElement.rect.width) / maxValue);
            _oldValue = value;
        }
        fillerImageElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (value * baseElement.rect.width) / maxValue);
        textElement.text = Mathf.Round(value).ToString() + " " + textPostfix;
        Debug.Log(textElement.text);
        _textPrefferedWidth = textElement.preferredWidth;
        if (fillerImageElement.rect.width < _textPrefferedWidth)
        {
            textElement.text = "";
        }
    }

    public void OnMaxValueChanged()
    {
        baseElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (maxValue * valueToPixelRatio));
        OnValueChanged();
    }

    private void Fade()
    {
        faderElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (_oldValue * baseElement.rect.width) / maxValue);
        _fading = true;
    }

    private void UpdateTimer()
    {
        if(_oldValue > value+fadeSpeed)
        {
            _oldValue -= fadeSpeed;
            Fade();
        } else
        {
            _fading = false;
            _timer = 0f;
        }

    }
}
