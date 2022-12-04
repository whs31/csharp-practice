using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class StatusBar : MonoBehaviour
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

    [Header("Status bar settings")]
    [SerializeField] protected StatusBarMode statusBarMode = StatusBarMode.OneSided;
    [SerializeField] protected UseValueChangeEffect useValueChangeEffect = UseValueChangeEffect.No;
    [SerializeField] protected float maxValuePixelRatio = 1;
    [SerializeField] protected float fadeDelay = 1f;
    [SerializeField] protected float fadeSpeed = 0.0005f;
    [Space(10)]

    [Header("Assigned UI elements")]
    [SerializeField] protected GameObject barBase;
    [SerializeField] protected GameObject barFader;
    [SerializeField] protected RectTransform barFaderHandle;
    [SerializeField] protected GameObject barTop;
    [SerializeField] protected TMPro.TextMeshProUGUI barText;

    [Space(10)]

    

    protected float maxValue = 0;
    protected float value = 0;
    protected string barTextPostfix = "Units";

    protected float textPrefferedWidth = 10f;


    protected float _timer = 0f;
    protected float _oldValue = 0f;
    protected bool _fading = false;

    protected void onValueChanged()
    {
        barTop.GetComponent<Scrollbar>().size = value / maxValue;
        if(statusBarMode == StatusBarMode.OneSided)
        {
            barTop.GetComponent<Scrollbar>().value = 0;
        } else
        {
            barTop.GetComponent<Scrollbar>().value = 0.5f;
        }
        barText.text = Mathf.Round(value).ToString() + " " + barTextPostfix;
        if(value * maxValuePixelRatio < textPrefferedWidth)
        {
            barText.text = "";
        }
    }
    protected void onMaxValueChanged()
    {
        barBase.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxValue * maxValuePixelRatio);
        barFader.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxValue * maxValuePixelRatio);
        barFader.GetComponent<Scrollbar>().size = 0.01f;
        barTop.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxValue * maxValuePixelRatio);
        barTop.GetComponent<Scrollbar>().size = value / maxValue;

        barText.text = Mathf.Round(value).ToString() + " " + barTextPostfix;
        if (value * maxValuePixelRatio < textPrefferedWidth)
        {
            barText.text = "";
        }
    }

    //call me in update of child class
    protected void UpdateFader()
    {
        if(_fading)
        {
            _timer += Time.deltaTime;
        }
        if (_timer > fadeDelay)
        {
            if (_oldValue > value)
            {
                _oldValue -= fadeSpeed;
                barFader.GetComponent<Scrollbar>().size = _oldValue / maxValue;
            }
            else
            {
                _fading = false;
                _timer = 0;
            }
        }
    }

    protected void Fade()
    {
        //fixes unity bug with handle
        //@TODO make this better
        barFaderHandle.anchorMin = new Vector2(0.5f, 0.5f);
        barFaderHandle.anchorMax = new Vector2(0.5f, 0.5f);
        barFaderHandle.position = new Vector2(0, 0);
        barFaderHandle.anchorMin = new Vector2(0.1225f, 0);
        barFaderHandle.anchorMax = new Vector2(0.8775f, 1);
        barFaderHandle.offsetMin = new Vector2(-0.5f, -10f);
        barFaderHandle.offsetMax = new Vector2(0.5f, 10f);

        
        if (!_fading)
        {
            barFader.GetComponent<Scrollbar>().size = value / maxValue;
            _oldValue = value;
        }
        _fading = true;
    }
}
