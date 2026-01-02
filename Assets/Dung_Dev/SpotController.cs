using System.Collections.Generic;
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
        
        
        int indexEmpty = GetFirstEmptySpot();
        
        spots[indexEmpty].SetFish(fishSelected);
        fishSelected.MoveToSpot(spots[indexEmpty]);
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