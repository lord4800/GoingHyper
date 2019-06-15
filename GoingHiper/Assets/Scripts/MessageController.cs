using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    static MessageController instance;
    static public MessageController Instance
    {
        get { return instance; }
    }
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text gameWinText;

    private void Awake()
    {
        instance = this;
    }

    public void GameOverEvent()
    {
        gameOverText.enabled = true;
    }

    public void GameWinEvent()
    {
        gameWinText.enabled = true;
    }
}
