using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Properties
    public static LevelManager Instance = null;

    [Header("Components Reference")]
    [SerializeField] private GameObject confettiObj = null;
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
        PlayerSingleton.Instance.GetPlayerMovementHandler.enabled = false;
        PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Run);

    }
    #endregion

    #region Public Core Functions

    public void StartLevel()
    {
        PlayerSingleton.Instance.GetPlayerMovementHandler.enabled = true;
        PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Run);
        LevelUIManager.Instance.SwitchUIPanel(UIPanelState.Gameplay);
    }
    public void GameOver(GameOverState state)
    {
        if (state == GameOverState.Victory)
        {
            PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Victory);
            PlayerSingleton.Instance.GetPlayerMovementHandler.enabled = false;
            confettiObj.SetActive(true);
            Invoke("VictoryScreen", 1.4f);

        }
        else if (state == GameOverState.Defeat)
        {
        
            PlayerSingleton.Instance.GetPlayerMovementHandler.enabled = false;
            PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Victory);
            Invoke("LoseScreen", 1.4f);
        }
    }


    #endregion

    #region Invoke functions

    void VictoryScreen()
    {
        LevelUIManager.Instance.SwitchUIPanel(UIPanelState.Win);
    }

    void LoseScreen()
    {
        LevelUIManager.Instance.SwitchUIPanel(UIPanelState.Lose);
    }
    #endregion
}

