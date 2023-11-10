using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyButtonScript : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    private AnimationClip animationClipRotateIn, animationClipRotateBack;
    [SerializeField]
    private new Animation animation;

    [Header("Utils")]
    [SerializeField]
    private float buttonRotationResetTime;
    private bool isRotated;
    

    public void OnClickRotate()
    {
        GameManager.instance.HidePopoupScore();
        if (!isRotated)
        {
            StartCoroutine(RotateAnimation());
        }
          
    }

    IEnumerator RotateAnimation()
    {
        animation.clip = animationClipRotateIn;
        animation.Play();
        isRotated = true;
        UIAudioManagerScript.instance.PlayTrophyButtonEvent();
        yield return new WaitForSecondsRealtime(2f);

        //Instantiate random item
        GameManager.instance.InstantiateTrophyRandomItem();

        UIAudioManagerScript.instance.PlayItemDropEvent();

        animation.clip = animationClipRotateBack;
        animation.Play();

        yield return new WaitForSecondsRealtime(buttonRotationResetTime);

        isRotated = false;
    }
}
