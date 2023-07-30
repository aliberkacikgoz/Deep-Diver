using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateHandler : MonoBehaviour
{
    [SerializeField] TriggerChannelSO _swimmingStartTrigger;
    [SerializeField] TriggerChannelSO _walkingStartTrigger;
    [SerializeField] TriggerChannelSO _submarineStartTrigger;

    [SerializeField] GameObject _inSubCam;
    [SerializeField] GameObject _subCam;
    [SerializeField] GameObject _swimCam;
    [SerializeField] GameObject _window;

    private bool _wasSwimCam = false;

    private void OnEnable()
    {
        _swimmingStartTrigger.AddListener(ActivateSwimmingCam);
        _walkingStartTrigger.AddListener(ActivateWalkingCam);
        _submarineStartTrigger.AddListener(ActivateSubmarineCam);
    }

    private void OnDisable()
    {
        _swimmingStartTrigger.RemoveListener(ActivateSwimmingCam);
        _walkingStartTrigger.RemoveListener(ActivateWalkingCam);
        _submarineStartTrigger.RemoveListener(ActivateSubmarineCam);
    }

    private void Start()
    {
        ActivateWalkingCam();
    }

    private void ActivateSubmarineCam()
    {
        _wasSwimCam = false;
        _swimCam.SetActive(false);
        _inSubCam.SetActive(false);

        _subCam.SetActive(true);
        _window.SetActive(true);
    }

    private void ActivateWalkingCam()
    {
        if (_wasSwimCam)
        {
            //var transposer = _inSubCam.GetComponent<CinemachineVirtualCamera>().GetComponent<CinemachineTransposer>().m_FollowOffset;
            //transposer = new Vector3(0, 12, 40);
        }
        _wasSwimCam = false;
        _swimCam.SetActive(false);
        _subCam.SetActive(false);

        _inSubCam.SetActive(true);
        _window.SetActive(false);
    }

    private void ActivateSwimmingCam()
    {
        _wasSwimCam = true;
        _inSubCam.SetActive(false);
        _subCam.SetActive(false);

        _swimCam.SetActive(true);
        _window.SetActive(true);
    }
}
