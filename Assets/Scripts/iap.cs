using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class iap : MonoBehaviour
{
    public Text dn;
    public Text un;
    public Text coin;
    public Image av;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(mainuserinfo());
    }
    public void back()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
        Debug.Log("hello");
    }
    public void Packone()
    {
        StartCoroutine(addcoinreq(250));
    }
    public void Packtow()
    {
        StartCoroutine(addcoinreq(750));
    }
    public void Packthree()
    {
        StartCoroutine(addcoinreq(1750));
    }
    public void Packfour()
    {
        StartCoroutine(addcoinreq(4000));
    }
    public void Packfive()
    {
        StartCoroutine(addcoinreq(12000));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator addcoinreq(int avt)
    {
        

        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("av", avt);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/addcoin.php", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Debug.Log(jsonString);
            Addf ms = JsonUtility.FromJson<Addf>(jsonString);
            int hello = ms.succse;
            if (hello == 1)
            {

                StartCoroutine(mainuserinfo());
           
            }

            Debug.Log(avt);
        }

    }
    IEnumerator mainuserinfo()
    {
        string usernametext = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        Debug.Log(usernametext);
        Debug.Log(passwordtext);
        Debug.Log(mode);

        WWWForm form = new WWWForm();
        form.AddField("username", usernametext);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);

        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/userinfo.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Users m = JsonUtility.FromJson<Users>(jsonString);
            coin.text = m.balance;                  
            un.text = usernametext;
            dn.text = m.displayname;

            Sprite[] avatars = Resources.LoadAll<Sprite>("Avatars");

            int avnum = m.avatar;
            Debug.Log("this is avatar num: " + avnum);
            if (avnum == 10)
            {
                avnum = 0;
            }
            av.sprite = avatars[avnum];
          

            Debug.Log(jsonString);
        }
    }
    [System.Serializable]
    public class Users
    {
        public string level;
        public string xp;
        public string balance;
        public string date_singup;
        public string lastseen;
        public int avatar;
        public string displayname;
        public string uniqiddecode;
        public string chart;
    }
    [System.Serializable]
    public class Addf
    {
        public int succse;
        public string status;

    }
}
