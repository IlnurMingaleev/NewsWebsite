using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{ 
    //UI Elements
    [SerializeField] private TextMeshProUGUI mainCategory;
    [SerializeField] private TextMeshProUGUI mainTitle;

    [SerializeField] private TextMeshProUGUI midCategory;
    [SerializeField] private TextMeshProUGUI midTitle;
    
    [SerializeField] private TextMeshProUGUI lastCategory;
    [SerializeField] private TextMeshProUGUI lastTitle;
    

    Color black = new Vector4(0, 0, 0, 1);

    // List of images in NewsPaper 
    [SerializeField] private  List<Image> imageList;

    // Api Manager instance
    private ApiManager apiManager;

    public List<Image> ImageList
    {
        get
        { 
            return imageList;
        }

        set
        { 
            imageList = value; 
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        apiManager = GetComponent<ApiManager>();

        // Filling main Container with news
        mainCategory.text = apiManager.articles[1].category[0].ToUpper();
        mainTitle.text = apiManager.articles[1].title;
        imageList[0].rectTransform.sizeDelta = new Vector2(600, 350);

        // Filling Middle Container with news
        midCategory.text = apiManager.articles[2].category[0].ToUpper();
        midTitle.text = apiManager.articles[2].title;

        // Filling Last Container with news
        lastCategory.text = apiManager.articles[3].category[0].ToUpper();
        lastTitle.text = apiManager.articles[3].title;

    }
    
}
