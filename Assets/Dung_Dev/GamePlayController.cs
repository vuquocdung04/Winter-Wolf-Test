using System;
using UnityEngine;

public enum GameMode
{
    Default = 0,
    AttackTime = 1,
}

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    public GameMode gameMode;
    public GameScene gameScene;
    public PlayerContain playerContain;
    public AutoPlay autoPlay;
    
    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        gameScene.Init();
        playerContain.Init();
        gameMode = GameMode.Default;
    }

    public void SetGameModeDefault() => gameMode = GameMode.Default;
    public void SetGameModeAttackTime() => gameMode = GameMode.AttackTime;
    
    public bool IsGameModeDefault() => gameMode == GameMode.Default;
    public bool IsGameModeTimeAttack() => gameMode == GameMode.AttackTime;
}