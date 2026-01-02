using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mouseWorldPos;

    public bool isBusy { get; private set; }
    

    public void Init()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isBusy) return;

        GetMousePos();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider == null) return;
            Fish fishDetected = hit.collider.GetComponent<Fish>();
            if (fishDetected != null)
            {
                if (!fishDetected.isFlightToSpot)
                {
                    GamePlayController.instance.playerContain.spotController.OnFishSelected(fishDetected);
                }
                else
                {
                    GamePlayController.instance.playerContain.spotController.OnSpotFishSelected(fishDetected);
                }
            }
        }
    }

    public bool SetBusy(bool state) => isBusy = state;

    private void GetMousePos()
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
    }
}