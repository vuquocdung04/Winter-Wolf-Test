using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpotController : MonoBehaviour
{
    public Transform spotHolder;
    public int totalSpotNeeded = 4;

    public List<Spot> spots = new List<Spot>();
    
    public void Init()
    {
    }

    public void OnFishSelected(Fish fishSelected)
    {
        if (IsSpotFull())
        {
            Debug.Log("Spot is full");
            return;
        }
        int indexEmpty = FindInsertSameType(fishSelected.id);
        
        MoveAllToRight(indexEmpty);
        
        spots[indexEmpty].SetFish(fishSelected);
        fishSelected.MoveToSpot(spots[indexEmpty]);

        DOVirtual.DelayedCall(fishSelected.moveDuration + 0.01f, delegate
        {
            HandleMatch3();
        });
    }

    private void HandleMatch3()
    {
        List<Fish> listToDestroy =  new List<Fish>();

        for (int i = 0; i < spots.Count - 2; i++)
        {
            if (!spots[i].IsEmpty() && !spots[i + 1].IsEmpty() && !spots[i + 2].IsEmpty())
            {
                var fish0 = spots[i].fish;
                var fish1 = spots[i + 1].fish;
                var fish2 = spots[i + 2].fish;

                if (fish0.id == fish1.id && fish2.id == fish1.id)
                {
                    listToDestroy.Add(fish0);
                    listToDestroy.Add(fish1);
                    listToDestroy.Add(fish2);
                    
                    spots[i].SetFish(null);
                    spots[i + 1].SetFish(null);
                    spots[i + 2].SetFish(null);
                }
            }
        }
        if (listToDestroy.Count >= 3)
        {
            for(int i = listToDestroy.Count - 1; i >= 0; i--)
                listToDestroy[i].Animation();
            
            GamePlayController.instance.playerContain.levelGenerator.HandleWin();
            MoveAllToLeft();
            Debug.Log("Match 3");
        }
        else
        {
            if (IsSpotFull())
            {
                GamePlayController.instance.gameScene.ShowLosePopup();
            }
        }
    }

    private void MoveAllToLeft()
    {
        List<Fish> activeFishInSpot = new List<Fish>();
        
        for (int i = 0; i < spots.Count; i++)
        {
            if (!spots[i].IsEmpty())
            {
                activeFishInSpot.Add(spots[i].fish);
                spots[i].SetFish(null);
            }
        }

        for (int i = 0; i < activeFishInSpot.Count; i++)
        {
            spots[i].SetFish(activeFishInSpot[i]);
            activeFishInSpot[i].MoveToSpot(spots[i]);
        }
    }
    
    private void MoveAllToRight(int insertIndex)
    {
        int firstEmptySpot = GetFirstEmptySpot();
        if(firstEmptySpot == insertIndex)  return;

        for (int i = firstEmptySpot; i > insertIndex; i--)
        {
            Spot currentSpot = spots[i];
            Spot prevSpot = spots[i - 1];
            
            currentSpot.SetFish(prevSpot.fish);
            prevSpot.SetFish(null);
            
            currentSpot.fish.MoveToSpot(currentSpot);
        }
    }

    private int FindInsertSameType(int fishId)
    {
        int lastIndexSameType = -1;
        
        for (int i = 0; i < spots.Count; i++)
        {
            if (!spots[i].IsEmpty() && spots[i].fish.id == fishId)
            {
                lastIndexSameType = i;
            }
        }

        if (lastIndexSameType == -1)
            return GetFirstEmptySpot();
        
        return lastIndexSameType + 1;
    }
    

    private int GetFirstEmptySpot()
    {
        for (int i = 0; i < spots.Count; i++)
        {
            if(spots[i].IsEmpty()) return i;
        }
        Debug.LogError("Not found empty spot");
        return 2026;
    }
    
    private bool IsSpotFull()
    {
        foreach (var spot in spots)
        {
            if(spot.IsEmpty()) return false;
        }
        return true;
    }


    [ContextMenu("Generate Spot")]
    private void GenerateSpot()
    {
        ClearSpot();
        Transform spot0 = spotHolder.GetChild(0);
        Vector3 origin = spot0.position;

        float lastPosX = -origin.x;
        float totalDistance = Mathf.Abs(lastPosX - origin.x);
        float spacing = totalDistance / totalSpotNeeded;

        for (int i = 0; i < totalSpotNeeded; i++)
        {
            float posX = (i + 1) * spacing;
            Spot spot = spot0.GetComponent<Spot>();
            var newSpot = Instantiate(spot, spotHolder);
            Vector3 newSpotPos = origin + new Vector3(posX, 0, 0);
            newSpot.transform.position = newSpotPos;
            newSpot.gameObject.name = "Spot: " + (i + 1);
        }

        foreach (Transform trans in spotHolder)
        {
            spots.Add(trans.GetComponent<Spot>());
        }
    }

    private void ClearSpot()
    {
        for (int i = spotHolder.childCount - 1; i > 0; i--)
        {
            DestroyImmediate(spotHolder.GetChild(i).gameObject);
        }
    }
}