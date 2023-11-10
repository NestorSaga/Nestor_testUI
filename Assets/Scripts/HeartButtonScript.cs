using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartButtonScript : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem heartParticles;

    public void onClickBurstHearts()
    {
        GameManager.instance.HidePopoupScore();
        UIAudioManagerScript.instance.PlayHeartButtonEvent();
        heartParticles.Play();
    }
}
