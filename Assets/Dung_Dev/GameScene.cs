using UnityEngine;

public class GameScene : MonoBehaviour
{
    public Transform winPopup;
    public Transform losePopup;
    
    public void Init()
    {
          winPopup.gameObject.SetActive(false);
          losePopup.gameObject.SetActive(false);
    }
    
    public void ShowWinPopup() => winPopup.gameObject.SetActive(true);
    public void ShowLosePopup() => losePopup.gameObject.SetActive(true);
}