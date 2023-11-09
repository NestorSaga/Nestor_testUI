using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartButtonScript : MonoBehaviour
{
    public ParticleSystem heartParticles;

    public void onClickBurstHearts()
    {
        Debug.Log("cue");
        heartParticles.Play();
    }
}
