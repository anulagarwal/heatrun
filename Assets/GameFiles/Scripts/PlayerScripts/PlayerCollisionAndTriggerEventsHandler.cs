using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionAndTriggerEventsHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private PlayerTemperatureHandler playerTemperatureHandler = null;
    #endregion

    #region MonoBehaviour Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            playerTemperatureHandler.UpdatePlayerTemperature(other.gameObject.GetComponent<ObstaclesHandler>().GetTemperature);
            Destroy(other.gameObject);
        }
    }
    #endregion
}
