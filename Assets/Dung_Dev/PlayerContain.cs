
using UnityEngine;

public class PlayerContain : MonoBehaviour
{
    public InputController inputController;
    public LevelGenerate levelGenerator;
    public SpotController  spotController;
    public void Init()
    {
        inputController.Init();
        levelGenerator.Init();

        spotController.Init();
    }
}