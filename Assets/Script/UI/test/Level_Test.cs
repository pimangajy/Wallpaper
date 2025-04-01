using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Test : MonoBehaviour
{
    public Text level_txt;
    public Text interest_txt;


    [SerializeField]
    private GameObject[] Charactors;

    void Start()
    {
        if(PlayerPrefs.HasKey("level"))
        {
            int level = PlayerPrefs.GetInt("level");
            bool interest = PlayerPrefs.GetInt("interest") >= 1 ? true : false ;

            level_txt.text = "Level : " + level;
            interest_txt.text = "Interest : " + interest;

            if(Charactors != null)
            {
                for (int i = 0; i < Charactors.Length; i++)
                {
                    Charactors[i].gameObject.SetActive(false);
                }
                Charactors[level].gameObject.SetActive(true);
            }

        }else
        {
            level_txt.text = "Level : 0";
            interest_txt.text = "Interest : false";
        }
    }


}
