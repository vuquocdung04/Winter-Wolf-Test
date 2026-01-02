using DG.Tweening;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int id;
    public float moveDuration = 0.2f;
    public Collider2D coll;


    public void MoveToSpot(Spot spot)
    {
        transform.SetParent(spot.transform);
        transform.DOLocalMove(Vector3.zero, moveDuration);
        
        coll.enabled = false;
    }

    public void Setup(Vector3 pos, int i, int j)
    {
        if(coll == null)
            coll = gameObject.AddComponent<BoxCollider2D>();
        
        transform.position = pos;
        gameObject.name = "Fish: " + i + "_" + j;
    }
}