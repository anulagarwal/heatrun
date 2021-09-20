using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerMultiplierTouchEventsHandler : MonoBehaviour, IPointerClickHandler
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float powerUpdate = 0f;
    #endregion

    #region MonoBehaviour Functions
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelUIManager.Instance.UpdateEndPB(powerUpdate);
    }
    #endregion
}
