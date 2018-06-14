using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasInjections : MonoBehaviour {
    [Component("Text")]
    public Text txt1;
    [Component("Text (1)")]
    public Text txt2;
}
