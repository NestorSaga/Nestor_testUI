using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class UIAudioManagerScript : MonoBehaviour
{
    public static UIAudioManagerScript instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }


    [SerializeField]
    private string menuMusic = null;

    [SerializeField]
    private string button1Event = null;

    [SerializeField]
    private string button2Event = null;

    [SerializeField]
    private string button3Event = null;

    [SerializeField]
    private string heartButton = null;

    [SerializeField]
    private string trophyButton = null;

    [SerializeField]
    private string crownTouch = null;

    [SerializeField]
    private string ghostSelect = null;

    [SerializeField]
    private string completed = null;

    [SerializeField]
    private string itemDrop = null;

    public void PlayMenuMusicEvent()
    {
        if (menuMusic != null)
        {
            RuntimeManager.PlayOneShot(menuMusic);
        }
    }

    public void PlayButton1Event()
    {
        if(button1Event != null)
        {
            RuntimeManager.PlayOneShot(button1Event);
        }
    }

    public void PlayButton2Event()
    {
        if (button2Event != null)
        {
            RuntimeManager.PlayOneShot(button2Event);
        }
    }

    public void PlayButton3Event()
    {
        if (button3Event != null)
        {
            RuntimeManager.PlayOneShot(button3Event);
        }
    }
    public void PlayHeartButtonEvent()
    {
        if (heartButton != null)
        {
            RuntimeManager.PlayOneShot(heartButton);
        }
    }
    public void PlayTrophyButtonEvent()
    {
        if (trophyButton != null)
        {
            RuntimeManager.PlayOneShot(trophyButton);
        }
    }

    public void PlayCrownTouchEvent()
    {
        if (crownTouch != null)
        {
            RuntimeManager.PlayOneShot(crownTouch);
        }
    }
    public void PlayGhostSelectEvent()
    {
        if (ghostSelect != null)
        {
            RuntimeManager.PlayOneShot(ghostSelect);
        }
    }

    public void PlayCompletedEvent()
    {
        if (completed != null)
        {
            RuntimeManager.PlayOneShot(completed);
        }
    }

    public void PlayItemDropEvent()
    {
        if (itemDrop != null)
        {
            RuntimeManager.PlayOneShot(itemDrop);
        }
    }

}
