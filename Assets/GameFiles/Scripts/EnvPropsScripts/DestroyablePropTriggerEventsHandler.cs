using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePropTriggerEventsHandler : MonoBehaviour
{//
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private GameObject burstVFXObj = null;
    #endregion

    #region MonoBehaviour Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Beam")
        {
            burstVFXObj.SetActive(true);
            burstVFXObj.transform.parent = null;
            Destroy(gameObject);
        }
    }
    #endregion
}
