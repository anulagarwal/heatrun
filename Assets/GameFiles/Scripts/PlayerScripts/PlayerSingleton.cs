using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    #region Properties
    public static PlayerSingleton Instance = null;

    [Header("Components Reference")]
    [SerializeField] private PlayerAnimationsHandler playerAnimationsHandler = null;
    [SerializeField] private PlayerMovementHandler playerMovementHandler = null;
    [SerializeField] private GameObject beamObj = null;
    [SerializeField] internal Transform beamStartPoint = null;

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
    public PlayerAnimationsHandler GetPlayerAnimationsHandler { get => playerAnimationsHandler; }

    public PlayerMovementHandler GetPlayerMovementHandler { get => playerMovementHandler; }

    public GameObject GetBeamObj { get => beamObj; }
    #endregion
}
