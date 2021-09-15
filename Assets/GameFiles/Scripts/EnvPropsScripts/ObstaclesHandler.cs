using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesHandler : MonoBehaviour
{
    #region Properties
    [Header("Atributes")]
    [SerializeField] private float temperature = 0f;
    [SerializeField] private bool stickable = false;
    [SerializeField] private float obstacleDestroyTime = 0f;
    #endregion

    #region Getter And Setter
    public float GetTemperature { get => temperature; }

    public bool IsStickable { get => stickable; }

    public float GetObstacleDestroyTime { get => obstacleDestroyTime; }
    #endregion

    #region Public Core Functions
    public void DestroyObstacle()
    {
        Destroy(this.gameObject, obstacleDestroyTime);
    }
    #endregion
}
