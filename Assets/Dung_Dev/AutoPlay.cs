using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    [ContextMenu("Auto Win")]
    public void AutoWin()
    {
        StartCoroutine(AutoWinRoutine());
    }

    private IEnumerator AutoWinRoutine()
    {
        yield return new WaitForSeconds(1f);
        var spotController = GamePlayController.instance.playerContain.spotController;
        var levelController = GamePlayController.instance.playerContain.levelGenerator;
        
        Fish[] fishes = levelController.fishHolder.GetComponentsInChildren<Fish>();
        Dictionary<int, List<Fish>> dictFishes = new Dictionary<int, List<Fish>>();

        foreach (var fish in fishes)
        {
            int id =  fish.id;
            if(!dictFishes.ContainsKey(id))
                dictFishes.Add(id, new List<Fish>());
            dictFishes[id].Add(fish);
        }

        foreach (var fishKey in dictFishes.Keys)
        {
            List<Fish> listSameType = dictFishes[fishKey];
            while (listSameType.Count >= 3)
            {
                var fish0 = listSameType[0];
                var fish1 = listSameType[1];
                var fish2 = listSameType[2];
                
                spotController.OnFishSelected(fish0);
                // reason moveDuration + 0.3f = 0.5f. because moveDuration = 0.2f(config);
                yield return new WaitForSeconds(fish0.moveDuration + 0.3f);
                spotController.OnFishSelected(fish1);
                yield return new WaitForSeconds(fish1.moveDuration + 0.3f);
                spotController.OnFishSelected(fish2);
                yield return new WaitForSeconds(fish2.moveDuration + 0.3f);
                listSameType.RemoveAt(0);
                listSameType.RemoveAt(0);
                listSameType.RemoveAt(0);
            }
        }
        GamePlayController.instance.gameScene.ShowWinPopup();
        Debug.Log("Auto Win completed");
    }

    [ContextMenu("Auto Lose")]
    public void AutoLose()
    {
        StartCoroutine(AutoLoseRoutine());
    }

    private IEnumerator AutoLoseRoutine()
    {
        yield return new WaitForSeconds(1f);
        var spotController = GamePlayController.instance.playerContain.spotController;
        var levelController = GamePlayController.instance.playerContain.levelGenerator;
        
        Fish[] fishes = levelController.fishHolder.GetComponentsInChildren<Fish>();
        Dictionary<int, List<Fish>> dictFishes = new Dictionary<int, List<Fish>>();

        int totalSpotFull = 0;
        foreach (var fish in fishes)
        {
            if (totalSpotFull >= spotController.spots.Count) break;
            int id =  fish.id;
            if(!dictFishes.ContainsKey(id))
                dictFishes.Add(id, new List<Fish>());

            if (dictFishes[id].Count < 2)
            {
                spotController.OnFishSelected(fish);
                dictFishes[id].Add(fish);
                totalSpotFull++;
                yield return new WaitForSeconds(0.5f);
            }
        }
        GamePlayController.instance.gameScene.ShowLosePopup();
        Debug.Log("Auto Lose completed");
    }
}