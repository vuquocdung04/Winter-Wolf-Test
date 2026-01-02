using UnityEngine;

public class Fish : MonoBehaviour
{
    public int id;
    public Collider2D coll;

    public void Setup(Vector3 pos, int i, int j)
    {
        if(coll == null)
            coll = gameObject.AddComponent<BoxCollider2D>();
        
        transform.position = pos;
        gameObject.name = "Fish: " + i + "_" + j;
    }
}