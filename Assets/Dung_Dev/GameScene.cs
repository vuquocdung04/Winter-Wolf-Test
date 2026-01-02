using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public Transform winPopup;
    public Transform losePopup;
    public Transform panelHome;
    public Button btnDefault;
    public Button btnAutoWin;
    public Button btnAutoLose;
    public Button btnAttackTime;

    [Header("Time Config")] public float totalTime = 60f;
    public TextMeshProUGUI timeText;
    private float timeRemaining;

    private bool isGameModeAttackTime;
    private bool isTimeRunning;

    public void Init()
    {
        panelHome.gameObject.SetActive(true);
        winPopup.gameObject.SetActive(false);
        losePopup.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);

        isGameModeAttackTime = false;
        timeRemaining = totalTime;

        btnDefault.onClick.AddListener(delegate
        {
            GamePlayController.instance.playerContain.inputController.CanPlayBeforeOneSecond();
            
            panelHome.gameObject.SetActive(false);
        });
        btnAutoWin.onClick.AddListener(delegate
        {
            GamePlayController.instance.playerContain.inputController.CanPlayBeforeOneSecond();
            panelHome.gameObject.SetActive(false);
            GamePlayController.instance.autoPlay.AutoWin();
        });

        btnAutoLose.onClick.AddListener(delegate
        {
            panelHome.gameObject.SetActive(false);
            GamePlayController.instance.playerContain.inputController.CanPlayBeforeOneSecond();

            GamePlayController.instance.autoPlay.AutoLose();

        });
        btnAttackTime.onClick.AddListener(delegate
        {
            GamePlayController.instance.playerContain.inputController.CanPlayBeforeOneSecond();
            
            GamePlayController.instance.SetGameModeAttackTime();
            isGameModeAttackTime = true;
            isTimeRunning = true;
            timeText.gameObject.SetActive(true);
            panelHome.gameObject.SetActive(false);
        });
    }

    public void Update()
    {
        if (!isTimeRunning) return;
        if (!isGameModeAttackTime) return;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI(timeRemaining);
        }
        else
        {
            isTimeRunning = false;
            timeRemaining = 0;
            GamePlayController.instance.playerContain.inputController.isLose = true;
            ShowLosePopup();
        }
    }

    private void UpdateTimerUI(float timeDisplay)
    {
        timeDisplay += 1;
        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void ShowWinPopup() => winPopup.gameObject.SetActive(true);
    public void ShowLosePopup() => losePopup.gameObject.SetActive(true);
}