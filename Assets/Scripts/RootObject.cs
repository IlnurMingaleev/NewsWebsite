using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootObject : MonoBehaviour
{
    public string Status { get; set; }
    public int TotalResults { get; set; }
    public List<Results> Results { get; set; }
}
