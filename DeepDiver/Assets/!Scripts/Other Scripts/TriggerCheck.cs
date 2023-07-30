using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [SerializeField] string _requiredOtherTag;

    [SerializeField] TriggerChannelSO _desiredTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (CheckCredentials(other.tag))
            _desiredTrigger.Invoke();
    }

    private bool CheckCredentials(string otherTag)
    {
        return otherTag == _requiredOtherTag;
    }


}
