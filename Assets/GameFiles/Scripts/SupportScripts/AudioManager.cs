using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Properties
    public static AudioManager Instance = null;

    [Header("Components Reference")]
    [SerializeField] private AudioSource audioSource = null;
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Public Core Functions
    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    #endregion
}
