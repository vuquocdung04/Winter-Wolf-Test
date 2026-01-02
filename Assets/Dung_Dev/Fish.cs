using DG.Tweening;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int id;
    public float moveDuration = 0.2f;
    public Collider2D coll;
    public bool isFlightToSpot = false;

    public Vector3 originalPos;
    public void Animation()
    {
        float scaleDuration = moveDuration * 0.5f;
        float originalY = transform.localPosition.y;
        float targetMoveY = originalY + 0.5f;

        GamePlayController.instance.playerContain.inputController.SetBusy(true);
        transform.DOLocalMoveY(targetMoveY, moveDuration * 0.5f).OnComplete(delegate
        {
            GamePlayController.instance.playerContain.inputController.SetBusy(false);
            transform.DOScale(Vector3.zero, scaleDuration).OnComplete(delegate
            {
                GamePlayController.instance.playerContain.inputController.SetBusy(false);
                Destroy(gameObject);
            });
        });
    }


    public void ReturnToOriginalPos()
    {
        transform.SetParent(GamePlayController.instance.playerContain.levelGenerator.fishHolder);
        transform.DOLocalMove(originalPos, 0.2f);
    }

    public void MoveToSpot(Spot spot)
    {
        transform.SetParent(spot.transform);
        transform.DOLocalMove(Vector3.zero, moveDuration);
        if (GamePlayController.instance.IsGameModeDefault())
            coll.enabled = false;
        
    }

    public void Setup(Vector3 pos, int i, int j)
    {
        if (coll == null)
            coll = gameObject.AddComponent<BoxCollider2D>();

        transform.position = pos;
        gameObject.name = "Fish: " + i + "_" + j;
    }
}