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
using System.Threading;
using System.Linq;


public class ApiManager : MonoBehaviour
{
    [SerializeField] private string category;
    [SerializeField] private string questionTag;
    // Api Settings fields
    private const string URL = "https://newsdata.io/api/1/news";
    private const string YOUR_API_KEY = "pub_910824786fe1d1aa9974a71bf8ff1599e8fd";
    private const string COUNTRY = "ca,au,us,se,hk";
   // private const string CATEGORY = "technology";
    private const string LANGUAGE = "en";


    private string mainRequest;

    
    // build the url and query
    [SerializeField] private string request;
   
    // Articles from Api response json as C# object
    public List<Results> articles { get; set; }

    // UI Manager instance

    private UIManager ui;

    // Fields for Image conversion 

    private List<Texture2D> textureList;
    private List<Sprite> spriteList;

    private string pathToJsonFile = @"D:\Documents\GameDev\Application\NewsWebsite\NewsWebsite\Assets\JSON Response\response.json";

    private RootObject APIResults;

    private string result;
    public string Category 
    {
        get 
        {
            return category;
        }
        set 
        {
            category = value;
        }
    }
    public string Request 
    {
        get 
        {
            return request;
        }

        set 
        {
            request = value;
        }
    }
    public string MainRequest
    {
        get
        {
            return mainRequest;
        }

        set
        {
            mainRequest = value;
        }
    }
    public string QuestionTag
    {
        get
        {
            return questionTag;
        }

        set
        {
            questionTag = value;
        }
    }
    void Start()
     {
        ui = GetComponent<UIManager>();
        mainRequest = string.Format("{0}?apikey={1}&country={2}&language={3}", URL, YOUR_API_KEY, COUNTRY, LANGUAGE);
        request = mainRequest;
     }

    public void MakeRequest() 
    {

        StartCoroutine(GetData(request));//(1)
        
    }


    // IEnumerator for Couroutine
    IEnumerator DownloadImage(string MediaUrl, int indexOfImage)
    {
        if (MediaUrl != null)
        {

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                    ui.ImageList[indexOfImage].GetComponent<Image>().overrideSprite = sprite;
                    Debug.Log(":\nReceived: " + request.downloadHandler.text);
                    break;

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
                result = webReq.downloadHandler.text;

                File.WriteAllText(pathToJsonFile, result);

                APIResults = JsonConvert.DeserializeObject<RootObject>(result);

                articles = APIResults.results;

                ArticleSortByImage();

                DownloadImages();

                ui.FillDataToUI();

                Debug.Log(":\nReceived: " + result);

                break;

        }
        
    }

    private void DownloadImages() 
    {
        for(int i = 0; i < 10; i++ ) 
        {
            StartCoroutine(DownloadImage(articles[i].image_url, i));
        }
    
    }
    public void ArticleSortByImage()
    {
        articles = (from f in articles
                    orderby f.image_url descending
                   select f).ToList<Results>();
    }


}

