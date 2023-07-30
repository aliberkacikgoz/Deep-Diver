using System;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [SerializeField] string _requiredOtherTag;
    [SerializeField] float _delayTime = .5f;
    [SerializeField] TriggerChannelSO _desiredTrigger;

    bool _isPlayerEntered = false;
    [SerializeField] bool isPlayerOxygenUpgrade = false;
    [SerializeField] bool isPlayerCapacityUpgrade = false;
    float _endTime;

    private bool _upgradedOnce = false;

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
        _upgradedOnce = false;
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

        if (isPlayerOxygenUpgrade)
        {
            if (MoneyManager.Instance.CurrentPlayerMoney >= 200)
            {
                if (_upgradedOnce) return;
                UpgradeManager.Instance.UpgradePlayerOxygenLevel();
                MoneyManager.Instance.SpendMoney(200);
                _upgradedOnce = true;
            }
            return;
        }
        else if (isPlayerCapacityUpgrade)
        {
            if (MoneyManager.Instance.CurrentPlayerMoney >= 200)
            {
                if (_upgradedOnce) return;
                UpgradeManager.Instance.UpgradePlayerInventory();
                MoneyManager.Instance.SpendMoney(200);
                _upgradedOnce = true;
            }
            return;
        }
        
      
        _isPlayerEntered = false;
        _desiredTrigger?.Invoke();
    }

}
