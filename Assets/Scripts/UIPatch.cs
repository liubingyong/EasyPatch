using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Basis;
using U3D.Threading;

public class UIPatch : MonoBehaviour
{
    [Component("imgBackground")]
    public Image imgBackground;

    [Component("txtMsg")]
    public Text txtMsg;

    // Use this for initialization
    void Awake()
    {
        Dispatcher.Initialize();
        DollService.Instance.Init();

        Messenger.AddListener<string>(NotiConst.UPDATE_MESSAGE, msg =>
        {
            Debug.Log("1" + msg);
            txtMsg.text = msg;
        });

        Messenger.AddListener<object>(NotiConst.UPDATE_PROGRESS, msg =>
        {
            var v = msg as string;

            if (v != null)
            {
                Debug.Log("2" + v);
                txtMsg.text = v;
            }
        });

        Messenger.AddListener<object>(NotiConst.UPDATE_DOWNLOAD, msg =>
        {
            var v = msg as string;

            if (v != null)
            {
                Debug.Log("3" + v);
                txtMsg.text = v;
            }
        });

        Messenger.MarkAsPermanent(NotiConst.UPDATE_MESSAGE);
        Messenger.MarkAsPermanent(NotiConst.UPDATE_PROGRESS);
        Messenger.MarkAsPermanent(NotiConst.UPDATE_DOWNLOAD);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
