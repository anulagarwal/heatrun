using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstaclesHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float temperature = 0f;
    [SerializeField] private bool stickable = false;
    [SerializeField] private bool grounded = false;
    [SerializeField] private bool slowDownPlayer = false;
    [SerializeField] private float obstacleDestroyTime = 0f;

    [Header("Components Reference")]
    [SerializeField] private TextMeshPro obstacleTempTxt = null;
    [SerializeField] private GameObject steam = null;

    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        if (obstacleTempTxt)
        {
            obstacleTempTxt.SetText(((int)temperature).ToString());
        }
    }
    #endregion

    #region Getter And Setter
    public float GetTemperature { get => temperature; }

    public bool IsStickable { get => stickable; }

    public bool IsGrounded { get => grounded; }

    public float GetObstacleDestroyTime { get => obstacleDestroyTime; }

    public bool SlowDownPlayer { get => slowDownPlayer; }
    #endregion

    #region Public Core Functions
    public void DestroyObstacle()
    {
       // Destroy(this.gameObject, obstacleDestroyTime);
    }

    public void ChangeObstacleTemperature(float changeSpeed, bool decrease)
    {
        if (decrease)
        {
            temperature -= changeSpeed * Time.deltaTime;
        }
        else
        {
            temperature += changeSpeed * Time.deltaTime;
        }
        if (stickable)
        {
            steam.SetActive(true);
        }

        if(Mathf.RoundToInt(temperature) == 0)
        {
            PlayerSingleton.Instance.GetPlayerTemperatureHandler.TempChangeStop();
            Destroy(gameObject);
        }
        if (obstacleTempTxt)
        {
            obstacleTempTxt.SetText(((int)temperature).ToString());
        }
    }
    #endregion
}
