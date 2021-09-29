using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    #region Properties
    public static LevelManager Instance = null;

    [Header("Components Reference")]
    [SerializeField] private GameObject confettiObj = null;
    [SerializeField] private GameObject cm_1 = null;
    [SerializeField] private GameObject cm_2 = null;

    [Header("Attributes")]
    [SerializeField] private int maxLevels;

    private int currentLevel;
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
        PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Idle);
        currentLevel = PlayerPrefs.GetInt("level", 1);
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
            currentLevel++;
            PlayerPrefs.SetInt("level", currentLevel);
            Invoke("LevelEndMultiplierPanel", 1.4f);
        }
        else if (state == GameOverState.Defeat)
        {

            PlayerSingleton.Instance.GetPlayerMovementHandler.enabled = false;
            PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Victory);
            Invoke("LoseScreen", 1.4f);
        }
    }

    public void SwitchCamera()
    {
        cm_1.SetActive(false);
        cm_2.SetActive(true);

        cm_2.GetComponent<CinemachineVirtualCameraBase>().Follow = PlayerSingleton.Instance.beamStartPoint;
    }

    public void DisplayEndScreen()
    {
        confettiObj.transform.position = GameObject.FindGameObjectWithTag("ConfettiSpawnPoint").transform.position;
        confettiObj.SetActive(true);
        Invoke("VictoryScreen", 2f);
    }

    public void ChangeLevel()
    {
        if (currentLevel > maxLevels)
        {
            SceneManager.LoadScene("Level " + Random.Range(1,maxLevels));
        }
        else
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
    }
    #endregion

    #region Invoke functions
    private void LevelEndMultiplierPanel()
    {
        LevelUIManager.Instance.SwapGameplayPanel();
    }


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

