using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public string GoToScene { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Application.LoadLevel(GoToScene);
        //Application.LoadLevelAdditive(GoToScene);
        //Application.UnloadLevel(Application.loadedLevelName);
    }
}
