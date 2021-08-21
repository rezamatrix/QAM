using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login : MonoBehaviour
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
    public void loginwithusername()
    {
        StartCoroutine(loginusername());
    }
    IEnumerator loginusername()
    {
        string usernametext = username.text.ToString();
        string passwordtext = password.text.ToString();
        WWWForm form = new WWWForm();
        form.AddField("username", usernametext.ToLower());
        form.AddField("password", passwordtext);

        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/login.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;

            Users m = JsonUtility.FromJson<Users>(jsonString);
            Debug.Log(m.succse.ToString());
            Debug.Log(m.status);
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
    [System.Serializable]
    public class Users
    {
        public int succse;
        public string status;

    }
}
