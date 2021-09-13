using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    #region Properties
    public static LevelUIManager Instance = null;

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuUIPanel = null;
    [SerializeField] private GameObject gameplayUIPanel = null;
    [SerializeField] private GameObject gameOverWinUIPanel = null;
    [SerializeField] private GameObject gameOverLoseUIPanel = null;

    [Header("Gameplay UI Panel")]
    [SerializeField] private VariableJoystick movementJS = null;
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
    #endregion
}
