using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequenceDiagram : MonoBehaviour {

	public GameObject Cam;
	public string url;
	public GameObject goLifeline;
	public GameObject goMessage;
	public GameObject goClass;

	private ParserXMI parserXMI;
	private Dictionary<Sequence,GameObject> Sequences; //diagrams
	private Dictionary<Lifeline,GameObject> Class; //diagrams

	private Dictionary<Lifeline,GameObject> Lifelines;
	private Dictionary<Message,GameObject> Messages;


	// Use this for initialization
	void Start () {
		Sequences = new Dictionary<Sequence,GameObject> ();
		Lifelines = new Dictionary<Lifeline,GameObject> ();
		Messages = new Dictionary<Message,GameObject> ();
		parserXMI = new ParserXMI (@url);

		foreach(Sequence s in parserXMI.SequenceDiagrams)
		{
			GameObject sequence = new GameObject(s.Name);

//			Diagram's loop
			if(s.Id != "EAID_C5D30B34_0645_46a3_95D3_A92E2696E07F")
			{
//				Debug.Log ("Achei o driagram");
				foreach(Lifeline l in s.Lifelines)
				{
					GameObject lifeline = (GameObject)Instantiate(goLifeline, new Vector3(0,0,0), Quaternion.identity);
					lifeline.transform.position = new Vector3(this.scale(l.Left), lifeline.transform.position.y, lifeline.transform.position.z);
					lifeline.transform.localScale = new Vector3( this.scale(l.Width),lifeline.transform.localScale.y , lifeline.transform.localScale.z );
					lifeline.name = l.Name;

					Transform txtLife = lifeline.transform.FindChild("Text");
					txtLife.GetComponent<TextMesh>().text = l.Name;
					txtLife.transform.position = new Vector3( this.scale(l.Left+(l.Width/2)), txtLife.transform.position.y, txtLife.transform.position.z);

//					Debug.Log (l.Name);
					foreach(Message m in l.Messages)
					{
//						Debug.Log("\t"+m.Name);
						GameObject message = (GameObject)Instantiate(goMessage, new Vector3(this.scale(m.PtStartX),this.scale(m.PtEndY),0), Quaternion.identity);
						message.transform.localScale = new Vector3(this.scale(m.Width),0.15f,0.2f);
						message.name = m.Name;

						GameObject txtMsg = new GameObject(m.Name);
						txtMsg.name = "Text";

						TextMesh textMsg = txtMsg.AddComponent<TextMesh>();
						textMsg.text = m.Name;
						textMsg.characterSize = 0.1f;
						textMsg.color = new Color(0,0,0);
						textMsg.alignment = TextAlignment.Center;
						textMsg.anchor = TextAnchor.MiddleCenter;
						textMsg.transform.position = new Vector3( this.scale(m.PtStartX+(m.Width/2)), this.scale(m.PtEndY), -0.12f );
												
						//save hierarchy
						lifeline.transform.parent = sequence.transform;
						message.transform.parent = lifeline.transform;
						txtMsg.transform.parent = message.transform;

						this.Messages.Add(m,message);
					}
					this.Lifelines.Add(l,lifeline);
				}
			}
			this.Sequences.Add(s,sequence);
		}
		this.directionArrow();
		this.renderClassDiagram();
	}
	
	// Update is called once per frame
	void Update () {
		movementCamera ();
	}

	private void renderClassDiagram()
	{
		int amountVerticalClass = 6;

		foreach(KeyValuePair<Sequence,GameObject> s in this.Sequences)
		{
			if(s.Key.Id == "EAID_C5D30B34_0645_46a3_95D3_A92E2696E07F")
			{
				//group
				GameObject classdiagram = new GameObject ("Class Diagram");
				
				int c = -2;
				foreach(KeyValuePair<Lifeline,GameObject> l in this.Lifelines)
				{
					int equal = 0;
					//verify if there is lifeline duplicate
					foreach(KeyValuePair<Lifeline,GameObject> ll in this.Lifelines)
					{
						if(l.Key.Name == ll.Key.Name)
						{
							equal++;
						}
					}

					//verify if there is lifeline duplicate
					if(equal <= 1)
					{
						//create class gameobject
						GameObject classd = (GameObject)Instantiate(goClass, new Vector3(c+=4,5,0), Quaternion.identity);
						float classd_x = classd.transform.position.x;
						float classd_y = classd.transform.position.y;
						float classd_z = classd.transform.position.z;
						classd.name = l.Key.Name;

						Transform methods = classd.transform.FindChild("Methods");
						methods.transform.localScale = new Vector3(methods.transform.localScale.x, l.Key.Messages.Count*0.1f , methods.transform.localScale.z);

						float methods_y = methods.position.y - (methods.localScale.y/2) + (classd.transform.FindChild("Attributes").localScale.y/2);
						methods.transform.position = new Vector3(methods.position.x, methods_y ,methods.position.z);

						//create text of the class
						GameObject txtClass = new GameObject(l.Key.Name);
						TextMesh textClass = txtClass.AddComponent<TextMesh>();
						textClass.name = "TextClass";
						textClass.text = l.Key.Name;
						textClass.characterSize = 0.1f;
						textClass.color = new Color(0,0,0);
						textClass.alignment = TextAlignment.Center;
						textClass.anchor = TextAnchor.MiddleCenter;
						textClass.transform.position = new Vector3(classd_x,classd_y,-0.12f);
						textClass.transform.parent = classd.transform;


						//create methods game object
						Transform method = classd.transform.FindChild("Methods");
						float method_y = method.position.y*1.42f;
						foreach(Message m in l.Key.Messages)
						{
							GameObject txtMethod = new GameObject(m.Name);
							TextMesh textMethod = txtMethod.AddComponent<TextMesh>();
							textMethod.name = "TextMethod";
							textMethod.text = m.Name;
							textMethod.characterSize = 0.05f;
							textMethod.color = new Color(0,0,0);
//							textMethod.alignment = TextAlignment.Center;
//							textMethod.anchor = TextAnchor.MiddleCenter;
							textMethod.transform.position = new Vector3(method.position.x+0.1f,method_y,-0.12f);
							textMethod.transform.parent = classd.transform;
							method_y-=0.1f;
						}
						
						classd.transform.parent = classdiagram.transform;
					}
				}				
			}
		}
	}

	private float scale(float n)
	{
		return n * 0.15f * 0.1f;
	}

	private void directionArrow()
	{
		foreach(KeyValuePair<Lifeline,GameObject> l in this.Lifelines)
		{
			foreach(KeyValuePair<Message,GameObject> m in this.Messages)
			{
				if(l.Key.Id == m.Key.IdSource)
				{
					foreach(KeyValuePair<Lifeline,GameObject> ll in this.Lifelines)
					{
						if(m.Key.IdTarget == ll.Key.Id)
						{
							if(l.Key.Seqno > ll.Key.Seqno)
							{
								m.Value.GetComponent<Renderer>().material.color = Color.grey;
							}
						}
					}
				}
			}
		}
	}

	private void movementCamera()
	{
		//		Translation
		float speed = 10.0f;
		if(Input.GetKey(KeyCode.RightArrow))
		{
			this.Cam.transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			this.Cam.transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			this.Cam.transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			this.Cam.transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
		}
		
		//		Zoom
		float zoomSpeed = 10.0f;
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		this.Cam.transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
		
		//		Rotate
		float minX = -360.0f;
		float maxX = 360.0f;
		float minY = -45.0f;
		float maxY = 45.0f;
		float sensX = 100.0f;
		float sensY = 100.0f;
		float rotationY = 0.0f;
		float rotationX = 0.0f;
		
		if (Input.GetMouseButton (0)) {
			rotationX += Input.GetAxis ("Mouse X") * sensX * Time.deltaTime;
			rotationY += Input.GetAxis ("Mouse Y") * sensY * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
			this.Cam.transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
		}
		
	}
}
