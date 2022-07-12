using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Threading.Tasks;
//using SimpleJSON;
//using System.Linq;

public class ApiManager : MonoBehaviour
{ 
    // Api Settings fields
    private const string URL = "https://newsdata.io/api/1/news";
    private const string YOUR_API_KEY = "pub_910824786fe1d1aa9974a71bf8ff1599e8fd";
    private const string COUNTRY = "ca";
    private const string CATEGORY = "technology";
    private const string LANGUAGE = "en";
    
    // build the url and query
    private string request =  string.Format("{0}?apikey={1}&country={2}&category={3}&language={4}", URL, YOUR_API_KEY, COUNTRY, CATEGORY, LANGUAGE);
   
    // Articles from Api response json as C# object
    public List<Results> articles { get; set; }

    // UI Manager instance

    private UIManager ui;

    // Fields for Image conversion 

    private List<Texture2D> textureList;
    private List<Sprite> spriteList;

    async void Awake()
    {
        ui = GetComponent<UIManager>();
       
        // Make Api GET request to NewsApi server

        //StartCoroutine(GetData(request));


        // JSON deserealization to objects

        articles = GetResults();

        //Get Images from the internet
        
        StartCoroutine(DownloadImage(articles[1].image_url, 0));
        StartCoroutine(DownloadImage(articles[5].image_url, 1));
        //StartCoroutine(DownloadImage(articles[1].image_url, 2));


    }

   

    // IEnumerator for Couroutine
    IEnumerator DownloadImage(string MediaUrl, int indexOfImage)
    {
        if (MediaUrl != null)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
            {
                // ImageComponent.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;

                Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                ui.ImageList[indexOfImage].GetComponent<Image>().overrideSprite = sprite;
            }
        }
    }
    // sends an API request - returns a JSON file
    IEnumerator GetData(string request)
    {

        // create the web request and download handler
        UnityWebRequest webReq = new UnityWebRequest();
        webReq.downloadHandler = new DownloadHandlerBuffer();
        
        webReq.url = request;
        // send the web request and wait for a returning result
        yield return webReq.SendWebRequest();
        
        // Check WebRequest results
        switch (webReq.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(": Error: " + webReq.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(": HTTP Error: " + webReq.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(":\nReceived: " + webReq.downloadHandler.text);
                break;

        }
        File.WriteAllText(@"D:\Documents\GameDev\Application\NewsWebsite\NewsWebsite\Assets\JSON Response\response.json", webReq.downloadHandler.text);
    }

    //Read JSON Response from json file.
    private List<Results> GetResults() 
    {
        //File.ReadAllText(@"D:\Documents\GameDev\Application\NewsWebsite\NewsWebsite\Assets\JSON Response\response.json"
        RootObject APIResults = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(@"D:\Documents\GameDev\Application\NewsWebsite\NewsWebsite\Assets\JSON Response\response.json"));
        return APIResults.results;

    }
  
}

