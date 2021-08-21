using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class addItemFreinds : MonoBehaviour
{
    int j = 0;
    public Text uniqcode;
    public InputField schuq;
    public Text level;
    public Text xp;  
    public Text displayname;
    public Image av;
    public Text xpp;
    public Text dn;
    public Text lvl;
    public Image avv;
    public GameObject freindItem;
    public GameObject content;
    public Transform contentobj;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
   
    // Start is called before the first frame update
    void Start()
    {
       
        StartCoroutine(regusername());
        StartCoroutine(Userinfo());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void closef()
    {
        obj1.gameObject.SetActive(false);
    }
    public void Oncsfs(string usernames,int mode)
    {
        StartCoroutine(Showfriend(usernames,mode));
        Debug.Log(usernames);
    }
    public void Add(string usernames)
    {
        StartCoroutine(Addreq(usernames));
        Debug.Log(usernames);
    }
    public void Remover(string usernames)
    {
        StartCoroutine(Removerreq(usernames));
        Debug.Log(usernames);

    }
    public void Removerrreq(string usernames)
    {
        StartCoroutine(Removerreqreq(usernames));
        Debug.Log(usernames);

    }
    
    public void Removeff(string usernames)
    {
        StartCoroutine(Removeffreq(usernames));
        Debug.Log(usernames);
    }
    public void Onclicksch()
    {
        StartCoroutine(Search());
    }
    public void Addff(string usernames)
    {
        StartCoroutine(Addfreq(usernames));
        Debug.Log(usernames);
    }
    public void Play(string usernames)
    {
        StartCoroutine(reqplay(usernames));
        Debug.Log(usernames);
    }

    IEnumerator reqplay(string user1)
    {
        string usernametext1 = user1;
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("username1", usernametext1);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/reqplay.php", form);
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

                obj1.gameObject.SetActive(false);
            }

            Debug.Log("hello");
        }
    }
    IEnumerator Addfreq(string user1)
    {
        string usernametext1 = user1;
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("username1", usernametext1);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/addfff.php", form);
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

                obj1.gameObject.SetActive(false);
            }

            Debug.Log("hello");
        }
    }
    IEnumerator Search()
    {
        string usernametext = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        string uq = schuq.text;
        WWWForm form = new WWWForm();
        form.AddField("username", usernametext);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        form.AddField("uq", uq);

        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/addf.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Usersinfoss m = JsonUtility.FromJson<Usersinfoss>(jsonString);
            obj1.gameObject.SetActive(true);
            dn.text = m.displayname;
            xpp.text = m.xp;
            lvl.text = m.level;
            Sprite[] avatars = Resources.LoadAll<Sprite>("Avatars");
            int avnum = m.avatar;
            Debug.Log("this is avatar num: " + avnum);
            if (avnum == 10)
            {
                avnum = 0;
            }
            avv.sprite = avatars[avnum];
            int st = m.status;
            string usernametexts =m.username;
            if (st == 0)
            {
                obj2.gameObject.SetActive(false);
                obj3.gameObject.SetActive(false);
                obj4.gameObject.SetActive(false);
                obj5.gameObject.SetActive(true);
                obj5.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                obj5.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Addff(usernametexts); });

            }
            if (st == 1)
            {
                obj2.gameObject.SetActive(true);
                obj3.gameObject.SetActive(false);
                obj4.gameObject.SetActive(false);
                obj5.gameObject.SetActive(false);
                obj2.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                obj2.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Play(usernametext); });
                obj2.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                obj2.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Removeff(usernametexts); });

            }
            if (st == 2)
            {
                obj2.gameObject.SetActive(false);
                obj3.gameObject.SetActive(true);
                obj4.gameObject.SetActive(false);
                obj5.gameObject.SetActive(false);
                obj3.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                obj3.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Removerrreq(usernametexts); });

            }
            Debug.Log(jsonString);

        }

    }
    public class Usersinfoss
    {
        public string level;
        public string xp;
        public string balance;
        public string date_singup;
        public string lastseen;
        public int avatar;
        public string displayname;
        public string uniqiddecode;
        public string username;
        public int status;
    }
    IEnumerator Removeffreq(string user1)
    {
        string usernametext1 = user1;
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("username1", usernametext1);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/removeff.php", form);
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
                foreach (Transform child in contentobj)
                {
                    GameObject.Destroy(child.gameObject);
                }

                StartCoroutine(regusername());
                obj1.gameObject.SetActive(false);
            }

            Debug.Log("hello");
        }
    }
    IEnumerator Removerreq(string user1)
    {
        string usernametext1 = user1;
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("username1", usernametext1);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/remover.php", form);
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
                foreach (Transform child in contentobj)
                {
                    GameObject.Destroy(child.gameObject);
                }

                StartCoroutine(regusername());
                obj1.gameObject.SetActive(false);
            }

            Debug.Log("hello");
        }
    }
    IEnumerator Removerreqreq(string user1)
    {
        string usernametext1 = user1;
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("username1", usernametext1);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/removereq.php", form);
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
                foreach (Transform child in contentobj)
                {
                    GameObject.Destroy(child.gameObject);
                }

                StartCoroutine(regusername());
                obj1.gameObject.SetActive(false);
            }

            Debug.Log("hello");
        }
    }
    IEnumerator Addreq(string user1)
    {
        string usernametext1 = user1;
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("username1", usernametext1);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/addff.php", form);
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
                foreach (Transform child in contentobj)
                {
                    GameObject.Destroy(child.gameObject);
                }

                StartCoroutine(regusername());
                obj1.gameObject.SetActive(false);
            }

            Debug.Log("hello");
        }
    }
        IEnumerator Showfriend(string user,int st)
    {
        string usernametext = user;      
        WWWForm form = new WWWForm();
        form.AddField("username", usernametext);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/userinfos.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {

            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Usersinfos m = JsonUtility.FromJson<Usersinfos>(jsonString);
            obj1.gameObject.SetActive(true);
            dn.text = m.displayname;
            xpp.text = m.xp;
            lvl.text = m.level;
            Sprite[] avatars = Resources.LoadAll<Sprite>("Avatars");
            int avnum = m.avatar;
            Debug.Log("this is avatar num: " + avnum);
            if (avnum == 10)
            {
                avnum = 0;
            }
            avv.sprite = avatars[avnum];
            if (st==1)
            {
                obj2.gameObject.SetActive(true);
                obj3.gameObject.SetActive(false);
                obj4.gameObject.SetActive(false);
                obj2.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                obj2.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Play(usernametext); });
                obj2.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                obj2.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Removeff(usernametext); });

            }
            if (st == 0)
            {
                obj2.gameObject.SetActive(false);
                obj3.gameObject.SetActive(false);
                obj4.gameObject.SetActive(true);
                obj4.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                obj4.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Add(usernametext);  });
                obj4.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                obj4.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { Remover(usernametext);  });
            }
            if (st == 2)
            {
                obj2.gameObject.SetActive(false);
                obj3.gameObject.SetActive(true);
                obj4.gameObject.SetActive(false);
            }
            Debug.Log(jsonString);
        }
        Debug.Log(user);

    }
    public void Backtomain()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }
    IEnumerator Userinfo()
    {
        string usernametext = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("username", usernametext);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);

        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/userinfo.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Usersinfo ms = JsonUtility.FromJson<Usersinfo>(jsonString);
            uniqcode.text = "Unique ID:" + ms.uniqiddecodeuis;
            displayname.text = ms.displayname;
            xp.text = ms.xp;
            level.text = ms.level;
            Sprite[] avatars = Resources.LoadAll<Sprite>("Avatars");
            int avnum = ms.avatar;
            Debug.Log("this is avatar num: " + avnum);
            if (avnum == 10)
            {
                avnum = 0;
            }
            av.sprite = avatars[avnum];


        }
    }

     IEnumerator regusername()
    {
        string usernametext = PlayerPrefs.GetString("username");
        Debug.Log(usernametext);
        WWWForm form = new WWWForm();
        form.AddField("username", usernametext);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/json.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            //  string json = "[{Name:'John Simith',Age:35},{Name:'Pablo Perez',Age:34}]";
            Debug.Log(www.downloadHandler.text);
            
            Sprite[] avatars = Resources.LoadAll<Sprite>("Avatars");
            Users[] m = JsonConvert.DeserializeObject<Users[]>(jsonString);                          
            for (int i=0;i<m.Length;i++)
            {

                
                GameObject newItem = Instantiate(freindItem) as GameObject;
                newItem.SetActive(true);
                newItem.name = "friend" + i;
                newItem.transform.SetParent(content.transform, false);
                Text[] newText = newItem.GetComponentsInChildren<Text>();
                Image[] img = newItem.GetComponentsInChildren<Image>();
                Button newButton = newItem.GetComponent<Button>();
                string name = m[i].username;
                int st = m[i].status;
                newButton.onClick.AddListener(delegate { Oncsfs(name, st); });
                newText[0].text = m[i].displayname;//DisplayName
                newText[1].text = "Unique ID:" + m[i].uniqiddecode;//unique ID
                newText[2].text =  m[i].level;//Level
                int avnum = m[i].avatar;
                Debug.Log("this is avatar num: " + avnum);
                if (avnum == 10)
                {
                    avnum = 0;
                }
                img[1].sprite = avatars[avnum];
                if (m[i].status==1) { 
                newItem.transform.GetChild(3).gameObject.SetActive(true);
                }
                else { 
                newItem.transform.GetChild(4).gameObject.SetActive(true);
                }
               
            }
           

      

        }
    }
    [System.Serializable]
    public class Usersinfo
    {
        public string level;
        public string xp;
        public string balance;
        public string date_singup;
        public string lastseen;
        public int avatar;
        public string displayname;
        public string uniqiddecodeuis;
        public string chart;
    }
    [System.Serializable]
    public class Usersinfos
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
    public class Users
    {
        public string username;
        public string level;
        public string xp;
        public int avatar;
        public string displayname;
        public string uniqiddecode;
        public int status;
    }
    [System.Serializable]
    public class Addf
    {
        public int succse;
        public string status;

    }

}
