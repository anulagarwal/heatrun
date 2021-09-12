using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemperatureHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float minTemp = -1f;
    [SerializeField] private float maxTemp = 1f;

    [Header("Components Reference")]
    [SerializeField] private MeshRenderer meshRenderer = null;

    private Material playerMat = null;
    private float playerTemperature = 0f;
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
        playerTemperature = minTemp;
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
        playerMat.SetFloat("_FillAmount", playerTemperature);
    }
    #endregion
}
