using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{

    [SerializeField] TriggerChannelSO _swimmingStartTrigger;
    [SerializeField] TriggerChannelSO _walkingStartTrigger;
    [SerializeField] TriggerChannelSO _submarineStartTrigger;

    [SerializeField] Vector3 _walkingStartPosition = new Vector3(-3,0,0);
    [SerializeField] Vector3 _swimmingStartPosition = new Vector3(0,12,0);
    [SerializeField] Vector3 _subStartRotation = new Vector3(0,0,-90);

    GameObject _playerSwimming;
    GameObject _playerWalking;
    GameObject _playerSubmarine;

    PlayerSwimmingMovement _playerSwimmingScript;
    PlayerSubmarineMovement _playerSubmarineScript;
    PlayerWalkingMovement _playerWalkingScript;


    private void OnEnable()
    {
        _swimmingStartTrigger.AddListener(ActivateSwimmingState);
        _walkingStartTrigger.AddListener(ActivateWalkingState);
        _submarineStartTrigger.AddListener(ActivateSubmarineState);
    }

    private void OnDisable()
    {
        _swimmingStartTrigger.RemoveListener(ActivateSwimmingState);
        _walkingStartTrigger.RemoveListener(ActivateWalkingState);
        _submarineStartTrigger.RemoveListener(ActivateSubmarineState);
    }

    private void Start()
    {
        //TODO:
        // Get the references
        // set camera for walking state
        // if active deactivate player swimmable and submarine controls
        // if not active,activate player walking char
        // if active, disable player swimming char
        // if not active, activate player walking controls

        // Get the references
        //TODO : Optimize it later
        _playerSwimming = GameObject.FindWithTag("PlayerSwimmer");
        _playerWalking = GameObject.FindWithTag("PlayerWalker");
        _playerSubmarine = GameObject.FindWithTag("PlayerSub");

        _playerSwimmingScript = _playerSwimming.GetComponent<PlayerSwimmingMovement>();
        _playerWalkingScript = _playerWalking.GetComponent<PlayerWalkingMovement>();
        _playerSubmarineScript = _playerSubmarine.GetComponent<PlayerSubmarineMovement>();

        // set camera for walking state
        ActivateWalkingState();

    }

    void ActivateSwimmingState()
    {
        //TODO: Player Swimmable Activate
        //Player walk conttrol disable
        //camera switch
        //player swimmable controls enable
        //Player walking deactivate

        _playerSwimming.SetActive(true);
        _playerSwimming.transform.localPosition = _swimmingStartPosition;

        _playerWalkingScript.enabled = false;
        //camera switch
        _playerSwimmingScript.enabled = true;

        _playerWalking.SetActive(false);
    }

    void ActivateWalkingState()
    {
        /*
        //TODO: player walking activate 
        //if active, deactivate player swimmable controls or player sub controls
        //camera switch
        //player walking controls enable
        //player swimming disable,

        _playerWalking.SetActive(true);
        _playerSwimmingScript.enabled = false;
        _playerSubmarineScript.enabled = false;
        _playerWalkingScript.enabled = true;
        _playerSwimming.SetActive(false);
        */
        // if active, deactivate player swimmable and submarine controls
        if (_playerSwimmingScript.enabled)
            _playerSwimmingScript.enabled = false;

        if (_playerSubmarineScript.enabled)
            _playerSubmarineScript.enabled = false;

        // if not active,activate player walking char
        if (!_playerWalking.activeInHierarchy)
        {
            _playerWalking.SetActive(true);
            _playerWalking.transform.localPosition = _walkingStartPosition;
            _playerSubmarine.transform.localRotation = Quaternion.Euler(_subStartRotation);
        }
        

        // if active, disable player swimming char
        if (_playerSwimming.activeInHierarchy)
            _playerSwimming.SetActive(false);
        

        //camera switch

        // if not active, activate player walking controls
        if (!_playerWalkingScript.enabled)
            _playerWalkingScript.enabled = true;
    }

    void ActivateSubmarineState()
    {
        //TODO: Player walking controls disable
        // camera switch
        // submarine controls enable
        // player walking disable

        _playerWalkingScript.enabled = false;
        //camera switch
        _playerSubmarineScript.enabled = true;

        _playerWalking.SetActive(false);

    }
}
