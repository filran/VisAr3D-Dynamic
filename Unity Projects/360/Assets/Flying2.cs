using UnityEngine;
using System.Collections;

public class Flying2 : MonoBehaviour {

    private bool start = false;
    private int count = 0;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Stop();
            Debug.Log(start);
        }

        //if(Input.GetKey(KeyCode.Space))
        //   {
        //       Stop();
        //       Debug.Log(start);
        //   }
    }

    void Stop()
    {
        start = !start;
    }
}
