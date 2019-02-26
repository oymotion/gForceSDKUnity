/*
 * Copyright 2017, OYMotion Inc.
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in
 *    the documentation and/or other materials provided with the
 *    distribution.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 * FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 * COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS
 * OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED
 * AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF
 * THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
 * DAMAGE.
 *
 */

using UnityEngine;
using gf;
using UnityEngine.UI;

public class GestureGameControl : MonoBehaviour {

    public GameObject ConnectCanvas;
    public GameObject GameCanvas;
    public GameObject Hand_left;
    public GameObject Hand_right;
    public GameObject StartButton;

    public static GestureGameControl gestureGameControl;
    public GForceDevice dev;

    int GameProcess = 0;             //Game Process Control
    public bool IsRight = false;

    // Use this for initialization
    void Start () {

        gestureGameControl = this;
        Hand_left.SetActive(false);
        Hand_right.SetActive(false);      
        StartButton.SetActive(false);
        GameCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        switch (GameProcess)
        {
            case 0:
                //Get Connection Status
                GameObject.Find("ConnectCanvas/ConnectStation").GetComponent<Text>().text = "Connecting "; //Show Gesture Name
                Device.ConnectionStatus status = dev.device.getConnectionStatus();

                if (status == Device.ConnectionStatus.Connected)
                {
                    dev.device.setNotification((uint)(gf.DataNotifFlags.DNF_EMG_GESTURE | gf.DataNotifFlags.DNF_QUATERNION));

                    StartButton.SetActive(true);
                    GameObject.Find("ConnectCanvas/ConnectStation").GetComponent<Text>().text = "Connected       "; //Show Gesture Name
                    Debug.LogFormat("ConnectionStatus: {0}", status);
                }
                break;

            case 1:

                //Arm shows
                if (IsRight)
                {
                    Hand_left.SetActive(false);
                    Hand_right.SetActive(true);
                }
                else
                {
                    Hand_right.SetActive(false);
                    Hand_left.SetActive(true);
                }

                ConnectCanvas.SetActive(false); //close ConnectCanvas
                GameCanvas.SetActive(true);
                AddProcess();
                break;

            case 2:

                //Arm  Control
                
                GestureControler.gesturecontroler.GestureControl(IsRight);
                GestureControler.gesturecontroler.ArmQuaterControl(IsRight);
                break;

            case 3:
                //Quit the game
                dev.device.disconnect();
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
                break;


        }
    }

    public void AddProcess()
    {
        GameProcess++;
    }

}
