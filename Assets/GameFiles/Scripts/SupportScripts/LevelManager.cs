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
    #endregion

    #region Public Core Functions
    public void GameOver(GameOverState state)
    {
        if (state == GameOverState.Victory)
        {
            PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Victory);
            confettiObj.SetActive(true);
        }
        else if (state == GameOverState.Defeat)
        {
            print("GameOver");
        }
    }
    #endregion
}
