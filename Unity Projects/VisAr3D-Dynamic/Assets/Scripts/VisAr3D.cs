using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class VisAr3D : MonoBehaviour
    {
        public string XMIFile;
        public GameObject Package;
        private float dist = .5f;

        void Start()
        {
            TheCore VisAr3D = new TheCore(XMIFile);

            float x = 0;
            foreach (Package p in VisAr3D.Packages)
            {
                GameObject package = (GameObject)Instantiate(Package, new Vector3(x, 0, 0), Quaternion.identity);
                Transform textPackage = package.transform.FindChild("name");

                package.name = p.Id;
                package.AddComponent<ChangeScene>().GoToScene = "PackageOpened";
                textPackage.GetComponent<TextMesh>().name = p.Name;
                textPackage.GetComponent<TextMesh>().text = p.Name;
                
                x = ++dist;
                DontDestroyOnLoad(package);
            }
        }
    }
}
