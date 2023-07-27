using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    //TODO: Rearrange joystick size to floating joystick scripts
    [SerializeField]
    private Vector2 JoystickSize = new Vector2(300, 300);

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _playerSpeed;

    [SerializeField]
    private FloatingJoystick Joystick;

    private Finger _movementFinger;
    private Vector2 _movementAmount;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == _movementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(
                    currentTouch.screenPosition,
                    Joystick.RectTransform.anchoredPosition
                ) > maxMovement)
            {
                knobPosition = (
                    currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition
                    ).normalized
                    * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            _movementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == _movementFinger)
        {
            _movementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            _movementAmount = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        //ekranný sol köþesisen sabitliyor buna bak
        if (_movementFinger == null)
        {
            _movementFinger = TouchedFinger;
            _movementAmount = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.sizeDelta = JoystickSize;
            Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }


    //Clamp ederken saðý da kontrol et
    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < JoystickSize.x / 2)
            StartPosition.x = JoystickSize.x / 2;

        if (StartPosition.x > Screen.width - JoystickSize.x / 2)
            StartPosition.x = Screen.width - JoystickSize.x / 2;
        

        if (StartPosition.y < JoystickSize.y / 2)
            StartPosition.y = JoystickSize.y / 2;
        
        if (StartPosition.y > Screen.height - JoystickSize.y / 2)
            StartPosition.y = Screen.height - JoystickSize.y / 2;

        return StartPosition;
    }

    private void Update()
    {
        //if its normal walk it will be in x and z axis
        Vector3 scaledMovement = _playerSpeed * Time.deltaTime * new Vector3(
            _movementAmount.x,
            0,
            _movementAmount.y

        );

        //if its swimming it will be in x and y axis
        /*
        Vector3 scaledMovement = _playerSpeed * Time.deltaTime * new Vector3(
            _movementAmount.x,
            _movementAmount.y,
            0

        );
        */

        transform.LookAt(transform.position + scaledMovement, Vector3.up);

        //Rigidbody ile hareket iþini burda ya
        _rigidbody.MovePosition(transform.position + scaledMovement);
        //_player.Move(scaledMovement);
    }

 

}
