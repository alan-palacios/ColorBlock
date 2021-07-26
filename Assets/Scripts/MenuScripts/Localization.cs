using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Localization : MonoBehaviour {

    public enum languages{
        Spanish,
        English
    }

    public languages language;
    public languages fallofLanguage;
    public bool production;

    private static bool created = false;

    static List<string> ids = new List<string>();
    static List<string> spanishWords = new List<string>();
    static List<string> englishWords = new List<string>();

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
        ReadCSVFile();
    }

    void ReadCSVFile(){
        TextAsset textAsset =  Resources.Load<TextAsset>("ColorBlock_Localization");

          string[] fLines = textAsset.text.Split ( '\n' );
          for ( int i=1; i < fLines.Length; i++ ) {

              string valueLine = fLines[i];
              string[] values = valueLine.Split (','); // your splitter here

              ids.Add(values[0]);
              spanishWords.Add(values[1]);
              englishWords.Add(values[2]);
          }
    }

    public string SearchWord(string id){
        int index = ids.FindIndex(x => x.StartsWith(id));

        if (production) {
            switch(Application.systemLanguage){
                case SystemLanguage.Spanish:
                    return spanishWords[index];
                case SystemLanguage.English:
                    return englishWords[index];
                default:
                    return englishWords[index];
            }
        }else{
            switch(language){
                case languages.Spanish:
                    return spanishWords[index];
                case languages.English:
                    return englishWords[index];
                default:
                    return englishWords[index];
            }
        }
    }

}
