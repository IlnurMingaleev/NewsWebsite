using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootObject : MonoBehaviour
{
    public string status { get; set; }
    public int totalResults { get; set; }
    public List<Results> results { get; set; }
}
