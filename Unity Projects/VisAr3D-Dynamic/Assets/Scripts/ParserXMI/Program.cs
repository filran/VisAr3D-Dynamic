using UnityEngine;
using System.Collections;


namespace ParserXMI
{
    public class Program : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            new XMI(@"C:\Users\hercules\Documents\GitHub\VisAr3D-Dynamic\EA Projects\TestEA.xmi");
            //new XMI(@"C:\Users\Filipe\Documents\GitHub\VisAr3D-Dynamic\EA Projects\TestEA.xmi");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
