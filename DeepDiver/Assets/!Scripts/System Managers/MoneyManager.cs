using TMPro;
using UnityEngine;

public class MoneyManager : MonoSingleton<MoneyManager>
{
    [Header("Money UI Element")]
    [SerializeField] private TextMeshProUGUI _moneyTMP;
    [Header("Current Player Money")]
    [SerializeField] private int _currentPlayerMoney;
    public int CurrentPlayerMoney 
    {
        get
        {
            return _currentPlayerMoney;
        }
    }

    [Header("Money Change Lerp Time")]
    [SerializeField]private float _lerpTime;

    private int _startMoney;
    private int _endMoney;
    private float _timeStartedLerping;

    private bool timerRunning = false;
    private float _timer;

    private void Awake()
    {
        _timer = _lerpTime;
    }

    private void Start()
    {
        _currentPlayerMoney = SaveManager.Instance.LoadPlayerMoney();
        _moneyTMP.text = _currentPlayerMoney.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GainMoney(100);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SpendMoney(100);
        }

        if (!timerRunning) return;

        _timer -= Time.smoothDeltaTime;
        if (_timer >= 0)
        {
            _moneyTMP.text = LerpInteger(_startMoney, _endMoney, _timeStartedLerping, _lerpTime).ToString();
        }
        else
        {
            timerRunning = false;
            _moneyTMP.text = _currentPlayerMoney.ToString();
            _timer = _lerpTime;
        }
    }

    public void GainMoney(int gainAmount)
    {
        StartLerping(+gainAmount);
        _currentPlayerMoney += gainAmount;
        SaveManager.Instance.SavePlayerMoney(_currentPlayerMoney);
    }

    public void SpendMoney(int spendAmount)
    {
        StartLerping(-spendAmount);
        _currentPlayerMoney -= spendAmount;
        SaveManager.Instance.SavePlayerMoney(_currentPlayerMoney);
    }

    private void StartLerping(int changeAmount)
    {
        _timeStartedLerping = Time.time;
        _startMoney = _currentPlayerMoney;
        _endMoney = _currentPlayerMoney + changeAmount;
        timerRunning = true;
    }

    private int LerpInteger(int start, int end, float timeStarted, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStarted;
        float percentageComplete = timeSinceStarted / lerpTime;

        int result = (int)Mathf.Lerp(start, end, percentageComplete);

        return result;
    }
}