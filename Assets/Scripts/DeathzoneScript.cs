using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathzoneScript : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Crown")
        {
            GameManager.instance.EndCrownGame();
        }
    }
}
