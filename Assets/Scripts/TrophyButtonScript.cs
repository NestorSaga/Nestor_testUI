using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyButtonScript : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    private AnimationClip animationClipRotateIn, animationClipRotateBack;
    [SerializeField]
    private Animation animation;
    private Animator animator;

    [Header("Utils")]
    private bool isRotated;

    private void Start()
    {
        //animationClip = GetComponent<AnimationClip>();
    }

    public void OnClickRotate()
    {
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

        animation.clip = animationClipRotateBack;
        animation.Play();

        yield return new WaitForSecondsRealtime(1.5f);

        isRotated = false;
    }
}
