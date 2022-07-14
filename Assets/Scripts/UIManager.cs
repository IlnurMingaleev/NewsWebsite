using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //UI Elements
    [SerializeField] private List<TextMeshProUGUI> categories;
    [SerializeField] private List<TextMeshProUGUI> titles;

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image buttonImage;

    [SerializeField] private Sprite next;
    [SerializeField] private Sprite back;

    private bool buttonClickSwitcher;
    

    Color black = new Vector4(0, 0, 0, 1);

    // List of images in NewsPaper 
    [SerializeField] private  List<Image> imageList;

    // Api Manager instance
    private ApiManager apiManager;

    private void Start()
    {
        buttonClickSwitcher = false;
        apiManager = GetComponent<ApiManager>();
    }
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
    public void Button() 
    {

        if (!buttonClickSwitcher)
        {
            NextButton();
            buttonClickSwitcher = true;
        }
        else 
        {
            BackButton();
            buttonClickSwitcher = false;
        }
        
    
    }

    // Back button on screen of application.

    public void BackButton() 
    {
        SceneManager.LoadScene("SampleScene");
        buttonText.text = "Next";
        buttonImage.GetComponent<Image>().overrideSprite = next;
    }

    // Next button on screen of application.

    public void NextButton() 
    {
        //Make API request
        apiManager.MakeRequest();
        
        imageList[0].rectTransform.sizeDelta = new Vector2(600, 300); // Change Image Size

        buttonText.text = "Back";
        buttonImage.GetComponent<Image>().overrideSprite = back;
    }
    // Function for filling UI elements with response data
    public void FillDataToUI() 
    {
        for (int i = 0; i < 10; i++)
        {
            categories[i].text = apiManager.Articles[i].category[0].ToUpper();
            string tempStr = apiManager.Articles[i].title;
            titles[i].text = CutString.TruncateLongString(tempStr, 40) + "...";

        }
    }
    // Buttons in first scroll view
    public void ScrollButton(Button button) 
    {

        apiManager.QuestionTag = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.ToLower()[1..];
        apiManager.Request = string.Format( "{0}&q={1}",apiManager.MainRequest,apiManager.QuestionTag);
        Debug.Log(apiManager.Request);
    }

   
}

