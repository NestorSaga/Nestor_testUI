using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardScript : MonoBehaviour
{

    [Header("References")]
    [SerializeReference]
    private Animator scoreAnimator;
    private bool isScoreBoardOut;

    public void OnClickPopupScoreBoard()
    {
        UIAudioManagerScript.instance.PlayButton2Event();

        if (!isScoreBoardOut)
        {
            scoreAnimator.SetBool("ScorePopup", true);
            isScoreBoardOut = true;
        }
        else if (isScoreBoardOut)
        {
            OnClickPopoutScoreBoard();
        }

    }

    public void OnClickPopoutScoreBoard()
    {
        scoreAnimator.SetBool("ScorePopup", false);
        isScoreBoardOut = false;
    }
}
