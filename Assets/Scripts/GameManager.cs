using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{


    [Header("References")]

    [SerializeField]
    private GameObject crown;
    private Rigidbody2D rb;
    private Vector3 startingCrownPos;
    private ScoreBoardScript scoreBoardScript;
    [SerializeField]
    private PlayScript playScript;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI topCrownScoreText;
    [SerializeField]
    private GameObject[] ghostPrefabs;


    [Header("Game Stats")]

    private int totalTouches;
    private int topTouches;

    private int selectedGhostSkinId;



    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    

    public enum State
    {
        MENU,
        PLAY
    }

    public State state;

    public void setMenuState()
    {
        state = State.MENU;
    }
    public void setPlayState()
    {
        state = State.PLAY;
    }

    private void Start()
    {
        state = State.MENU;

        scoreBoardScript = GetComponent<ScoreBoardScript>();
        startingCrownPos = crown.transform.position;
        rb = crown.GetComponent<Rigidbody2D>();

    }


    public void StartCrownGame()
    {

        Cursor.visible = false;
        totalTouches = 0;
        scoreBoardScript.OnClickPopoutScoreBoard();


        crown.GetComponent<CircleCollider2D>().enabled = true;
        Rigidbody2D rb = crown.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.AddTorque((45 * Mathf.Deg2Rad) * rb.inertia, ForceMode2D.Impulse);

        SelectGhostSkin();

        setPlayState();
    }
    
    private void SelectGhostSkin()
    {
        switch (selectedGhostSkinId)
        {
            case 0:
                playScript.InstantiateGhosts(ghostPrefabs[selectedGhostSkinId]);
                break;
            default:
                playScript.InstantiateGhosts(ghostPrefabs[selectedGhostSkinId]);
                break;
        }
    }

    public void IncreaseCrownScore()
    {
        totalTouches++;
    }

    public void UpdateCrownScore()
    {
        title.text = "Score: " + totalTouches;
    }
    

    public void EndCrownGame()
    {

        setMenuState();
        playScript.resetPlayButton();
        Cursor.visible = true;
        UpdateTopCrownScore();
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        crown.transform.position = startingCrownPos;
        crown.transform.rotation = Quaternion.identity;
    }


    private void UpdateTopCrownScore()
    {
        if (totalTouches > topTouches)
        {
            topCrownScoreText.text = totalTouches.ToString();
        }
    }

    private void Update()
    {

    }




}
