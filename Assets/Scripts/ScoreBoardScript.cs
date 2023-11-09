using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardScript : MonoBehaviour
{

    [SerializeReference]
    private Animator scoreAnimator;

    private bool isScoreBoardOut;

    public void OnClickPopupScoreBoard()
    {
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

    private void Update()
    {
        /*if(Input.GetMouseButtonDown(0) && isScoreBoardOut)
        {
            OnClickPopoutScoreBoard();
        }*/
    }
}
