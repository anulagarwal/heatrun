using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesHandler : MonoBehaviour
{
    #region Properties
    [Header("Atributes")]
    [SerializeField] private float temperature = 0f;
    #endregion

    #region Getter And Setter
    public float GetTemperature { get => temperature; }
    #endregion
}
