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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsAndTimes : MonoBehaviour {

    public int score = 0;
    public int error = 0;
    public float spawntime = 0;
    public float GameTime = 60f;
    public Text TimeUI;
    public Text ScoreUI;

    public static PointsAndTimes instance;
    public bool canDrawLine = false;

    int gear1 = 0;
    int gear2 = 0;
    int gear3 = 0;
    int gear4 = 0;
    int gear5 = 0;

    [SerializeField]
    public Transform[] P1s = new Transform[4];
    public Transform[] P2s = new Transform[4];
    public Transform[] P3s = new Transform[4];
    public Transform[] P4s = new Transform[4];
    public Transform[] P5s = new Transform[4];
    // Use this for initialization
    void Start () {
        instance = this;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //Points And Times Control
    public void PointsAndTimesControl()
    {
        spawntime += Time.deltaTime;
        GameTime -= Time.deltaTime;

        TimeUI.text = "Time:" + GameTime.ToString("0");
        ScoreUI.text = "Score:" + score.ToString("0");

        if (GameTime <= 0)
        {
            TimeUI.text = "Time:" + 0.ToString("0");
            ArmGameControl.instance.AddProcess();
        }
    }


    //Results analysis
    public void CalculateScore()
    {
        if (score < 1000)
        {
            gear1 = 0;
        }
        else if (score >= 1000 && score < 2500)
        {
            gear1 = 1;
        }
        else if (score >= 2500 && score < 4000)
        {
            gear1 = 2;
        }
        else
        {
            gear1 = 3;
        }
        //=======================================================================
        if (error < 1000)
        {
            gear2 = 3;
        }
        else if (error >= 1000 && error < 2500)
        {
            gear2 = 2;
        }
        else if (error >= 2500 && error < 4000)
        {
            gear2 = 1;
        }
        else
        {
            gear2 = 0;
        }
        //=======================================================================

        if ((score - error) < 1000)
        {
            gear3 = 0;
        }
        else if ((score - error) >= 1000 && (score - error) < 2500)
        {
            gear3 = 1;
        }
        else if ((score - error) >= 2500 && (score - error) < 4000)
        {
            gear3 = 2;
        }
        else
        {
            gear3 = 3;
        }
        //=======================================================================
        if (score < 1000)
        {
            gear4 = 0;
        }
        else if (score >= 1000 && score < 2500)
        {
            gear4 = 1;
        }
        else if (score >= 2500 && score < 4000)
        {
            gear4 = 2;
        }
        else
        {
            gear4 = 3;
        }
        //=======================================================================
        if ((score + error) < 1000)
        {
            gear5 = 0;
        }
        else if ((score + error) >= 1000 && (score + error) < 2500)
        {
            gear5 = 1;
        }
        else if ((score + error) >= 2500 && (score + error) < 4000)
        {
            gear5 = 2;
        }
        else
        {
            gear5 = 3;
        }

        DrawLine_T.instance.Ps[0] = P1s[gear1];//手眼协调
        DrawLine_T.instance.Ps[1] = P2s[gear2];//力量控制
        DrawLine_T.instance.Ps[2] = P3s[gear3];//精度控制
        DrawLine_T.instance.Ps[3] = P4s[gear4];//反应速度
        DrawLine_T.instance.Ps[4] = P5s[gear5];//持续发力

        // Debug.Log(score.ToString() + "..." + error.ToString());
    }
}
