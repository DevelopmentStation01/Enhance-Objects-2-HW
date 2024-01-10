using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text txtHighScore;
    [SerializeField] private TMP_Text txtNuks;
    [SerializeField] private GameObject nukeImg;
    [SerializeField] private Transform nukesBar;

    [Header("Menu")]
    [SerializeField] private GameObject menuGanvas;
    [SerializeField] private GameObject lblGameOver;
    [SerializeField] private TMP_Text txtMenuHighScore;

    [Header("TEST")]
    public TMP_Text textTest;

    private Player player;
    private ScoreManager scoreManager;
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Running start");

        //scoreManager = GameManager.GetInstance().scoreManager;

        GameManager.GetInstance().OnGameStart += GameStarted;
        GameManager.GetInstance().OnGameOver += GameOver;
    }

    public void UpdateHealth(float currentHealth)
    {
        txtHealth.SetText(currentHealth.ToString());
    }

    public void UpdateNuke(int nukes)
    {
        if (nukes > nukesBar.transform.childCount) // Add missing instances, when nuke collected
        {
            int missingInstances = nukes - nukesBar.transform.childCount;
            for (int i = 0; i < missingInstances; i++)
            {
                Instantiate(nukeImg, nukesBar);
            }
        }
        else if (nukes < nukesBar.transform.childCount) // Remove extra instances, when nuke was used
        {
            if (nukesBar.transform.childCount > 0)
                Destroy(nukesBar.transform.GetChild(0).gameObject);
        }
    }

    public void UpdateGunPower(float currentGunPower)
    {
        textTest.SetText(currentGunPower.ToString());
    }

    public void UpdateScore()
    {
        txtScore.SetText(GameManager.GetInstance().scoreManager.GetScore().ToString());
    }

    public void UpdateHighScore()
    {
        Debug.Log("Running update");
        
        txtHighScore.SetText(GameManager.GetInstance().scoreManager.GetHighScore().ToString());
        txtMenuHighScore.SetText($"High Score: {GameManager.GetInstance().scoreManager.GetHighScore()}");
    }

    public void GameStarted()
    {
        player = GameManager.GetInstance().GetPlayer();

        player.health.OnHealthUpdate += UpdateHealth;
        player.nuke.OnNuke += UpdateNuke;
        player.gunPower.OnGunPowerUpdate += UpdateGunPower;

        menuGanvas.SetActive(false);
    }

    public void GameOver()
    {
        lblGameOver.SetActive(true);
        menuGanvas.SetActive(true);
    }
}
