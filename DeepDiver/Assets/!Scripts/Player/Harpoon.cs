using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _whatIsGrappleable;
    [SerializeField] private float _grappleDelayTime;
    [SerializeField] private float overshootYAxis;
    [SerializeField] private LineRenderer _lineRenderer;
    
    private PlayerSwimmingMovement _swimmingMovement;
    private Vector3 _currentGrapplePoint;
    private Transform _currentGrappleFish;

    private bool _isGrappling;

    private void Awake()
    {
        _lineRenderer.enabled = false;
        _swimmingMovement = GetComponent<PlayerSwimmingMovement>();
    }

    private void LateUpdate()
    {
        if (_isGrappling) 
        {
            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, _currentGrappleFish.position);
        }
    }

    public void StartGrapple()
    {
        _isGrappling = true;

        //_swimmingMovement.DisableMovement = true;

        RaycastHit hit;
        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hit, 100, _whatIsGrappleable))
        {
            _currentGrapplePoint = hit.point;
            _currentGrappleFish = hit.transform;

            Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
        }
        else
        {
            _currentGrapplePoint = _firePoint.position + _firePoint.forward * 100;

            Invoke(nameof(StopGrapple), _grappleDelayTime);
        }

        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(1, _currentGrapplePoint);
    }

    public void ExecuteGrapple()
    {
        //_swimmingMovement.DisableMovement = false;

        //Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        //float grapplePointRelativeYPos = _currentGrapplePoint.y - lowestPoint.y;
        //float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        //if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        //_swimmingMovement.JumpToPosition(_currentGrapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        _isGrappling = false;
        //_swimmingMovement.DisableMovement = false;
        _lineRenderer.enabled = false;
    }
}