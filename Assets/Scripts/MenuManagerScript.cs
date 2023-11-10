using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerScript : MonoBehaviour
{
    [Header("References")]

    [SerializeField]
    private GameObject mainButtonsBackground, extraButtonsBackground, title, notification;

    [SerializeField]
    private Animator animator;

    [Header("Utils")]
    [SerializeField]
    private bool isNotificationOn;


    public void OnClickMenuSwap()
    {
        disableNotification();
        UIAudioManagerScript.instance.PlayButton3Event();
        animator.SetBool("SwapMenu", true);
    }

    public void OnClickMenuSwapBack()
    {
        UIAudioManagerScript.instance.PlayButton3Event();
        animator.SetBool("SwapMenu", false);
    }

    public void enableNotification()
    {
        isNotificationOn = true;
        notification.SetActive(true);
    }

    public void disableNotification()
    {
        isNotificationOn = false;
        notification.SetActive(false);
    }
}
