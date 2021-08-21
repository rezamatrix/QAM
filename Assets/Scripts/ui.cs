using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public GameObject objlogin;
    public GameObject objreg;

    // Use this for initialization
    // Use this for initialization
    private void Awake()
    {
        PlayerPrefs.SetFloat("musicTime", 0f);
        if (PlayerPrefs.GetInt("login") ==1)
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        }
    }
    public void hidepanls()
    {
        obj.gameObject.SetActive(false);

    }
    public void hidepanlslog()
    {
        objlogin.gameObject.SetActive(false);

    }
    public void hidepanlsreg()
    {
        objreg.gameObject.SetActive(false);

    }
    // Use this for initialization
    public void showpanss()
    {
        obj.gameObject.SetActive(true);

    }
}
