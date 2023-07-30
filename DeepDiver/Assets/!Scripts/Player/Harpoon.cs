using UnityEngine;

[RequireComponent(typeof(Rope))]
public class Harpoon : MonoBehaviour
{
    public Transform firePoint;
    [SerializeField] private LayerMask _whatIsGrappleable;
    [SerializeField] private float _grappleDelayTime;
    [SerializeField] private float overshootYAxis;
    [SerializeField] private LineRenderer _lineRenderer;
    
    private PlayerSwimmingMovement _swimmingMovement;
    public Vector3 currentGrapplePoint;
    private Transform _currentGrappleFish;

    public bool isGrappling;

    private void Awake()
    {
        _lineRenderer.enabled = false;
        _swimmingMovement = GetComponent<PlayerSwimmingMovement>();
    }

    //private void LateUpdate()
    //{
    //    if (isGrappling) 
    //    {
    //        _lineRenderer.SetPosition(0, firePoint.position);
    //        _lineRenderer.SetPosition(1, _currentGrappleFish.position);
    //    }
    //}

    public void StartGrapple()
    {
        isGrappling = true;

        //_swimmingMovement.DisableMovement = true;

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100, _whatIsGrappleable))
        {
            _currentGrappleFish = hit.transform;
            currentGrapplePoint = _currentGrappleFish.position;

            //Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
            _lineRenderer.enabled = true;
        }
        else
        {
            _lineRenderer.enabled = false;
        }
        //_lineRenderer.SetPosition(1, currentGrapplePoint);
    }

    public void ExecuteGrapple()
    {
        //_swimmingMovement.DisableMovement = false;

        //Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        //float grapplePointRelativeYPos = _currentGrapplePoint.y - lowestPoint.y;
        //float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        //if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        //_swimmingMovement.JumpToPosition(_currentGrapplePoint, highestPointOnArc);

        //Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        isGrappling = false;
        _lineRenderer.enabled = false;
        //_swimmingMovement.DisableMovement = false;
    }
}