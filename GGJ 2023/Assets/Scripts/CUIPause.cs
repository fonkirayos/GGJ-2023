using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIPause : MonoBehaviour
{

    public Button ResumeButton;
    public Button QuitButton;

    public bool isPause = false;

    Button selectedButton;
    bool selectCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        ResumeButton.onClick.AddListener(Resume);
        QuitButton.onClick.AddListener(quitGame);
        selectedButton = ResumeButton;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            if (!isPause)
            {
                isPause = true;
                Time.timeScale = 0f;
                ResumeButton.gameObject.SetActive(true);
                QuitButton.gameObject.SetActive(true);
                selectedButton.Select();
            }
            else
            {
                isPause = false;
                Time.timeScale = 1f;
                ResumeButton.gameObject.SetActive(false);
                QuitButton.gameObject.SetActive(false);
            }
            
        }
        if (isPause)
        {
            if (Input.GetAxisRaw("Horizontal") != 0f)
            {
                if (!selectCooldown)
                {
                    if (selectedButton == ResumeButton)
                        selectedButton = QuitButton;
                    else
                        selectedButton = ResumeButton;
                    selectCooldown = true;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") == 0f)
            {
                selectCooldown = false;
            }

            if (Input.GetButtonUp("Jump"))
            {
                
                selectedButton.onClick.Invoke();
            }
        }
        
    }

    void Resume()
    {
        isPause = false;
        Time.timeScale = 1f;
        ResumeButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
    }

    void quitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
