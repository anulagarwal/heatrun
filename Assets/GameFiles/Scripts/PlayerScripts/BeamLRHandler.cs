using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLRHandler : MonoBehaviour
{//
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private LineRenderer lineRenderer = null;
    [SerializeField] private Transform beamTip = null;
    #endregion

    #region MonoBehaviour Functions
    private void Update()
    {
        lineRenderer.SetPosition(1, beamTip.position - transform.position);
    }
    #endregion
}
