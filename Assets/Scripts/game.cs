using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    public float timeLeft = 26;
    public Text  timetext;
    public Text op1;
    public Text op2;
    public Text op3;
    public Text op4;
    public Text q;
    public Text p1;
    public Text p2;
    public Text b;
    public Button b1;
    public Button b2;
    public Button b3;
    public Button b4;
    public Button next;
    public Button good;
    public Button bad;
    public Button back;
    public Button powerremove2answer;
    public GameObject gb;
    public GameObject power;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playreql0());
        StartCoroutine(info());
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft>=0 && timeLeft !=3600) { 
        timeLeft -= Time.deltaTime;
        int iValue = (int)timeLeft;
        timetext.text = iValue.ToString();
        }
        if (timeLeft < 0 && timeLeft != 3600)
        {
            gb.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            gb.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            gb.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
            gb.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            gb.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            gb.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            gb.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
            gb.transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
            gb.transform.gameObject.SetActive(false);
            power.transform.gameObject.SetActive(false);
            next.transform.gameObject.SetActive(true);
            good.transform.gameObject.SetActive(true);
            bad.transform.gameObject.SetActive(true);
           

        }
    }
    public void removetowoption(Button bt1, Button bt2, Button bt3, Button bt4,int ans)
    {
        StartCoroutine(addcoinreq(-60));
        StartCoroutine(info());
        Button[] but = new Button[3];
        if (ans == 1)
        {
            but[0] = bt2;
            but[1] = bt3;
            but[2] = bt4;
        }
        if (ans == 2)
        {
            but[0] = bt1;
            but[1] = bt3;
            but[2] = bt4;
        }
        if (ans == 3)
        {
            but[0] = bt1;
            but[1] = bt2;
            but[2] = bt4;
        }
        if (ans == 4)
        {
            but[0] = bt1;
            but[1] = bt2;
            but[2] = bt3;
        }
        int reand1 = Random.Range(0, 2);
        int reand2 = Random.Range(0, 2);
        if (reand2== reand1 && reand1==1 && reand2==1) {
            reand1 = 2;
        }
        if (reand2 == reand1 && reand1 == 0 && reand2 == 0)
        {
            reand1 = 2;
        }
        if (reand2 == reand1 && reand1 == 2 && reand2 == 2)
        {
            reand1 = 0;
        }
        Button bt1t = but[reand1];
        Button bt2t = but[reand2];
        bt1t.transform.gameObject.SetActive(false);
        bt2t.transform.gameObject.SetActive(false);
        Debug.Log("b2");
        powerremove2answer.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void backtor(int crt)
    {
        Debug.Log("crt: 1"+crt);
        if (crt+1==6)
        {
            StartCoroutine(lvlup());
            Debug.Log("crt: 2" );
        }
        if (crt + 1 == 12)
        {
            StartCoroutine(lvlup());
            Debug.Log("crt: 3" );
        }
        if (crt + 1 == 18)
        {
            StartCoroutine(lvlup());
            Debug.Log("crt: 4" );
        }
        if (crt + 1 == 24)
        {
            StartCoroutine(lvlup());
            Debug.Log("crt: 5" );
        }
        if (crt + 1 == 30)
        {
            StartCoroutine(lvlup());
            Debug.Log("crt: 6" );
        }
        SceneManager.LoadScene("Resault", LoadSceneMode.Single);
    }
    IEnumerator lvlup()
    {
        string id = PlayerPrefs.GetString("game");
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("username", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
    
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/lvlup.php", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Debug.Log(jsonString);
            Addfs ms = JsonUtility.FromJson<Addfs>(jsonString);
            int hello = ms.succse;
            if (hello == 1)
            {


            }

            Debug.Log("lvlup");
        }
    }
    IEnumerator info()
    {
        string id = PlayerPrefs.GetString("game");
        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("username", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);

        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/pb.php", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Debug.Log(jsonString);
            Topui ms = JsonUtility.FromJson<Topui>(jsonString);
            b.text = ms.balance.ToString();
            p1.text = ms.p1.ToString();
            p2.text = ms.p2.ToString();

            Debug.Log("lvlup");
        }
    }
    public void nextq(int round,int rr)
    {
        if (round ==1000) { back.transform.gameObject.SetActive(true);
            back.GetComponent<Button>().onClick.AddListener(delegate { backtor(rr); });
            timeLeft = 3600;
            next.transform.gameObject.SetActive(false);
            good.transform.gameObject.SetActive(false);
            bad.transform.gameObject.SetActive(false);
        } else { 
        gb.transform.gameObject.SetActive(true);
        power.transform.gameObject.SetActive(true);
        next.transform.gameObject.SetActive(false);
        good.transform.gameObject.SetActive(false);
        bad.transform.gameObject.SetActive(false);
        next.GetComponent<Button>().onClick.RemoveAllListeners();
        StartCoroutine(playreql0());
        timeLeft = 26;
        }
        StartCoroutine(info());
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
            Addfs ms = JsonUtility.FromJson<Addfs>(jsonString);
            int hello = ms.succse;
            if (hello == 1)
            {

                

            }

            Debug.Log(avt);
        }

    }
    IEnumerator playreql0()
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
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/q.php", form);
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
            op1.text =ms.o1;
            op2.text = ms.o2;
            op3.text = ms.o3;
            op4.text = ms.o4;
            q.text = ms.q;
            int ans= ms.a;
            int r= ms.r;
            int cr= ms.cr;
            int ida= ms.id;
            b1.transform.gameObject.SetActive(true);
            b2.transform.gameObject.SetActive(true);
            b3.transform.gameObject.SetActive(true);
            b4.transform.gameObject.SetActive(true);
            b1.GetComponent<Button>().onClick.RemoveAllListeners();
            b1.GetComponent<Button>().onClick.AddListener(delegate { checker(ans, 1, ida); });
            b2.GetComponent<Button>().onClick.RemoveAllListeners();
            b2.GetComponent<Button>().onClick.AddListener(delegate { checker(ans, 2, ida); });
            b3.GetComponent<Button>().onClick.RemoveAllListeners();
            b3.GetComponent<Button>().onClick.AddListener(delegate { checker(ans, 3, ida); });
            b4.GetComponent<Button>().onClick.RemoveAllListeners();
            b4.GetComponent<Button>().onClick.AddListener(delegate { checker(ans, 4, ida); });
            next.GetComponent<Button>().onClick.RemoveAllListeners();
            if (r %3 != 0) { 
            next.GetComponent<Button>().onClick.AddListener(delegate { nextq(r, cr); });
            }
            else
            {
                next.GetComponent<Button>().onClick.AddListener(delegate { nextq(1000, cr); });
            }

            int bal = ms.balance;
            if (bal>=60)
           {
            powerremove2answer.GetComponent<Button>().onClick.RemoveAllListeners();
            powerremove2answer.GetComponent<Button>().onClick.AddListener(delegate { removetowoption(b1,b2,b3,b4, ans); });
       
                Debug.Log("b1");
           }
            
        }
    }
    IEnumerator addpoint(int idd,int ans)
    {

        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("id", idd);
        form.AddField("username", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        form.AddField("ans", ans);
        UnityWebRequest www = UnityWebRequest.Post(StaticStrings.API + "/qans.php", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Debug.Log(jsonString);
            Addfs ms = JsonUtility.FromJson<Addfs>(jsonString);
            int hello = ms.succse;
            if (hello == 1)
            {
                StartCoroutine(info());

            }

            Debug.Log("hello");
        }

    }
    public void checker(int answer,int how ,int ids)
    {
        StartCoroutine(info());
        StartCoroutine(addpoint(ids, how));
        if (answer== how)
        {
            gb.transform.GetChild(how-1).GetChild(1).gameObject.SetActive(true);
            
        }
        else
        {
            gb.transform.GetChild(how - 1).GetChild(2).gameObject.SetActive(true);
            gb.transform.GetChild(answer - 1).GetChild(1).gameObject.SetActive(true);
        }
        b1.GetComponent<Button>().onClick.RemoveAllListeners();
        b2.GetComponent<Button>().onClick.RemoveAllListeners();
        b3.GetComponent<Button>().onClick.RemoveAllListeners();
        b4.GetComponent<Button>().onClick.RemoveAllListeners();
        timeLeft = 6;
        Debug.Log(answer+ " " +how);
        StartCoroutine(info());
    }
    public class Addf
    {
        
        public string q;
        public string o1;
        public string o2;
        public string o3;
        public string o4;
        public int a;
        public int r;
        public int cr;
        public int id;
        public int balance;

    }
    [System.Serializable]
    public class Addfs
    {
        public int succse;
        public string status;
       

    }
    [System.Serializable]
    public class Topui
    {
        public int p1;
        public int p2;
        public int balance;


    }
}
