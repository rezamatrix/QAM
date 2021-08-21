using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                PlayerPrefs.SetFloat("musicTime", 0f);

            }
            GameObject.Find("Audio Source").GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("musicTime");
            GameObject.Find("Audio Source").GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("Audio Source").GetComponent<AudioSource>().Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playBtn()
    {
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            GameObject.Find("btnAudio").GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("musicTime", GameObject.Find("Audio Source").GetComponent<AudioSource>().time);
        }
    }
    public void playBtnGame()
    {
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            GameObject.Find("btnAudio").GetComponent<AudioSource>().Play();
        }
    }
}