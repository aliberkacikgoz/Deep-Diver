using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class FloatingJoystick : MonoBehaviour
{
    [SerializeField]
    private MovementListenerSO _movementListener;

    [SerializeField]
    private Vector2 _joystickSize = new Vector2(300, 300);

    [SerializeField]
    private RectTransform _knobRect;

    private RectTransform _joystickRect;
    private CanvasGroup _canvasGroup;

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

    private void Awake()
    {
        _joystickRect = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger != _movementFinger)
            return;

        HandleJoystickKnobPosition(MovedFinger);
    }

    private void HandleJoystickKnobPosition(Finger MovedFinger)
    {
        Vector2 knobPosition;

        //max movement of knob is radius of joystick
        float maxMovementOfKnob = _joystickSize.x / 2f;
        ETouch.Touch currentTouch = MovedFinger.currentTouch;

        //calculates the position of knob if it doesnt extend
        knobPosition = currentTouch.screenPosition - _joystickRect.anchoredPosition;

        //Joystick ortasý ve parmaðýn dokunduðu yer arasý uzaklýðý hesaplýyor max hareketten 
        //fazlaysa knobun pozisyonunu çemberin parmaða en yakýn hizasýna yerleþtiriyor
        if (Vector2.Distance(currentTouch.screenPosition, _joystickRect.anchoredPosition)
            > maxMovementOfKnob)
        {
            knobPosition = (currentTouch.screenPosition - _joystickRect.anchoredPosition)
                .normalized * maxMovementOfKnob;
        }

        //reset to old position
        _knobRect.anchoredPosition = knobPosition;
        _movementAmount = knobPosition / maxMovementOfKnob;

        _movementListener.Move(_movementAmount);
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == _movementFinger)
        {
            _movementFinger = null;

            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;

            _knobRect.anchoredPosition = Vector2.zero;
            _movementAmount = Vector2.zero;
            _movementListener.Move(_movementAmount);
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        //ekranný sol köþesisen sabitliyor buna bak
        if (_movementFinger == null)
        {
            _movementFinger = TouchedFinger;

            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;

            _movementAmount = Vector2.zero;
            _movementListener.Move(_movementAmount);

            _joystickRect.sizeDelta = _joystickSize;
            _joystickRect.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    //Clamp ederken saðý da kontrol et
    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < _joystickSize.x / 2)
            StartPosition.x = _joystickSize.x / 2;

        if (StartPosition.x > Screen.width - _joystickSize.x / 2)
            StartPosition.x = Screen.width - _joystickSize.x / 2;


        if (StartPosition.y < _joystickSize.y / 2)
            StartPosition.y = _joystickSize.y / 2;

        if (StartPosition.y > Screen.height - _joystickSize.y / 2)
            StartPosition.y = Screen.height - _joystickSize.y / 2;

        return StartPosition;
    }

}
