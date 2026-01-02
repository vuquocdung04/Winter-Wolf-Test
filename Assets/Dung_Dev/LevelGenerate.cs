using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    public Transform fishHolder;

    public int boardSizeX = 4;
    public int boardSizeY = 6;
    public float spacing = 1;
    
    public List<Fish> fishPrefabs = new List<Fish>();
    public void Init()
    {
        
    }

    [ContextMenu("Generate Level")]
    private void GenerateLevel()
    {
        ClearLevel();
        Vector3 origin = new Vector3(-boardSizeX * 0.5f + 0.5f, -boardSizeY * 0.5f + 0.5f, 0f);

        for (int i = 0; i < boardSizeY; i++)
        {
            for (int j = 0; j < boardSizeX; j++)
            {
                Fish fishPrefab = fishPrefabs[0];
                Fish newFish = Instantiate(fishPrefab, fishHolder);
                Vector3 fishPos =  origin + new Vector3(j * spacing, i * spacing, 0f);
                newFish.Setup(fishPos,i,j);
            }
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