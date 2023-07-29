using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    [SerializeField] TriggerChannelSO _swimmingStartTrigger;
    [SerializeField] TriggerChannelSO _walkingStartTrigger;
    [SerializeField] TriggerChannelSO _submarineStartTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerSwimmer") && this.CompareTag("StopSwimTrigger"))
        {
            _walkingStartTrigger.Invoke();
            Debug.Log("Walking start triggered by player swimmer");
        }

        if (other.gameObject.CompareTag("PlayerWalker") && this.CompareTag("SwimTrigger"))
        {
            _swimmingStartTrigger.Invoke();
            Debug.Log("Swimming start triggered by player walker ");

        }

        if (other.gameObject.CompareTag("PlayerWalker") && this.CompareTag("SubTrigger"))
        {
            _submarineStartTrigger.Invoke();
            Debug.Log("Sub start triggered by player walker ");

        }

        if (other.gameObject.CompareTag("PlayerSub") && this.CompareTag("StopSubTrigger"))
        {
            _walkingStartTrigger.Invoke();
            Debug.Log("Walking start triggered by player sub ");
        }

    }
}
