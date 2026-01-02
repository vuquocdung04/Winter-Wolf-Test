using UnityEngine;

public class Spot : MonoBehaviour
{
    public Fish fish;
    
    public bool IsEmpty() => fish == null;

    public void SetFish(Fish newFish)
    {
        fish = newFish;
    }
}