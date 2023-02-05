using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CDaisy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject superDaisy;
    public bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
            Time.timeScale = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<CPlayerController>().bHasWatercan)
            {
                spriteRenderer.enabled = false;
                superDaisy.SetActive(true);
                win = true;
            }
        }
    }
}
