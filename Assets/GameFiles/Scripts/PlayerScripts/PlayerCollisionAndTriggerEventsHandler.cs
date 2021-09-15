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
    [SerializeField] private Transform obstacleHolder = null;
    #endregion

    #region MonoBehaviour Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            playerTemperatureHandler.UpdatePlayerTemperature(other.gameObject.GetComponent<ObstaclesHandler>().GetTemperature);

            if (other.gameObject.TryGetComponent<ObstaclesHandler>(out ObstaclesHandler obstaclesHandler))
            {
                if (!obstaclesHandler.IsStickable)
                {
                    if (obstaclesHandler.GetTemperature < 0)
                    {
                        tempRiseVFX.Stop();
                        if (!tempDropVFX.isPlaying)
                        {
                            tempDropVFX.Play();
                        }
                    }
                    else if (obstaclesHandler.GetTemperature > 0)
                    {
                        tempDropVFX.Stop();
                        if (!tempRiseVFX.isPlaying)
                        {
                            tempRiseVFX.Play();
                        }
                    }
                    Destroy(other.gameObject);
                }
                else
                {
                    other.gameObject.transform.position = obstacleHolder.position;
                    other.gameObject.transform.parent = obstacleHolder;
                    obstaclesHandler.DestroyObstacle();
                    if (obstaclesHandler.GetTemperature < 0)
                    {
                        playerTemperatureHandler.TempChange(true, obstaclesHandler.GetObstacleDestroyTime);
                    }
                    else
                    {
                        playerTemperatureHandler.TempChange(true, obstaclesHandler.GetObstacleDestroyTime);
                    }
                }
            }
        }
        else if (other.gameObject.tag == "Finish")
        {
            playerMovementHandler.enabled = false;
            LevelManager.Instance.GameOver(GameOverState.Victory);
        }
    }
    #endregion
}
