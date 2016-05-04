using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Animation : MonoBehaviour {

    public GameObject go_message;
    public List<Message> ListMessages { get; set; }
    public Dictionary<Message , GameObject> Messages { get; set; }

    void Awake()
    {
        ListMessages = new List<Message>();
        ListMessages.Add(new Message("Message One", 0.0f, 0.0f, 1.0f));
        ListMessages.Add(new Message("Message Two", 1.0f, -1.0f, 1.0f));
        ListMessages.Add(new Message("Message Three", 2.0f, -2.0f, 1.0f));

        Messages = new Dictionary<Message, GameObject>();

        foreach (Message m in ListMessages)
        {
            GameObject msg = new GameObject();
            msg = (GameObject)Instantiate(go_message, new Vector3(m.StartX, m.StartY, 0), Quaternion.identity);
            msg.transform.FindChild("line").GetComponent<Renderer>().material.DOFade(0, 0);
            Messages.Add(m, msg);
        }
    }

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(1);

        AnimationMessage();
    }

    void AnimationMessage()
    {
        foreach(KeyValuePair<Message,GameObject> m in Messages)
        {
            //for(int i=4; i>=1; i--)
            //{
                Transform line = m.Value.transform.FindChild("line");

                line.GetComponent<Renderer>().material.DOFade(1,1);
                line.DOScaleX(m.Key.Dist , 1);
                line.DOLocalMoveX(m.Key.Dist / 2, 1);
            //}
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
