using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results : MonoBehaviour
{
    public string title { get; set; }

    public string link { get; set; }
    public string source_id { get; set; }
    public string[] keywords { get; set; }
    public string[] creator { get; set; }
    public string image_url { get; set; }
    public string video_url { get; set; }

    public string description { get; set; }

    public string pubDate { get; set; }
    public string content { get; set; }
    public string[] country { get; set; }
    public string[] category { get; set; }
    public string language { get; set; }
}
