using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using AssemblyCSharp;

public class result : MonoBehaviour
{
    public Text p1;
    public Text p2;
    public Text dp1;
    public Text dp2;
    public Text lvl1;
    public Text lvl2;
    public Image av1;
    public Image av2;
    public GameObject r1;
    public GameObject r2;
    public GameObject r3;
    public GameObject r4;
    public GameObject r5;
    public Button play;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(mainuserinfo());
    }
    public void playl0()
    {
        StartCoroutine(playreql0());
     
    }
    IEnumerator playreql0( )
    {

        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        string id = PlayerPrefs.GetString("game");
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("username", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/sgame.php", form);
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

                //obj1.gameObject.SetActive(false);
            }
            if (hello == 2)
            {
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
                //obj1.gameObject.SetActive(false);
            }

            Debug.Log("hello");
        }
    }
    public void nl()
    {
        StartCoroutine(nextlvl());
    }
    IEnumerator nextlvl()
    {

        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        string id = PlayerPrefs.GetString("game");
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("username", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/startg.php", form);
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
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
                //obj1.gameObject.SetActive(false);
            }
            if (hello == 0)
            {
                
              
            }

            Debug.Log("hello");
        }
    }
    public class Addf
    {
        public int succse;
        public string status;

    }
    IEnumerator mainuserinfo()
    {

        string usernametext = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        string id = PlayerPrefs.GetString("game");
        Debug.Log(usernametext);
        Debug.Log(passwordtext);
        Debug.Log(mode);

        WWWForm form = new WWWForm();
        form.AddField("username", usernametext);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        form.AddField("id", id);

        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/resullt.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Users m = JsonUtility.FromJson<Users>(jsonString);
            p1.text = m.user_point_1;
            p2.text = m.user_point_2;
            dp1.text = m.useradisplayname;
            dp2.text = m.userbdisplayname;
            lvl1.text = m.useralevel;
            lvl2.text = m.userblevel;
            //lvl2.text = ;


            Sprite[] avatars = Resources.LoadAll<Sprite>("Avatars");

            int avnum1 = m.useraavatar;
            int avnum2 = m.userbavatar;
        
            if (avnum1 == 10)
            {
                avnum1 = 0;
            }
            if (avnum2 == 10)
            {
                avnum2 = 0;
            }
            av1.sprite = avatars[avnum1];
            av2.sprite = avatars[avnum2];           
            int lvl=m.level;
            int rc1=m.cr1;
            int rc2=m.cr1;

            if (lvl == 0 && rc1==0 && rc2 == 0)
            {
                play.GetComponent<Button>().onClick.AddListener(delegate { playl0(); });
            }
            if (rc1 >= 0 && rc1 < 3)
            {
                //r1.gameObject.SetActive(true);               
                play.GetComponent<Button>().onClick.AddListener(delegate { nl(); });
            }
            if (rc1 >= 3 && rc1 < 6)
            {
               // r2.gameObject.SetActive(true);
                play.GetComponent<Button>().onClick.AddListener(delegate { nl(); });
            }
            if (rc1 >=6 && rc1 < 9)
            {
               // r3.gameObject.SetActive(true);
                play.GetComponent<Button>().onClick.AddListener(delegate { nl(); });
            }
            if (rc1 >= 9 && rc1 < 12)
            {
               // r4.gameObject.SetActive(true);
                play.GetComponent<Button>().onClick.AddListener(delegate { nl(); });
            }
            if (rc1 >= 12 && rc1 < 15)
            {
               // r5.gameObject.SetActive(true);
                play.GetComponent<Button>().onClick.AddListener(delegate { nl(); });
            }
            if ( rc1 == 15)
            {

                play.gameObject.SetActive(false);
            }
            if (m.usera1==1){r1.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); }else if (m.usera1 == 2) { r1.transform.GetChild(0).GetChild(0).gameObject.SetActive(true); }
            if (m.usera2 == 1) { r1.transform.GetChild(1).GetChild(1).gameObject.SetActive(true); } else if (m.usera2 == 2) { r1.transform.GetChild(1).GetChild(0).gameObject.SetActive(true); }
            if (m.usera3 == 1) { r1.transform.GetChild(2).GetChild(1).gameObject.SetActive(true); } else if (m.usera3 == 2) { r1.transform.GetChild(2).GetChild(0).gameObject.SetActive(true); }
            if (m.usera4 == 1) { r2.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); } else if (m.usera4 == 2) { r2.transform.GetChild(0).GetChild(0).gameObject.SetActive(true); }
            if (m.usera5 == 1) { r2.transform.GetChild(1).GetChild(1).gameObject.SetActive(true); } else if (m.usera5 == 2) { r2.transform.GetChild(1).GetChild(0).gameObject.SetActive(true); }
            if (m.usera6 == 1) { r2.transform.GetChild(2).GetChild(1).gameObject.SetActive(true); } else if (m.usera6 == 2) { r2.transform.GetChild(2).GetChild(0).gameObject.SetActive(true); }
            if (m.usera7 == 1) { r3.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); } else if (m.usera7 == 2) { r3.transform.GetChild(0).GetChild(0).gameObject.SetActive(true); }
            if (m.usera8 == 1) { r3.transform.GetChild(1).GetChild(1).gameObject.SetActive(true); } else if (m.usera8 == 2) { r3.transform.GetChild(1).GetChild(0).gameObject.SetActive(true); }
            if (m.usera9 == 1) { r3.transform.GetChild(2).GetChild(1).gameObject.SetActive(true); } else if (m.usera9 == 2) { r3.transform.GetChild(2).GetChild(0).gameObject.SetActive(true); }
            if (m.usera10 == 1) { r4.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); } else if (m.usera10 == 2) { r4.transform.GetChild(0).GetChild(0).gameObject.SetActive(true); }
            if (m.usera11 == 1) { r4.transform.GetChild(1).GetChild(1).gameObject.SetActive(true); } else if (m.usera11 == 2) { r4.transform.GetChild(1).GetChild(0).gameObject.SetActive(true); }
            if (m.usera12 == 1) { r4.transform.GetChild(2).GetChild(1).gameObject.SetActive(true); } else if (m.usera12 == 2) { r4.transform.GetChild(2).GetChild(0).gameObject.SetActive(true); }
            if (m.usera13 == 1) { r5.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); } else if (m.usera13 == 2) { r5.transform.GetChild(0).GetChild(0).gameObject.SetActive(true); }
            if (m.usera14 == 1) { r5.transform.GetChild(1).GetChild(1).gameObject.SetActive(true); } else if (m.usera14 == 2) { r5.transform.GetChild(1).GetChild(0).gameObject.SetActive(true); }
            if (m.usera15 == 1) { r5.transform.GetChild(2).GetChild(1).gameObject.SetActive(true); } else if (m.usera15 == 2) { r5.transform.GetChild(2).GetChild(0).gameObject.SetActive(true); }
            if (m.userb1 == 1) { r1.transform.GetChild(3).GetChild(1).gameObject.SetActive(true); } else if (m.userb1 == 2) { r1.transform.GetChild(3).GetChild(0).gameObject.SetActive(true); }
            if (m.userb2 == 1) { r1.transform.GetChild(4).GetChild(1).gameObject.SetActive(true); } else if (m.userb2 == 2) { r1.transform.GetChild(4).GetChild(0).gameObject.SetActive(true); }
            if (m.userb3 == 1) { r1.transform.GetChild(5).GetChild(1).gameObject.SetActive(true); } else if (m.userb3 == 2) { r1.transform.GetChild(5).GetChild(0).gameObject.SetActive(true); }
            if (m.userb4 == 1) { r2.transform.GetChild(3).GetChild(1).gameObject.SetActive(true); } else if (m.userb4 == 2) { r2.transform.GetChild(3).GetChild(0).gameObject.SetActive(true); }
            if (m.userb5 == 1) { r2.transform.GetChild(4).GetChild(1).gameObject.SetActive(true); } else if (m.userb5 == 2) { r2.transform.GetChild(4).GetChild(0).gameObject.SetActive(true); }
            if (m.userb6 == 1) { r2.transform.GetChild(5).GetChild(1).gameObject.SetActive(true); } else if (m.userb6 == 2) { r2.transform.GetChild(5).GetChild(0).gameObject.SetActive(true); }
            if (m.userb7 == 1) { r3.transform.GetChild(3).GetChild(1).gameObject.SetActive(true); } else if (m.userb7 == 2) { r3.transform.GetChild(3).GetChild(0).gameObject.SetActive(true); }
            if (m.userb8 == 1) { r3.transform.GetChild(4).GetChild(1).gameObject.SetActive(true); } else if (m.userb8 == 2) { r3.transform.GetChild(4).GetChild(0).gameObject.SetActive(true); }
            if (m.userb9 == 1) { r3.transform.GetChild(5).GetChild(1).gameObject.SetActive(true); } else if (m.userb9 == 2) { r3.transform.GetChild(5).GetChild(0).gameObject.SetActive(true); }
            if (m.userb10 == 1) { r4.transform.GetChild(3).GetChild(1).gameObject.SetActive(true); } else if (m.userb10 == 2) { r4.transform.GetChild(3).GetChild(0).gameObject.SetActive(true); }
            if (m.userb11 == 1) { r4.transform.GetChild(4).GetChild(1).gameObject.SetActive(true); } else if (m.userb11 == 2) { r4.transform.GetChild(4).GetChild(0).gameObject.SetActive(true); }
            if (m.userb12 == 1) { r4.transform.GetChild(5).GetChild(1).gameObject.SetActive(true); } else if (m.userb12 == 2) { r4.transform.GetChild(5).GetChild(0).gameObject.SetActive(true); }
            if (m.userb13 == 1) { r5.transform.GetChild(3).GetChild(1).gameObject.SetActive(true); } else if (m.userb13 == 2) { r5.transform.GetChild(3).GetChild(0).gameObject.SetActive(true); }
            if (m.userb14 == 1) { r5.transform.GetChild(4).GetChild(1).gameObject.SetActive(true); } else if (m.userb14 == 2) { r5.transform.GetChild(4).GetChild(0).gameObject.SetActive(true); }
            if (m.userb15 == 1) { r5.transform.GetChild(5).GetChild(1).gameObject.SetActive(true); } else if (m.userb15 == 2) { r5.transform.GetChild(5).GetChild(0).gameObject.SetActive(true); }



            Debug.Log(jsonString);
        }
    }
    public void back()
    {
        SceneManager.LoadScene("continue", LoadSceneMode.Single);
        Debug.Log("hello");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    [System.Serializable]
    public class Users
    {
        public string winer;
        public int status;
        public int level;
        public string user_point_1;
        public string user_point_2;
        public string useralevel;
        public int useraavatar;
        public string useradisplayname;
        public string userblevel;
        public int userbavatar;
        public string userbdisplayname;
        public int cr1;
        public int cr2;
        public int usera1;
        public int usera2;
        public int usera3;
        public int usera4;
        public int usera5;
        public int usera6;
        public int usera7;
        public int usera8;
        public int usera9;
        public int usera10;
        public int usera11;
        public int usera12;
        public int usera13;
        public int usera14;
        public int usera15;
        public int userb1;
        public int userb2;
        public int userb3;
        public int userb4;
        public int userb5;
        public int userb6;
        public int userb7;
        public int userb8;
        public int userb9;
        public int userb10;
        public int userb11;
        public int userb12;
        public int userb13;
        public int userb14;
        public int userb15;   
    }
}
