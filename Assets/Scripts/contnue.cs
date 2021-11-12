using AssemblyCSharp;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class contnue : MonoBehaviour
{

    public Text uid;
    public Text level;
    public Text xp;
    public Text dp;
    public Image avatar;
    public GameObject newitem;
    public GameObject content;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(mainuserinfo());
        StartCoroutine(game());
    }
    public void back()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
        Debug.Log("hello");
    }
    public void resullt(string id)
    {
        PlayerPrefs.SetString("game", id);
        SceneManager.LoadScene("Resault", LoadSceneMode.Single);
        Debug.Log("hello");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void remreq(int usernames)
    {
        StartCoroutine(remgreq(usernames));
        Debug.Log(usernames);
    }
    IEnumerator remgreq(int user1)
    {
        int usernametext1 = user1;
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("id", usernametext1);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/removeinv.php", form);
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
                Transform cobj = content.transform;
                foreach (Transform child in cobj)
                {
                    GameObject.Destroy(child.gameObject);
                }
                StartCoroutine(game());

            }

            Debug.Log("hello");
        }
    }
    public void accinv(int usernames)
    {
        StartCoroutine(accinvreq(usernames));
        Debug.Log(usernames);
    }
    IEnumerator accinvreq(int user1)
    {
        int usernametext1 = user1;
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("id", usernametext1);
        form.AddField("username2", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/accinv.php", form);
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
                Transform cobj = content.transform;
                foreach (Transform child in cobj)
                {
                    GameObject.Destroy(child.gameObject);
                }
                StartCoroutine(game());

            }

            Debug.Log("hello");
        }
    }
    IEnumerator game()
    {
        string usernametext = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        Debug.Log(usernametext);
        WWWForm form = new WWWForm();
        form.AddField("username", usernametext);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/game.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
           
            Debug.Log(www.downloadHandler.text);

            Sprite[] avatars = Resources.LoadAll<Sprite>("Avatars");
            Usersgame[] m = JsonConvert.DeserializeObject<Usersgame[]>(jsonString);
            for (int i = 0; i < m.Length; i++)
            {
                GameObject newItem = Instantiate(newitem) as GameObject;
                newItem.SetActive(true);
                newItem.name = "game" + i;
                int st = m[i].status;
                newItem.transform.SetParent(content.transform, false);
                newItem.transform.GetChild(0).GetComponent<Text>().text =m[i].userbdisplayname;
                string id = m[i].id.ToString();
                // Button newButton = newitem.GetComponent<Button>();
                // newButton.onClick.AddListener(delegate { resullt(id); });
                if (st != 3)
                {
                    newItem.transform.GetChild(9).GetComponent<Button>().onClick.AddListener(delegate { resullt(id); });
                }
               
                int avnum = m[i].userbavatar;
                Debug.Log("this is avatar num: " + avnum);
                if (avnum == 10)
                {
                    avnum = 0;
                }
                newItem.transform.GetChild(2).GetComponent<Image>().sprite = avatars[avnum];
                newItem.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = m[i].userblevel;
                newItem.transform.GetChild(3).GetComponent<Text>().text = m[i].time;
                newItem.transform.GetChild(4).GetComponent<Text>().text = m[i].times;
                newItem.transform.GetChild(11).GetComponent<Text>().text = m[i].user_point_2.ToString();
                newItem.transform.GetChild(13).GetComponent<Text>().text = m[i].user_point_1.ToString();
                int p1 = m[i].user_point_1;
                int p2 = m[i].user_point_2;
                int lvl = m[i].level;
              
                if (lvl==1)
                {
                    newItem.transform.GetChild(5).gameObject.SetActive(true);
                    newItem.transform.GetChild(5).GetComponent<Text>().text = "1st Round";
                }
                if (lvl == 2)
                {
                    newItem.transform.GetChild(5).gameObject.SetActive(true);
                    newItem.transform.GetChild(5).GetComponent<Text>().text = "2nd Round";
                }
                if (lvl == 3)
                {
                    newItem.transform.GetChild(5).gameObject.SetActive(true);
                    newItem.transform.GetChild(5).GetComponent<Text>().text = "3rd Round";
                }
                if (lvl == 4)
                {
                    newItem.transform.GetChild(5).gameObject.SetActive(true);
                    newItem.transform.GetChild(5).GetComponent<Text>().text = "4th Round";
                }
                if (lvl == 5 && st == 1)
                {
                    newItem.transform.GetChild(5).gameObject.SetActive(true);
                    newItem.transform.GetChild(5).GetComponent<Text>().text = "5th Round";
                }

                if (p1==p2 && lvl==6) {
                    newItem.transform.GetChild(8).gameObject.SetActive(true);
                }
                if (p2 > p1 && lvl ==6 )
                {
                    newItem.transform.GetChild(7).gameObject.SetActive(true);
                }
                if (p2 < p1 && lvl == 6 )
                {
                    newItem.transform.GetChild(6).gameObject.SetActive(true);
                }
                if (st == 3)
                {
                    int ids = m[i].id;
                    newItem.transform.GetChild(10).gameObject.SetActive(true);
                    newItem.transform.GetChild(10).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                    newItem.transform.GetChild(10).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { accinv(ids); });
                    newItem.transform.GetChild(10).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                    newItem.transform.GetChild(10).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { remreq(ids); });

                }
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
            uid.text = m.uniqiddecode;

            xp.text = m.xp;
            level.text = m.level;
            dp.text = m.displayname;

            Sprite[] avatars = Resources.LoadAll<Sprite>("Avatars");

            int avnum = m.avatar;
            Debug.Log("this is avatar num: " + avnum);
            if (avnum == 10)
            {
                avnum = 0;
            }
            avatar.sprite = avatars[avnum];
          

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
    public class Usersgame
    {
        public string time;
        public string times;
        public int user_point_1;
        public int user_point_2;
        public string winer;
        public int status;
        public int level;
        public string userblevel;
        public int userbavatar;
        public string userbdisplayname;
        public int id;
    }
    [System.Serializable]
    public class Addf
    {
        public int succse;
        public string status;

    }
}
