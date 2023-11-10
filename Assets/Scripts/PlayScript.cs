using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour
{

    [Header("References")]

    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject UIparent;
    [SerializeField]
    private GameObject playParent;
    private Vector3 buttonStartingPos;
    [SerializeField]
    private Transform leftAnchor, rightAnchor;

    [Header("Utils")]
    [SerializeField]
    private float lateralReboundForce;
    private GameObject instancedLeftGhost, instancedRightGhost;


    private void Start()
    {
        buttonStartingPos = playButton.transform.position;
    }

    void Update()
    {
        if(GameManager.instance.state == GameManager.State.PLAY)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            playButton.transform.position = mousePosition;
        }
    }

    public void OnClickStartPlay()
    {
        UIAudioManagerScript.instance.PlayButton1Event();
        transform.SetParent(playParent.transform);
        GameManager.instance.StartCrownGame();
        playButton.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Crown")
        {
            if(collision.transform.position.x > transform.position.x)
            {
                Debug.Log("right");
                collision.transform.GetComponent<Rigidbody2D>().AddForce(transform.right * lateralReboundForce, ForceMode2D.Impulse);
            }
            else if(collision.transform.position.x < transform.position.x)
            {
                collision.transform.GetComponent<Rigidbody2D>().AddForce(-transform.right * lateralReboundForce, ForceMode2D.Impulse);
            }
            UIAudioManagerScript.instance.PlayCrownTouchEvent();
            GameManager.instance.IncreaseCrownScore();
            GameManager.instance.UpdateCrownScore();
        }
    }

    public void resetPlayButton()
    {
        playButton.transform.position = new Vector3(buttonStartingPos.x, buttonStartingPos.y, buttonStartingPos.z);
        transform.SetParent(UIparent.transform);
        playButton.transform.GetChild(0).GetComponent<Button>().interactable = true;
    }

    public void InstantiateGhosts(GameObject ghost)
    {
        instancedLeftGhost = Instantiate(ghost, leftAnchor);
        instancedRightGhost = Instantiate(ghost, rightAnchor);
    }

    public void DestroyInstantiatedGhosts()
    {
        Destroy(instancedLeftGhost);
        Destroy(instancedRightGhost);
    }
}
