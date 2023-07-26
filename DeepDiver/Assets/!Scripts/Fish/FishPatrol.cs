using System.Collections;
using UnityEngine;

public class FishPatrol : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _targetChangeTime = 2f;
    [SerializeField] private WaitForSeconds _changeTargetInterval;
    [SerializeField] private Transform _areaBoundary;

    private Vector3 _targetPosition;

    private void Awake()
    {
        _changeTargetInterval = new WaitForSeconds(_targetChangeTime);
    }

    private void Start()
    {
        StartCoroutine(ChangeTargetPosition());
    }

    private void Update()
    {
        MoveFish();
    }

    private void MoveFish()
    {
        transform.LookAt(_targetPosition);

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private IEnumerator ChangeTargetPosition()
    {
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere;

            randomDirection.z = 0f;

            _targetPosition = _areaBoundary.position + randomDirection * _areaBoundary.localScale.x * 0.5f;

            yield return _changeTargetInterval;
        }
    }
}