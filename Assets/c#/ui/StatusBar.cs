using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] protected Scrollbar bar;
    [SerializeField] protected RectTransform barBase;
    [SerializeField] protected TMPro.TextMeshProUGUI barText;
    [SerializeField] protected float maxValuePixelRatio = 1;

    protected float maxValue = 0;
    protected float value = 0;
    protected string barTextPostfix = "Units";

    protected void onValueChanged()
    {
        bar.size = value / maxValue;
        barText.text = Mathf.Round(value).ToString() + " " + barTextPostfix;
    }
    protected void onMaxValueChanged()
    {
        barBase.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxValue * maxValuePixelRatio);
        bar.size = value / maxValue;
        barText.text = Mathf.Round(value).ToString() + " " + barTextPostfix;
    }
}
