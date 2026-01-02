using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    public Transform fishHolder;

    public int boardSizeX = 4;
    public int boardSizeY = 6;
    public float spacing = 1;
    public List<Fish> fishPrefabs = new List<Fish>();

    private int totalWin;
    public int currentWin;
    public void Init()
    {
        totalWin = (boardSizeY *  boardSizeX) / 3;
    }

    public void HandleWin()
    {
        currentWin++;
        if (currentWin == totalWin)
        {
            GamePlayController.instance.gameScene.ShowWinPopup();
        }
    }
    

    [ContextMenu("Generate Level")]
    private void GenerateLevel()
    {
        ClearLevel();
        
        if (!DivisibleCondition()) return;

        Vector3 origin = new Vector3(-boardSizeX * 0.5f + 0.5f, -boardSizeY * 0.5f + 0.5f, 0f);

        List<Fish> fishes = AvailableBoard(boardSizeX * boardSizeY);
        int indexFish = 0;
        
        for (int i = 0; i < boardSizeY; i++)
        {
            for (int j = 0; j < boardSizeX; j++)
            {
                Fish fishPrefab = fishes[indexFish];
                Fish newFish = Instantiate(fishPrefab, fishHolder);
                Vector3 fishPos =  origin + new Vector3(j * spacing, i * spacing, 0f);
                newFish.Setup(fishPos,i,j);
                indexFish++;
            }
        }
    }

    private bool DivisibleCondition()
    {
        int totalFish = boardSizeY *  boardSizeX;
        if(totalFish % 3 == 0) return true;
        return false;
    }

    private List<Fish> AvailableBoard(int total)
    {
        List<Fish> pool = new List<Fish>();
        for (int i = 0; i < fishPrefabs.Count; i++)
        {
            Fish fish = fishPrefabs[i];

            pool.Add(fish);
            pool.Add(fish);
            pool.Add(fish);
        }

        while (pool.Count < total)
        {
            int rand =  Random.Range(0, fishPrefabs.Count);
            Fish fish = fishPrefabs[rand];
            pool.Add(fish);
            pool.Add(fish);
            pool.Add(fish);
        }
        Shuffle(pool);
        return pool;
    }

    private void Shuffle(List<Fish> fishes)
    {
        int i = fishes.Count;
        while (i > 1)
        {
            i--;
            int k = Random.Range(0, i+1);
            (fishes[i], fishes[k]) = (fishes[k], fishes[i]);
        }
    }


    private void ClearLevel()
    {
        for (int i = fishHolder.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(fishHolder.GetChild(i).gameObject);
        }
    }
    
}