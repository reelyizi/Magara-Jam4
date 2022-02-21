using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public Image healthBar;
    public int playerHealth = 300;
    private int totalHealth;
    public int PlayerHealth
    {
        get { return playerHealth; }
        set 
        { 
            playerHealth += value;
            healthBar.fillAmount = 1 - (((float)totalHealth - (float)playerHealth) / (float)totalHealth);
            if (value < 0 && Random.value > 0.5f)
                AudioManager.instance.AudioPlay("PlayerGetDamage");
        }
    }

    [SerializeField] private int nextLevelXP = 100;
    public int attributePoints = 0;

    private void Start()
    {
        totalHealth = playerHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && playStatus != PlayStatus.pause && !skillPanel.activeInHierarchy)
        {
            //playStatus = PlayStatus.skillTab;
            Debug.Log("A");
            skillPanel.SetActive(true);
            //inGamePanel.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && playStatus != PlayStatus.pause && skillPanel.activeInHierarchy)
        {
            //playStatus = PlayStatus.ingame;
            //inGamePanel.SetActive(true);
            skillPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            playerXP += 1000;
        }
    }
}
