using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private GameObject headerGanvas;
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text textPower;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text txtHighScore;
    [SerializeField] private TMP_Text txtNuks;
    [SerializeField] private GameObject nukeImg;
    [SerializeField] private Transform nukesBar;
     [SerializeField] private Slider lifeSlider;
    [SerializeField] private Slider powerSlider;

    [Header("Menu")]
    [SerializeField] private GameObject menuGanvas;
    [SerializeField] private GameObject lblTitle;
    [SerializeField] private GameObject lblGameOver;
    [SerializeField] private TMP_Text txtMenuHighScore;    

    private Player player;
    private ScoreManager scoreManager;
    

    // Start is called before the first frame update
    void Start()
    {
        lblTitle.SetActive(true);
        lblGameOver.SetActive(false);
        headerGanvas.SetActive(false);
        menuGanvas.SetActive(true);

        // Setting max values for the sliders
        lifeSlider.maxValue = 100f;
        powerSlider.maxValue = 5f;

        Debug.Log("Running start");

        //scoreManager = GameManager.GetInstance().scoreManager;

        GameManager.GetInstance().OnGameStart += GameStarted;
        GameManager.GetInstance().OnGameOver += GameOver;
    }

    public void UpdateHealth(float currentHealth)
    {
        float roundedHealthValue = (float)System.Math.Round(currentHealth, 0); // Round health value
        txtHealth.SetText(roundedHealthValue.ToString());

        lifeSlider.value = roundedHealthValue; // Showing current health
    }

    public void UpdateGunPower(float currentGunPower)
    {
        float roundedPowerValue = (float)System.Math.Round(currentGunPower, 1); // Round gun power value
        textPower.SetText(roundedPowerValue.ToString());

        powerSlider.value = roundedPowerValue; // Showing current gun power
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

    public void GameStarted()
    {
        player = GameManager.GetInstance().GetPlayer();

        player.health.OnHealthUpdate += UpdateHealth;
        player.nuke.OnNuke += UpdateNuke;
        player.gunPower.OnGunPowerUpdate += UpdateGunPower;

        menuGanvas.SetActive(false);
        headerGanvas.SetActive(true);
    }

    public void GameOver()
    {
        lblTitle.SetActive(false);
        lblGameOver.SetActive(true);
        headerGanvas.SetActive(false);
        menuGanvas.SetActive(true);
    }
}
