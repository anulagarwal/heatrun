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

    [Header("Components Reference")]
    [SerializeField] private SkinnedMeshRenderer meshRenderer = null;
    [SerializeField] private TextMeshPro temperatureTxt = null;
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
    }

    private void Update()
    {
        if (tempChangeMechanism != null)
        {
            tempChangeMechanism();
        }
    }
    #endregion

    #region Getter And Setter
    public ObstaclesHandler ActiveStuckedObstacle { get; set; }
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
            ActiveStuckedObstacle.ChangeObstacleTemperature(tempChangeSpeed, true);
        }
    }

    private void TempIncrement()
    {
        playerTemperature += tempChangeSpeed * Time.deltaTime;
        TempTxtUpdate();
        if (ActiveStuckedObstacle)
        {
            ActiveStuckedObstacle.ChangeObstacleTemperature(tempChangeSpeed, false);
        }
    }

    private void TempTxtUpdate()
    {
        if (playerTemperature >= 0)
        {
            temperatureTxt.text = ((int)playerTemperature + " C");

        }
        else if (playerTemperature > 0)
        {
            temperatureTxt.text = ("-" + (int)playerTemperature + " C");
        }
      //  temperatureTxt.text = ((int)playerTemperature + " C");
        tempBar.fillAmount = playerTemperature / maxTemp;
        playerMat.SetFloat("_FillAmount", playerTemperature / maxTemp);
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

        Invoke("TempChangeStop", timer);
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

        TempTxtUpdate();
    }
    #endregion

    #region Invoke Functions
    private void TempChangeStop()
    {
        tempChangeMechanism = null;
        ActiveStuckedObstacle = null;
        PlayerSingleton.Instance.GetPlayerMovementHandler.enabled = true;
    }
    #endregion
}
