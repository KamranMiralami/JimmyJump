using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnockOutBehaviour : MonoBehaviour
{
    public GameObject gameHandler;
    public TextMeshProUGUI ShootText;
    public int numberOfHits=0;
    public int goal=1;
    public void Hit()
    {
        if (numberOfHits < goal)
        {
            numberOfHits++;
            ShootText.text=numberOfHits.ToString();
        }
        else
        {
            return;
        }
        if (numberOfHits >= goal) {
            gameHandler.GetComponent<GameHandlerScript>().win();
        }
    }
}
