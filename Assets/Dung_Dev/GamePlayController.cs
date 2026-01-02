using System;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    public GameScene gameScene;
    public PlayerContain playerContain;
    
    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        gameScene.Init();
        playerContain.Init();
    }
}