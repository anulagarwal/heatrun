using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionAndTriggerEventsHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private PlayerMovementHandler playerMovementHandler = null;
    [SerializeField] private PlayerTemperatureHandler playerTemperatureHandler = null;
    [SerializeField] private ParticleSystem tempRiseVFX = null;
    [SerializeField] private ParticleSystem tempDropVFX = null;
    #endregion

    #region MonoBehaviour Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            playerTemperatureHandler.UpdatePlayerTemperature(other.gameObject.GetComponent<ObstaclesHandler>().GetTemperature);
            if (other.gameObject.GetComponent<ObstaclesHandler>().GetTemperature < 0)
            {
                tempRiseVFX.Stop();
                if (!tempDropVFX.isPlaying)
                {
                    tempDropVFX.Play();
                }
            }
            else if (other.gameObject.GetComponent<ObstaclesHandler>().GetTemperature > 0)
            {
                tempDropVFX.Stop();
                if (!tempRiseVFX.isPlaying)
                {
                    tempRiseVFX.Play();
                }
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Finish")
        {
            playerMovementHandler.enabled = false;
            LevelManager.Instance.GameOver(GameOverState.Victory);
        }
    }
    #endregion
}
