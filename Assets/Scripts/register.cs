using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class register : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public Text notifction;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void regwithusername()
    {
        StartCoroutine(regusername());
    }
    public void geswithusername()
    {
        StartCoroutine(gesusername());
    }
    IEnumerator regusername()
    {
        string usernametext = username.text.ToString().ToLower();
        string passwordtext = password.text.ToString();
        WWWForm form = new WWWForm();
        form.AddField("username", usernametext);
        form.AddField("password", passwordtext);

        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/reg.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            //  string json = "[{Name:'John Simith',Age:35},{Name:'Pablo Perez',Age:34}]";

            Users m = JsonUtility.FromJson<Users>(jsonString);
            Debug.Log(m.succse.ToString());
            Debug.Log(m.status.ToString());
            notifction.text = m.status.ToString();
            Debug.Log(www.downloadHandler.text);
            Debug.Log("Form upload complete!");
            if (m.succse == 1)
            {
                PlayerPrefs.SetString("username", usernametext.ToLower());
                PlayerPrefs.SetString("password", passwordtext);
                PlayerPrefs.SetString("mode", "reg");
                PlayerPrefs.SetInt("login", 1);
                PlayerPrefs.SetFloat("music", 1f);
                PlayerPrefs.SetFloat("sound", 1f);
                PlayerPrefs.Save();
                SceneManager.LoadScene("main", LoadSceneMode.Single);
            }
        }
    }
    IEnumerator gesusername()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://bitcorp.ir/qaa/ges.php");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;

            ges am = JsonUtility.FromJson<ges>(jsonString);
            Debug.Log(am.succse.ToString());
            Debug.Log(am.status.ToString());
            Debug.Log(am.username.ToString());
            if (am.succse == 1)
            {
                PlayerPrefs.SetString("username", am.username.ToString());
                PlayerPrefs.SetString("password", "kk");
                PlayerPrefs.SetString("mode", "ges");
                PlayerPrefs.SetInt("login", 1);
                PlayerPrefs.SetFloat("music", 1f);
                PlayerPrefs.SetFloat("sound", 1f);
                PlayerPrefs.Save();
                SceneManager.LoadScene("main", LoadSceneMode.Single);
            }
            Debug.Log(www.downloadHandler.text);
            Debug.Log("Form upload complete!");

        }
    }
    [System.Serializable]
    public class Users
    {
        public int succse;
        public string status;

    }
    [System.Serializable]
    public class ges
    {
        public int succse;
        public string status;
        public string username;

    }
}
