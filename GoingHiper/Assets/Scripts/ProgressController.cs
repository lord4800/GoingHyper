using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour
{

    [SerializeField] Slider _slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMaxValue(float max)
    {
        _slider.maxValue = max;
    }

    public void ChangeValue(float distance)
    {
        _slider.value = distance;
    }
}