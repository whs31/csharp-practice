using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] protected GameObject bar;
    [SerializeField] protected TMPro.TextMeshProUGUI barText;
    [SerializeField] protected float maxValuePixelRatio = 1;

    protected float maxValue = 0;
    protected float value = 0;
    protected string barTextPostfix = "Units";

    protected float textPrefferedWidth = 10f;

    protected void onValueChanged()
    {
        bar.GetComponent<Scrollbar>().size = value / maxValue;
        barText.text = Mathf.Round(value).ToString() + " " + barTextPostfix;
        if(value * maxValuePixelRatio < textPrefferedWidth)
        {
            barText.text = "";
        }
    }
    protected void onMaxValueChanged()
    {
        bar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxValue * maxValuePixelRatio);
        bar.GetComponent<Scrollbar>().size = value / maxValue;
        barText.text = Mathf.Round(value).ToString() + " " + barTextPostfix;
    }
}
