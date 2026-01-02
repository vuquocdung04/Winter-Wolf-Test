using UnityEngine;

public class Spot : MonoBehaviour
{
    public Fish fish { get; private set; }
    
    public bool IsEmpty() => fish == null;

    public void SetFish(Fish newFish)
    {
        fish = newFish;
    }
}