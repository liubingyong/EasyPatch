using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LuaBridge : MonoBehaviour
{
    public static void LoadDataForTexture2D(Texture2D tex2d, byte[] data)
    {
        tex2d.LoadImage(data);
    }
}
