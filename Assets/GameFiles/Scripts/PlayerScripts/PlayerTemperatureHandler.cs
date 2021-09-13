using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTemperatureHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float minTemp = -1f;
    [SerializeField] private float maxTemp = 1f;

    [Header("Components Reference")]
    [SerializeField] private SkinnedMeshRenderer meshRenderer = null;
    [SerializeField] private TextMeshPro temperatureTxt = null;

    private Material playerMat = null;
    [SerializeField] private float playerTemperature = 0f;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        playerMat = meshRenderer.material;
        InitialSettings();
    }
    #endregion

    #region Private Core Functions
    private void InitialSettings()
    {
        playerTemperature = 0;
        playerMat.SetFloat("_FillAmount", playerTemperature);
    }
    #endregion

    #region Public Core Functions
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
        if(playerTemperature >= 0)
        {
            temperatureTxt.SetText(playerTemperature + " C");

        }
        else if(playerTemperature > 0)
        {
            temperatureTxt.SetText("-"+playerTemperature*100 + " C");
        }
        temperatureTxt.SetText(playerTemperature + " C");
        playerMat.SetFloat("_FillAmount", playerTemperature);
    }
    #endregion
}