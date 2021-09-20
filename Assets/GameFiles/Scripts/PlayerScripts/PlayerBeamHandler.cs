using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeamHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float beamSpeed = 0f;

    [Header("Components Reference")]
    [SerializeField] private Transform beam_1 = null;
    [SerializeField] private Transform beam_2 = null;
    #endregion

    #region MonoBehaviour Functions
    private void Update()
    {
        beam_1.localScale += Vector3.forward * Time.deltaTime * beamSpeed;
        beam_2.localScale += Vector3.forward * Time.deltaTime * beamSpeed;
    }
    #endregion
}
