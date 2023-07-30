using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderAnimationHandler : MonoBehaviour
{
    [SerializeReference] private TriggerCheck _triggerCheckScript;
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.value = 0;
    }

    private void OnEnable()
    {
        _triggerCheckScript.OnStep += AnimateSlider;
    }
    private void OnDisable()
    {
        _triggerCheckScript.OnStep -= AnimateSlider;
    }

    private void AnimateSlider(float obj)
    {
        _slider.value = obj;
    }


}
