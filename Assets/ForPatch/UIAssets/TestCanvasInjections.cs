using SuperScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCanvasInjections : MonoBehaviour {
    [Component("ScrollView_TopToBottom")]
    public LoopListView2 mLoopListView;
}
