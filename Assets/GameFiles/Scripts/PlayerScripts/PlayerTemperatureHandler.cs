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

    #region Private Core Functions
    private void InitialSettings()
    {
        playerTemperature = 0;
        playerMat.SetFloat("_FillAmount", playerTemperature);
    }

    private void TempDecrement()
    {
        playerTemperature -= 1 * Time.deltaTime;
        TempTxtUpdate();
    }

    private void TempIncrement()
    {
        playerTemperature += 1 * Time.deltaTime;
        TempTxtUpdate();
    }

    private void TempTxtUpdate()
    {
        playerTemperature = (int)playerTemperature;
        if (playerTemperature >= 0)
        {
            temperatureTxt.text = (playerTemperature + " C");

        }
        else if (playerTemperature > 0)
        {
            temperatureTxt.text = ("-" + playerTemperature + " C");
        }
        temperatureTxt.text = (playerTemperature + " C");
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
    }
    #endregion
}
