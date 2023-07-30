using UnityEngine;
using UnityEngine.UI;

public class CatchFishBar : MonoBehaviour
{
    public Slider catchBar;

    private PlayerSwimmingMovement _playerSwimmingMovement;
    private PlayerAttack _playerAttack;
    private Vector3 joystickInputDirection;
    private Vector3 targetDirection;
    private float fillSpeed = 12.0f;

    internal void GetFishInfo(Fish fish)
    {
        targetDirection = (_playerSwimmingMovement.transform.position - fish.transform.position).normalized;
    }

    private void Awake()
    {
        _playerSwimmingMovement = GetComponent<PlayerSwimmingMovement>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        catchBar.value = 0;
    }

    private void Update()
    {
        if (!_playerAttack.startedCatching) return;

        joystickInputDirection = _playerSwimmingMovement.transform.up;

        float dotProduct = Vector3.Dot(joystickInputDirection, targetDirection);

        float fillAmount = (dotProduct + 1) / 2;

        //fillAmount = Mathf.Clamp01(fillAmount);

        float smoothStepFillAmount = Mathf.SmoothStep(catchBar.value, fillAmount, Time.deltaTime * fillSpeed);

        catchBar.value = smoothStepFillAmount;

        if (catchBar.value > 0.99f)
        {
            _playerAttack.catchSuccesfull = true;
            _playerAttack.CheckIfSuccesfull();
        }
    }
}