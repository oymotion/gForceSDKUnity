using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gf;

public class GetDatatoControl : MonoBehaviour {

    public GameObject gforce = null;
    public Quaternion quater; // for display in editor only

    public byte[] Rawdata;

    public GameObject CH1;
    public GameObject CH2;
    public GameObject CH3;
    public GameObject CH4;
    public GameObject CH5;
    public GameObject CH6;
    public GameObject CH7;
    public GameObject CH8;

    bool notifSwitchSet = false;
    public GForceDevice dev;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (!notifSwitchSet)
        {
            Device.ConnectionStatus status = dev.device.getConnectionStatus();

            if (status == Device.ConnectionStatus.Connected)
            {
                dev.device.setNotification((uint)(gf.DataNotifFlags.DNF_EMG_RAW | gf.DataNotifFlags.DNF_QUATERNION));
                notifSwitchSet = true;
            }
        }

        GForceDevice gforceDevice = gforce.GetComponent<GForceDevice>();

        quater = gforceDevice.quater;      //Get gForce quater
        Rawdata = gforceDevice.EMGRawdata; //Get gForce Rawdata
    }

    void OnGUI()
    {

        //Show Quater
        GUI.Label(new Rect(12, 68, Screen.width, Screen.height), "Quater.W : " + quater.w);
        GUI.Label(new Rect(12, 98, Screen.width, Screen.height), "Quater.X : " + quater.x);
        GUI.Label(new Rect(12, 128, Screen.width, Screen.height), "Quater.Y : " + quater.y);
        GUI.Label(new Rect(12, 158, Screen.width, Screen.height), "Quater.Z : " + quater.z);

        //Show Rawdata
        GUI.Label(new Rect(300, 68, Screen.width, Screen.height),  "CH_1 : " + Rawdata[0]);
        GUI.Label(new Rect(300, 98, Screen.width, Screen.height),  "CH_2 : " + Rawdata[1]);
        GUI.Label(new Rect(300, 128, Screen.width, Screen.height), "CH_3 : " + Rawdata[2]);
        GUI.Label(new Rect(300, 158, Screen.width, Screen.height), "CH_4 : " + Rawdata[3]);
        GUI.Label(new Rect(440, 68, Screen.width, Screen.height),  "CH_5 : " + Rawdata[4]);
        GUI.Label(new Rect(440, 98, Screen.width, Screen.height),  "CH_6 : " + Rawdata[5]);
        GUI.Label(new Rect(440, 128, Screen.width, Screen.height), "CH_7 : " + Rawdata[6]);
        GUI.Label(new Rect(440, 158, Screen.width, Screen.height), "CH_8 : " + Rawdata[7]);

    }
}
