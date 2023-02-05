using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckpoint : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (transform.position != collision.gameObject.GetComponent<CPlayerController>().respawnPos)
            {
                collision.gameObject.GetComponent<CPlayerController>().playCheckpointSound();
                collision.gameObject.GetComponent<CPlayerController>().respawnPos = transform.position;
            }
           

        }
    }
}
