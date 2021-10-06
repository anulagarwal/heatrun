using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollisionAndTriggerEventsHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private PlayerMovementHandler playerMovementHandler = null;
    [SerializeField] private PlayerAnimationsHandler playerAnimationsHandler = null; 
    [SerializeField] private PlayerTemperatureHandler playerTemperatureHandler = null;
    [SerializeField] private ParticleSystem tempRiseVFX = null;
    [SerializeField] private ParticleSystem tempDropVFX = null;
    [SerializeField] private Transform obstacleHolder = null;
    #endregion

    #region MonoBehaviour Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" )
        {
            if (other.gameObject.TryGetComponent<ObstaclesHandler>(out ObstaclesHandler obstaclesHandler))
            {
                if (!obstaclesHandler.IsGrounded)
                {
                    if (!obstaclesHandler.IsStickable)
                    {
                        playerTemperatureHandler.UpdatePlayerTemperature(other.gameObject.GetComponent<ObstaclesHandler>().GetTemperature);

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
                        PlayerSingleton.Instance.GetPlayerMovementHandler.ForceStop = true;
                        playerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Push);                                             
                        playerTemperatureHandler.ActiveStuckedObstacle = obstaclesHandler;
                        if (obstaclesHandler.GetTemperature < 0)
                        {
                            playerTemperatureHandler.TempChange(true, obstaclesHandler.GetObstacleDestroyTime);

                        }
                        else
                        {
                            playerTemperatureHandler.TempChange(false, obstaclesHandler.GetObstacleDestroyTime);
                        }
                    }
                }

                if (obstaclesHandler.SlowDownPlayer)
                {
                    PlayerSingleton.Instance.GetPlayerMovementHandler.EnableDefaultSpeed(false);
                    PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.SlowWalk);
                }
            }
        }
        else if (other.gameObject.tag == "Finish")
        {
            playerMovementHandler.enabled = false;
            LevelManager.Instance.GameOver(GameOverState.Victory);

            LevelUIManager.Instance.EnablePBDecrementMech();
            LevelManager.Instance.SwitchCamera();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<ObstaclesHandler>(out ObstaclesHandler obstaclesHandler))
        {
            if (obstaclesHandler.IsStickable)
            {
                playerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Run);
                playerTemperatureHandler.TempChangeStop();
            }

            if (obstaclesHandler.SlowDownPlayer)
            {
                PlayerSingleton.Instance.GetPlayerMovementHandler.EnableDefaultSpeed(true);
                PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Run);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (other.gameObject.TryGetComponent<ObstaclesHandler>(out ObstaclesHandler obstaclesHandler))
            {
                if (obstaclesHandler.IsGrounded)
                {

                    if (playerTemperatureHandler.GetPlayerTemperature > 0)
                    {
                        print(playerTemperatureHandler.GetPlayerTemperature);
                        playerTemperatureHandler.UpdatePlayerTemperature(other.gameObject.GetComponent<ObstaclesHandler>().GetTemperature / 10);

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
                    }
                }
            }
        }
    }

    #endregion
}