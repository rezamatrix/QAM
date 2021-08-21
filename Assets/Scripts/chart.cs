using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class chart : MonoBehaviour
{
    public GameObject chartitem;
    public GameObject content;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(regusername());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void back()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
        Debug.Log("hello");
    }
    IEnumerator regusername()
    {
        string usernametext = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        Debug.Log(usernametext);
        WWWForm form = new WWWForm();
        form.AddField("username", usernametext);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/chart.php", form);
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
            for (int i = 0; i < m.Length; i++)
            {


                GameObject newItem = Instantiate(chartitem) as GameObject;
                newItem.SetActive(true);
                newItem.name = "friend" + i;
                newItem.transform.SetParent(content.transform, false);

                string name = m[i].username;
                newItem.transform.GetChild(5).GetComponent<Text>().text= m[i].displayname;
                newItem.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<Text>().text = m[i].level;
                newItem.transform.GetChild(7).GetChild(1).GetComponent<Text>().text = m[i].xp;
                // newText[0].text = //DisplayName                
                //  newText[2].text =//Level
                int avnum = m[i].avatar;
                Debug.Log("this is avatar num: " + avnum);
                if (avnum == 10)
                {
                    avnum = 0;
                }
                //av1.sprite = avatars[avnum1];
                newItem.transform.GetChild(6).GetComponent<Image>().sprite= avatars[avnum];
                if (i == 0)
                {
                    newItem.transform.GetChild(0).gameObject.SetActive(true);
                    newItem.transform.GetChild(8).gameObject.SetActive(true);
                    newItem.transform.GetChild(8).GetComponent<Text>().text = m[i].chart.ToString();
                }
                if (i==1) {
                    newItem.transform.GetChild(1).gameObject.SetActive(true);
                    newItem.transform.GetChild(8).gameObject.SetActive(false);
                    newItem.transform.GetChild(9).gameObject.SetActive(true);
                    newItem.transform.GetChild(9).GetComponent<Text>().text = m[i].chart.ToString();
                }
                if (i == 2)
                {
                    newItem.transform.GetChild(2).gameObject.SetActive(true);
                    newItem.transform.GetChild(9).gameObject.SetActive(false);
                    newItem.transform.GetChild(10).gameObject.SetActive(true);
                    newItem.transform.GetChild(10).GetComponent<Text>().text = m[i].chart.ToString();
                }
                if (i ==3)
                {
                    newItem.transform.GetChild(3).gameObject.SetActive(true);
                    newItem.transform.GetChild(10).gameObject.SetActive(false);
                    newItem.transform.GetChild(11).gameObject.SetActive(true);
                    newItem.transform.GetChild(11).GetComponent<Text>().text = m[i].chart.ToString();
                }
                if (i > 3  && i<=10)
                {
                    newItem.transform.GetChild(4).gameObject.SetActive(true);
                    newItem.transform.GetChild(11).gameObject.SetActive(false);
                    newItem.transform.GetChild(12).gameObject.SetActive(true);
                    newItem.transform.GetChild(12).GetComponent<Text>().text = m[i].chart.ToString();
                }
                //img[1].sprite = avatars[avnum];

                //newItem.transform.GetChild(3).gameObject.SetActive(true);


                // newItem.transform.GetChild(4).gameObject.SetActive(true);
                Debug.Log(jsonString);

            }




        }
    }
    [System.Serializable]
    public class Users
    {
        public string displayname;
        public int avatar;
        public string xp;
        public string level;
        public string username;
        public int chart;
    }
}
