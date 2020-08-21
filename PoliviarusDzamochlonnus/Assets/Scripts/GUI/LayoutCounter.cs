using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LayoutCounter : MonoBehaviour
{
    [SerializeField] private LayoutElement bar;
    [SerializeField] private RectTransform disabledBar;
    [SerializeField] private TextMeshProUGUI text;

    public void Redraw(int count, int disabledCount = 0)
    {
        bar.flexibleWidth = count;
        var disabledF = (float)disabledCount / count;
        if (float.IsNaN(disabledF)) disabledF = 0;
        disabledF = 0.25f;
        disabledBar.anchorMin = disabledBar.anchorMin.Set(x: disabledBar.pivot.x - disabledF * disabledBar.pivot.x);
        disabledBar.anchorMax = disabledBar.anchorMax.Set(x: disabledBar.pivot.x + disabledF * (1-disabledBar.pivot.x));
        disabledBar.sizeDelta = Vector2.zero;
        if (text) text.text = "" + count;
    }
}
