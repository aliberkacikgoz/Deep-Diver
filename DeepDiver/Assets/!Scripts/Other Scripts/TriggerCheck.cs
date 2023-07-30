using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [SerializeField] string _requiredOtherTag;
    [SerializeField] float _delayTime = 2.0f;

    [SerializeField] TriggerChannelSO _desiredTrigger;

    bool _isPlayerEntered = false;
    float _enterTime;

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckCredentials(other.tag))
            return;
           
        _isPlayerEntered = true;
        _enterTime = Time.time;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CheckCredentials(other.tag))
            return;

        _isPlayerEntered = false;
    }

    private bool CheckCredentials(string otherTag)
    {
        return otherTag == _requiredOtherTag;
    }

    private void Update()
    {
        if (!_isPlayerEntered)
            return;

        if (Time.time < _enterTime + _delayTime)
            return;

        _isPlayerEntered = false;
        _desiredTrigger?.Invoke();
    }


}
