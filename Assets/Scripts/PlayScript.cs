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
    private GameObject parent;
    private Vector3 buttonStartingPos;
    [SerializeField]
    private Transform leftAnchor, rightAnchor;

    private void Start()
    {

        buttonStartingPos = playButton.transform.position;

        Debug.Log("anchored pos is "+ buttonStartingPos);
    }

    void Update()
    {
        if(GameManager.instance.state == GameManager.State.PLAY)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;

            playButton.transform.position = mousePosition;
            
            /*Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.MoveTowards(transform.position, mousePosition, 10 * Time.deltaTime);
            */
        }
    }

    public void OnClickStartPlay()
    {
        GameManager.instance.StartCrownGame();
        playButton.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Crown")
        {
            GameManager.instance.IncreaseCrownScore();
            GameManager.instance.UpdateCrownScore();
        }
    }

    public void resetPlayButton()
    {
        playButton.transform.position = new Vector3(buttonStartingPos.x, buttonStartingPos.y, buttonStartingPos.z);

        Debug.Log("resetted to " + new Vector3(buttonStartingPos.x, buttonStartingPos.y, buttonStartingPos.z));
        playButton.transform.GetChild(0).GetComponent<Button>().interactable = true;
    }

    public void InstantiateGhosts(GameObject ghost)
    {
        Instantiate(ghost, leftAnchor);
        Instantiate(ghost, rightAnchor);

        Debug.Log("instanciaos");
    }
}
