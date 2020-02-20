using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
    public class ModelScript : MonoBehaviour
    {
        [SerializeField] private GameObject Louver0;
        [SerializeField] private GameObject Louver1;
        [SerializeField] private GameObject Louver2;

        private static List<GameObject> LouverMaterials;

        // Start is called before the first frame update
        void Start()
        {
            LouverMaterials = new List<GameObject>()
            {
                Louver0,
                Louver1,
                Louver2
            };
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClick(int num)
        {
            switch (num)
            {
                case 1:
                    NetWork_01.myProp.dic["Louver"] = 0;
                    ChangeModel("Louver", 0);
                    break;
                case 2:
                    NetWork_01.myProp.dic["Louver"] = 1;
                    ChangeModel("Louver", 1);
                    break;
                case 3:
                    NetWork_01.myProp.dic["Louver"] = 2;
                    ChangeModel("Louver", 2);
                    break;
            }
        }


        public static void ChangeModel(string model, int index)
        {
            Debug.Log("Change!");
            switch (model)
            {
                case "Louver":
                    for (int i = 0; i < LouverMaterials.Count; i++)
                    {
                        if (i == index)
                        {
                            LouverMaterials[i].SetActive(true);
                        }
                        else
                        {
                            LouverMaterials[i].SetActive(false);
                        }
                    }
                    break;
            }
        }

        private static void ChangeMaterial(string model, int index)
        {
            switch (model)
            {
                case "Louver":
                    for (int i = 0; i < LouverMaterials.Count; i++)
                    {
                        if (i == index)
                        {
                            LouverMaterials[i].SetActive(true);
                        }
                        else
                        {
                            LouverMaterials[i].SetActive(false);
                        }
                    }
                    break;
            }
        }
    }
}
