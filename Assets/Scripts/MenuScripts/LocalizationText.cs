using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocalizationText : MonoBehaviour
{
    Text txt;
    public string id;

    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = GameObject.Find("Localization").GetComponent<Localization>().SearchWord(id);

    }

}
