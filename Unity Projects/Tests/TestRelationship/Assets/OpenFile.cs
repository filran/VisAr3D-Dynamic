using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class OpenFile : MonoBehaviour {

    public string fileName;

	// Use this for initialization
    void Start()
    {
        Debug.Log(readTextFile(fileName));

        FileInfo theSourceFile = new FileInfo(fileName);
        StreamReader reader = theSourceFile.OpenText();
        string text;
        do
        {
            text = reader.ReadLine();
            //Console.WriteLine(text);
            //print(text);
            //GetComponent<Text>().text += text;
        } while (text != null);         
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private string readTextFile(string file_path)
    {
        string s = "";

        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            // Do Something with the input. 
            s += inp_ln + "\n";
        }

        inp_stm.Close();
        return s;
    }

}
