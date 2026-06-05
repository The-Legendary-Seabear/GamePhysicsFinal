using UnityEngine;
using TMPro;

public class WinLose : MonoBehaviour
{
    public TextMeshProUGUI displayText;

    void Start()
    {
        if(GameManager.Instance.finalScore >= 900)
        {
            displayText.text = "You Win!";
        } else
        {
            displayText.text = "You Lose...";
        }

        GameManager.Instance.ResetGame();
        
    }

}
