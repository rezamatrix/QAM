using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class perfabs : MonoBehaviour
{
    public Text coin;
    public Text chart;
    //public Text xp;
    public Text level;
    public Image avatar;
    public Text dp;
    public Text game;
    public Text addreqs;
    public Image avtr;
    public GameObject stting;
    public GameObject setavt;
    public GameObject setdis;
    public GameObject setpass;
    public InputField displayname;
    public InputField p1;
    public InputField p2;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(mainuserinfo());
        setb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startstteing() { stting.gameObject.SetActive(true); }
    public void closestteing() { stting.gameObject.SetActive(false); }
    public void startsetav() { setavt.gameObject.SetActive(true); }
    public void closesetav() { setavt.gameObject.SetActive(false); }
    public void startsetdis() { setdis.gameObject.SetActive(true); }
    public void closesetdis() { setdis.gameObject.SetActive(false); }
    public void startsetpass() { setpass.gameObject.SetActive(true); }
    public void closesetpass() { setpass.gameObject.SetActive(false); }
    public void friendlist ()
    {
        SceneManager.LoadScene("freindsList", LoadSceneMode.Single);
        Debug.Log("hello");
    }
    public void Logout()
    {
        PlayerPrefs.SetString("username", "");
        PlayerPrefs.SetString("password", "");
        PlayerPrefs.SetInt("login", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("login", LoadSceneMode.Single);
    }
    public void contniue()
    {
        SceneManager.LoadScene("continue", LoadSceneMode.Single);
    }
    public void Chart()
    {
        SceneManager.LoadScene("chart", LoadSceneMode.Single);
        Debug.Log("hello");
    }
    public void coinshop()
    {
        SceneManager.LoadScene("inAppPurchase", LoadSceneMode.Single);
        Debug.Log("hello");
    }

    public void chdn()
    {
        StartCoroutine(chdnreq());
        Debug.Log("hello");
    }
    public void chp()
    {
        StartCoroutine(chpreq());
        Debug.Log("hello");
    }
    public void chav(int a)
    {
        StartCoroutine(chavreq(a));
        
    }
    public void setb()
    {
        //for (int i=1;i<11;i++)
       // {
            setavt.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { chav(1); });
            setavt.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { chav(2); });
            setavt.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { chav(3); });
            setavt.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { chav(4); });
            setavt.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate { chav(5); });
            setavt.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(delegate { chav(6); });
            setavt.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(delegate { chav(7); });
            setavt.transform.GetChild(8).GetComponent<Button>().onClick.AddListener(delegate { chav(8); });
            setavt.transform.GetChild(9).GetComponent<Button>().onClick.AddListener(delegate { chav(9); });
            setavt.transform.GetChild(10).GetComponent<Button>().onClick.AddListener(delegate { chav(10); });

           // Debug.Log(i);
      //  }
       
    }
    IEnumerator chavreq(int avt)
    {
        string usernametext1 = displayname.text.ToString();
     
            string usernametext2 = PlayerPrefs.GetString("username");
            string passwordtext = PlayerPrefs.GetString("password");
            string mode = PlayerPrefs.GetString("mode");
            WWWForm form = new WWWForm();
            form.AddField("av", avt);
            form.AddField("username2", usernametext2);
            form.AddField("password", passwordtext);
            form.AddField("mode", mode);
            UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/changeav.php", form);
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
                    closesetav();
                }

                Debug.Log(avt);
            }
        
    }
    IEnumerator chpreq()
    {
        
        string usernametext1 = p1.text.ToString();
        string usernametext3 = p2.text.ToString();
        if (usernametext1.Length >= 1) { 
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("p1", usernametext1);
        form.AddField("p2", usernametext3);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/changepass.php", form);
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
                closesetpass();
               // PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("Login", LoadSceneMode.Single);
            }

            Debug.Log("hello");
        }
        }
    }
    IEnumerator chdnreq()
    {
        string usernametext1 = displayname.text.ToString();
        if (usernametext1.Length>=1) {
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("dis", usernametext1);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/changedis.php", form);
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
                closesetdis();
            }

            Debug.Log("hello");
            }
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
            chart.text =m.chart;
            //xp.text = m.xp;
            game.text = m.game;
            addreqs.text = m.addf;
            level.text = m.level;
            dp.text = m.displayname;
            
            Sprite[] avatars = Resources.LoadAll<Sprite>("Avatars");
            
            int avnum = m.avatar;
            Debug.Log("this is avatar num: " + avnum);
            if (avnum==10)
            {
                avnum = 0;
            }
            avatar.sprite = avatars[avnum];
            avtr.sprite = avatars[avnum];

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
        public string addf;
        public string game;
    }
    [System.Serializable]
    public class Addf
    {
        public int succse;
        public string status;

    }
}
