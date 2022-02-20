using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public enum PlayStatus { 
        skillTab,
        pause,
        ingame
    }

    public PlayStatus playStatus;

    public GameObject skillPanel, pausePanel, inGamePanel;

    public int playerXP;
    public int PlayerXP
    {
        get { return playerXP; }
        set 
        {
            playerXP += value; 
            if(PlayerXP >= nextLevelXP)
            {
                PlayerXP = -nextLevelXP;
                attributePoints++;
            }
            // Do Image fill
        }
    }

    [SerializeField] private int nextLevelXP = 100;
    public int attributePoints = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && playStatus != PlayStatus.pause && !skillPanel.activeInHierarchy)
        {
            //playStatus = PlayStatus.skillTab;
            skillPanel.SetActive(true);
            //inGamePanel.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && playStatus != PlayStatus.pause && skillPanel.activeInHierarchy)
        {
            //playStatus = PlayStatus.ingame;
            //inGamePanel.SetActive(true);
            skillPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !skillPanel.activeInHierarchy)
        {
            playStatus = PlayStatus.pause;
            skillPanel.SetActive(false);
            inGamePanel.SetActive(false);
            pausePanel.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && skillPanel.activeInHierarchy)
        {
            playStatus = PlayStatus.ingame;
            pausePanel.SetActive(false);
            inGamePanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            playerXP += 1000;
        }
    }
}
