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
    private MenuManagerScript menuManagerScript;
    [SerializeField]
    private Transform trophyItemSpawnPos;
    [SerializeField]
    private GameObject trophyItemPrefab;
    [SerializeField]
    private PlayScript playScript;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI topCrownScoreText;
    [SerializeField]
    private TextMeshProUGUI skinDescriptionText;
    [SerializeField]
    private GameObject[] ghostPrefabs;
    [SerializeField]
    private Image[] skinBackgrounds;
    [SerializeField]
    private Sprite[] randomDropItems;
    [SerializeField]
    private Animator ghostsAnimator;


    [Header("Game Stats")]
    private int totalTouches;
    private int topTouches;
    private int selectedGhostSkinId = 0;
    private bool isSkin1Unlocked, isSkin2Unlocked, isSkin3Unlocked;


    [Header("Utils")]
    [SerializeField]
    private Color selectedColor;
    [SerializeField]
    private Color selectableColor;
    [SerializeField]
    private int touchesForSkin1Unlock;
    [SerializeField]
    private int touchesForSkin2Unlock;
    [SerializeField]
    private int touchesForSkin3Unlock;
    [SerializeField]
    private float itemSpawnForce;







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
        menuManagerScript = GetComponent<MenuManagerScript>();
        startingCrownPos = crown.transform.position;
        rb = crown.GetComponent<Rigidbody2D>();

        UIAudioManagerScript.instance.PlayMenuMusicEvent();

    }


    public void StartCrownGame()
    {

        Cursor.visible = false;
        totalTouches = 0;
        scoreBoardScript.OnClickPopoutScoreBoard();

        StartCoroutine(PlayGhostsAnimation());
        setPlayState();
        
    }

    private IEnumerator PlayGhostsAnimation()
    {
        
        ghostsAnimator.SetBool("startedPlaying", true);
        playScript.InstantiateGhosts(ghostPrefabs[selectedGhostSkinId]);
        yield return new WaitForSecondsRealtime(1.5f);

        crown.GetComponent<CircleCollider2D>().enabled = true;
        Rigidbody2D rb = crown.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.AddTorque((45 * Mathf.Deg2Rad) * rb.inertia, ForceMode2D.Impulse);

    }

    
    public void SelectGhostSkin(int id)
    {

        switch (id)
        {
            case 0:
                skinDescriptionText.text = "This is the default skin!";
                skinBackgrounds[selectedGhostSkinId].color = selectableColor;
                skinBackgrounds[id].color = selectedColor;            
                selectedGhostSkinId = id;
                break;
            case 1:
                skinDescriptionText.text = "Need a score of at least " + touchesForSkin1Unlock + "!";
                if (isSkin1Unlocked)
                {
                    skinDescriptionText.text = "Touching grass is important!";
                    skinBackgrounds[id].color = selectedColor;
                    skinBackgrounds[selectedGhostSkinId].color = selectableColor;
                    selectedGhostSkinId = id;
                }
                break;
            case 2:
                skinDescriptionText.text = "Need a score of at least " + touchesForSkin2Unlock + "!";
                if (isSkin2Unlocked)
                {
                    skinDescriptionText.text = "Nothing beats chicken";
                    skinBackgrounds[id].color = selectedColor;
                    skinBackgrounds[selectedGhostSkinId].color = selectableColor;
                    selectedGhostSkinId = id;
                }
                break;
            case 3:
                skinDescriptionText.text = "Need a score of at least " + touchesForSkin3Unlock + "!";
                if (isSkin3Unlocked)
                {
                    skinDescriptionText.text = "A true proof of skill";
                    skinBackgrounds[id].color = selectedColor;
                    skinBackgrounds[selectedGhostSkinId].color = selectableColor;
                    selectedGhostSkinId = id;
                }
                break;

        }

    }

    public void IncreaseCrownScore()
    {
        totalTouches++;
        CheckForSkinUnlocks();
    }

    public void CheckForSkinUnlocks()
    {
        if(totalTouches >= touchesForSkin1Unlock && totalTouches < touchesForSkin2Unlock && !isSkin1Unlocked)
        {
            isSkin1Unlocked = true;
            UpdateSkinUnlocks(1);
            UIAudioManagerScript.instance.PlayCompletedEvent();
            menuManagerScript.enableNotification();
            
        }
        else if(totalTouches >= touchesForSkin2Unlock && totalTouches < touchesForSkin3Unlock && !isSkin2Unlocked)
        {
            isSkin2Unlocked = true;
            UpdateSkinUnlocks(2);
            UIAudioManagerScript.instance.PlayCompletedEvent();
            menuManagerScript.enableNotification();
        }
        else if(totalTouches >= touchesForSkin3Unlock && !isSkin3Unlocked)
        {
            isSkin3Unlocked = true;
            UpdateSkinUnlocks(3);
            UIAudioManagerScript.instance.PlayCompletedEvent();
            menuManagerScript.enableNotification();
        }
    }

    public void UpdateSkinUnlocks(int id)
    {
        ghostPrefabs[id].GetComponent<GhostScript>().ghostBase.color = new Color(1, 1, 1, 1);
        ghostPrefabs[id].GetComponent<GhostScript>().cosmetic.color = new Color(1, 1, 1, 1);
      
        skinBackgrounds[id].color = selectableColor;
    }

    public void UpdateCrownScore()
    {
        title.text = "Score: " + totalTouches;
    }
    

    public void EndCrownGame()
    {

        ghostsAnimator.SetBool("startedPlaying", false);
        playScript.DestroyInstantiatedGhosts();
        title.text = "CROWN GAME";

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

    public void InstantiateTrophyRandomItem()
    {
        GameObject randomItem =  Instantiate(trophyItemPrefab, trophyItemSpawnPos);
        randomItem.GetComponent<Image>().sprite = randomDropItems[Random.Range(0, randomDropItems.Length)];
        randomItem.GetComponent<Rigidbody2D>().AddForce(trophyItemSpawnPos.up * itemSpawnForce, ForceMode2D.Impulse);
        StartCoroutine(DestroyItem(randomItem));
        
    }

    IEnumerator DestroyItem(GameObject item)
    {
        yield return new WaitForSecondsRealtime(10);
        Destroy(item);
    }

    private void Update()
    {

    }




}
