using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PlayerTemperatureHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float minTemp = -1f;
    [SerializeField] private float maxTemp = 1f;
    [SerializeField] private float tempChangeSpeed = 0f;
    [SerializeField] private float barFillSpeed = 0f;


    [Header("Components Reference")]
    [SerializeField] private SkinnedMeshRenderer meshRenderer = null;
    [SerializeField] private TextMeshPro temperatureTxt = null;
    [SerializeField] private TextMeshProUGUI tempText = null;
    [SerializeField] private GameObject flamePS = null;
    [SerializeField] private List<ParticleSystem> flames = null;


    [SerializeField] private Image tempBar = null;


    private Material playerMat = null;
    [SerializeField] private float playerTemperature = 0f;
    #endregion

    #region Delegates
    public delegate void TempDecrementMechanism();

    public TempDecrementMechanism tempChangeMechanism;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        playerMat = meshRenderer.material;
        InitialSettings();
        tempChangeMechanism = null;
       // tempBar.fillAmount = Mathf.Lerp(tempBar.fillAmount, playerTemperature / maxTemp, barFillSpeed);
        playerTemperature = 0;
        UpdatePlayerTemperature(0);
        UpdateText();
    }

    private void Update()
    {
        if (tempChangeMechanism != null)
        {
            tempChangeMechanism();
        }
        tempBar.fillAmount = Mathf.Lerp(tempBar.fillAmount, playerTemperature / maxTemp, barFillSpeed);

    }
    #endregion

    #region Getter And Setter
    public ObstaclesHandler ActiveStuckedObstacle { get; set; }

    public float GetPlayerTemperature { get => playerTemperature; }
    #endregion

    #region Private Core Functions
    private void InitialSettings()
    {
        playerTemperature = 0;
        playerMat.SetFloat("_FillAmount", playerTemperature);
    }

    private void TempDecrement()
    {
        playerTemperature -= tempChangeSpeed * Time.deltaTime;
        TempTxtUpdate();
        
        if (ActiveStuckedObstacle)
        {
            ActiveStuckedObstacle.ChangeObstacleTemperature(tempChangeSpeed, false);
        }
    }

    private void TempIncrement()
    {
        playerTemperature += tempChangeSpeed * Time.deltaTime;
        TempTxtUpdate();
        if (ActiveStuckedObstacle)
        {
            ActiveStuckedObstacle.ChangeObstacleTemperature(tempChangeSpeed, true);
        }
    }

    private void TempTxtUpdate()
    {
        if (playerTemperature > 0)
        {
            temperatureTxt.text = ((int)playerTemperature + " C");
            playerMat.SetFloat("_EnterColdTransition", 0);
            playerMat.SetFloat("_Texture_2_BS", playerTemperature / maxTemp);
            playerMat.SetFloat("_EmissionPower", 2f);
        }
        else if (playerTemperature <= 0)
        {
            temperatureTxt.text = ("-" + (int)playerTemperature + " C");
            playerMat.SetFloat("_EnterColdTransition", 1);
            playerMat.SetFloat("_Texture_1_BS", -(playerTemperature) / maxTemp);
            playerMat.SetFloat("_EmissionPower", 1f);
        }
        UpdateText();
      //  temperatureTxt.text = ((int)playerTemperature + " C");
        //playerMat.SetFloat("_FillAmount", playerTemperature / maxTemp);        
    }

    void UpdateText()
    {
        if (playerTemperature >= 0 && playerTemperature < 20)
        {
            tempText.text = "COLD!";
        }
        if (playerTemperature >= 20 && playerTemperature < 40)
        {
            tempText.text = "WARM!";
        }

        if (playerTemperature >= 40 && playerTemperature < 70)
        {
            tempText.text = "HOT!";
        }
        if (playerTemperature >= 70 && playerTemperature < 100)
        {
            tempText.text = "SUPER HOT!!!";
        }
        if (playerTemperature >= 100)
        {
            tempText.text = "SUPER FIRE!!!";
        }
    }

    #endregion

    #region Public Core Functions
    public void TempChange(bool decrease, float timer)
    {
        tempChangeMechanism = null;
        if (decrease)
        {
            tempChangeMechanism += TempDecrement;
        }
        else
        {
            tempChangeMechanism += TempIncrement;
        }

    }

    public void UpdatePlayerTemperature(float value)
    {
        playerTemperature += value;
        if (playerTemperature > maxTemp)
        {
            playerTemperature = maxTemp;
        }
        else if (playerTemperature < minTemp)
        {
            playerTemperature = minTemp;
        }

        if (playerTemperature > 0)
        {
            flamePS.SetActive(true);
        }
        else
        {
            flamePS.SetActive(false);
        }

        foreach(ParticleSystem ps in flames)
        {
            ps.startSize = playerTemperature / 100;
        }
        TempTxtUpdate();
    }
    #endregion

    #region Invoke Functions
    public void TempChangeStop()
    {
        tempChangeMechanism = null;
        ActiveStuckedObstacle = null;
        PlayerSingleton.Instance.GetPlayerMovementHandler.ForceStop = false;
        PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Run);

    }
    #endregion
}
