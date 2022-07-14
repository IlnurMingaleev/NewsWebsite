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
    //Query fields
    [SerializeField] private string category;
    [SerializeField] private string questionTag;
    // Api Settings fields
    private const string URL = "https://newsdata.io/api/1/news";
    private const string YOUR_API_KEY = "pub_910824786fe1d1aa9974a71bf8ff1599e8fd";
    private const string COUNTRY = "ca,au,us,se,hk";
    // private const string CATEGORY = "technology";
    private const string LANGUAGE = "en";


    private string mainRequest;


    // build the url and query together
    [SerializeField] private string request;

    // Articles from Api response json as C# object
    public List<Results> Articles { get; set; }

    // UI Manager instance

    private UIManager ui;

    // Fields for Image conversion 

    // Path to Json file
    private readonly string pathToJsonFile = @"D:\Documents\GameDev\Application\NewsWebsite\NewsWebsite\Assets\JSON Response\response.json";

    // Results of response json as object
    private RootObject APIResults;

    // field for Json response string
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
        // main request is used as default request when non of question tag have been chosen
        mainRequest = string.Format("{0}?apikey={1}&country={2}&language={3}", URL, YOUR_API_KEY, COUNTRY, LANGUAGE); 
        request = mainRequest;
    }

    public void MakeRequest()
    {

        StartCoroutine(GetData(request));//(1)

    }


    // IEnumerator for Couroutine
    IEnumerator GetData(string MediaUrl, bool isImage = false, int index = 0)
    {
        UnityWebRequest request;
        if (MediaUrl != null)
        {
            if (isImage)
            {
                 request = UnityWebRequestTexture.GetTexture(MediaUrl);
            }
            else 
            {
                request = new()
                {
                    downloadHandler = new DownloadHandlerBuffer(),

                    url = MediaUrl
                };

            }
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
                    result = request.downloadHandler.text;
                    if (isImage)
                    {
                        Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                        SuccessActionsImage(tex, index);
                    }
                    else 
                    {
                        SuccessActionsArticles(result);
                    }
                    
                    Debug.Log(":\nReceived: " + result);
                    break;

            }
        }
    }
    
    //Perform Operations with loaded images
    private void SuccessActionsImage(Texture2D texture, int index) 
    {
        
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
        ui.ImageList[index].GetComponent<Image>().overrideSprite = sprite;
    }
    //Perform Operations if API request is succesful
    private void SuccessActionsArticles(string result) 
    {
        File.WriteAllText(pathToJsonFile, result); // Write result response to a json file

        APIResults = JsonConvert.DeserializeObject<RootObject>(result); // Deserialize RootObject from json string

        Articles = APIResults.Results; // Assign articles List of reult articles from API response

        ArticleSortByImage(); // Sort Articles so that articles with image appear first

        DownloadImages(); // Get Images using UnityWebRequest

        ui.FillDataToUI(); // Fill response date to UI elements so that all of 10 articles appear on application screen

    }
    //Get images for all articles where image_url is not null.
    private void DownloadImages() 
    {
        for(int i = 0; i < 10; i++ ) 
        {
            StartCoroutine(GetData(Articles[i].image_url, true, i));
        }
    
    }

    // Sorting articles in such way that first we will see articles with image
    public void ArticleSortByImage()
    {
        Articles = (from f in Articles
                    orderby f.image_url descending
                   select f).ToList<Results>();
    }


}

