using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    #region Properties
    public static LevelUIManager Instance = null;

    [Header("Attributes")]
    [SerializeField] private float endPBDecrementSpeed = 0f;

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuUIPanel = null;
    [SerializeField] private GameObject gameplayUIPanel = null;
    [SerializeField] private GameObject gameOverWinUIPanel = null;
    [SerializeField] private GameObject gameOverLoseUIPanel = null;

    [Header("Gameplay UI Panel")]
    [SerializeField] private VariableJoystick movementJS = null;
    [SerializeField] private Image endPB = null;
    [SerializeField] private GameObject endLevelMechPanel = null;
    [SerializeField] private GameObject powerMultiplierBtn = null;
    #endregion

    #region Delegates
    public delegate void EndPBMechanism();

    public EndPBMechanism endPBMechanism = null;
    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        endPBMechanism = null;
    }

    private void Update()
    {
        if (endPBMechanism != null)
        {
            endPBMechanism();
        }
    }
    #endregion

    #region Getter And Setter
    public VariableJoystick GetMovementJS { get => movementJS; }
    #endregion

    #region Public Core Functions
    public void SwitchUIPanel(UIPanelState state)
    {
        switch (state)
        {
            case UIPanelState.MainMenu:
                mainMenuUIPanel.SetActive(true);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(false);
                break;
            case UIPanelState.Gameplay:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(true);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(false);
                break;
            case UIPanelState.Win:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(true);
                gameOverLoseUIPanel.SetActive(false);
                break;
            case UIPanelState.Lose:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(true);
                break;
        }
    }

    public void UpdateEndPB(float amount)
    {
        if (endPB.fillAmount < 1)
        {
            endPB.fillAmount += amount;
        }
    }

    public void EnablePBDecrementMech()
    {
        endPBMechanism += DecrementPB;
        Invoke("StopEndPBDecrement", 4);
    }

    public void SwapGameplayPanel()
    {
        movementJS.gameObject.SetActive(false);
        endLevelMechPanel.SetActive(true);
    }
    #endregion

    #region Private Core Functions
    private void DecrementPB()
    {
        if (endPB.fillAmount > 0)
        {
            endPB.fillAmount -= (endPBDecrementSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Invoke Functions
    private void StopEndPBDecrement()
    {
        endPBMechanism = null;
        powerMultiplierBtn.SetActive(false);
        PlayerSingleton.Instance.GetBeamObj.SetActive(true);
        LevelManager.Instance.EnablePP(true);

        //print();
        Invoke("StopBeam", (PlayerSingleton.Instance.GetPlayerTemperatureHandler.GetPlayerTemperature * endPB.fillAmount) / 100 * 10);
    }

    private void StopBeam()
    {
        PlayerSingleton.Instance.GetBeamObj.SetActive(false);
        LevelManager.Instance.DisplayEndScreen();
        LevelManager.Instance.EnablePP(false);
    }
    #endregion
}
