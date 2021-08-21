using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class newgame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void newgames()
    {
        StartCoroutine(Newgame());
    }
    IEnumerator Newgame()
    {

        string usernametext2 = PlayerPrefs.GetString("username");
        string passwordtext = PlayerPrefs.GetString("password");
        string mode = PlayerPrefs.GetString("mode");
        WWWForm form = new WWWForm();
        form.AddField("username", usernametext2);
        form.AddField("password", passwordtext);
        form.AddField("mode", mode);
        UnityWebRequest www = UnityWebRequest.Post("http://bitcorp.ir/qaa/findgame.php", form);
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
                string id = ms.id;
                PlayerPrefs.SetString("game",id);
                SceneManager.LoadScene("Resault", LoadSceneMode.Single);
            }

            Debug.Log("hello");
        }
    }
    [System.Serializable]
    public class Addf
    {
        public int succse;
        public string status;
        public string id;

    }
}
