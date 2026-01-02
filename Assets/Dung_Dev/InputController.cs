using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mouseWorldPos;
    public void Init()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GetMousePos();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if(hit.collider == null) return;
            
            Fish fishDetected = hit.collider.GetComponent<Fish>();
            if (fishDetected == null) return;
            
            GamePlayController.instance.playerContain.spotController.OnFishSelected(fishDetected);
            
        }
    }

    private void GetMousePos()
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
    }
}