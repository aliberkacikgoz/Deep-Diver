using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private bool invert;
    [SerializeField] private bool lookAtMainCamera;

    [SerializeField] private Transform _objectTransform;

    private void Awake()
    {
        if (!lookAtMainCamera) return;
        _objectTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (invert)
        {
            var myPosition = transform.position;
            var directionToCamera = (_objectTransform.position - myPosition).normalized;
            transform.LookAt(myPosition + directionToCamera * -1);
        }
        else
        {
            transform.LookAt(_objectTransform);
        }
    }
}