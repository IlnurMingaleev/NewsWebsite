using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CutString
{
    public static string TruncateLongString(this string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str)) return str;

        return str.Substring(0, Math.Min(str.Length, maxLength));
    }
}
