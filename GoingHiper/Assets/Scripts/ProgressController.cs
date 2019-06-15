using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour
{
    [SerializeField] Slider _slider;

    public void ChangeMaxValue(float max)
    {
        _slider.maxValue = max;
    }

    public void ChangeValue(float distance)
    {
        _slider.value = distance;
    }
}