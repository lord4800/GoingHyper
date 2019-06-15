using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] BallController ball;
    [SerializeField] Text winText;

    // Start is called before the first frame update
    void Start()
    {
        ball.WinAction += ShowWin;
    }

    void ShowWin()
    {
        winText.enabled = true;
    }
}
