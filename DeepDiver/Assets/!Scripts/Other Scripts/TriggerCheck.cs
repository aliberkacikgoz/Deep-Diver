using System;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [SerializeField] string _requiredOtherTag;
    [SerializeField] float _delayTime = .5f;
    [SerializeField] TriggerChannelSO _desiredTrigger;

    bool _isPlayerEntered = false;
    float _endTime;

    public event Action<float> OnStep;
    //public float Step => Mathf.Clamp01((Time.time - (_enterTime + _delayTime)) / _delayTime);

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckCredentials(other.tag))
            return;
           
        _isPlayerEntered = true;
        _endTime = Time.time + _delayTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CheckCredentials(other.tag))
            return;

        _isPlayerEntered = false;
        OnStep?.Invoke(0);
    }

    private bool CheckCredentials(string otherTag)
    {
        return otherTag == _requiredOtherTag;
    }

    private void Update()
    {
        if (!_isPlayerEntered)
            return;

 
        if (Time.time < _endTime) 
        {
            float step = (_endTime - Time.time) / _delayTime;
            step = Mathf.Clamp01(step);

            OnStep?.Invoke(1f - step);
            return;
        }
      
        _isPlayerEntered = false;
        _desiredTrigger?.Invoke();
    }

}
