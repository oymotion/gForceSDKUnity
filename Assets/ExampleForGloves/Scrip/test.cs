using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public GameObject shizhi1;
    public GameObject shizhi2;
    public GameObject shizhi3;

    int date;
    private Quaternion pos1;
    private Quaternion pos2;
    private Quaternion pos3;
    // Use this for initialization
    void Start () {
        date = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        pos1 = shizhi1.transform.localRotation;
        pos2 = shizhi2.transform.localRotation;
        pos3 = shizhi3.transform.localRotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pos1.z += Time.deltaTime ;
            pos2.z += Time.deltaTime ;
            pos3.z += Time.deltaTime ;

            Debug.Log(pos1.z);
        }

        shizhi1.transform.localRotation = pos1;
        shizhi2.transform.localRotation = pos2;
        shizhi3.transform.localRotation = pos3;
    }

}
