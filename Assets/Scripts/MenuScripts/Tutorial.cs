using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public GameObject tutorial_1;
    public GameObject tutorial_2;
    public GameObject tutorialCanvas;
    public MenuGUIManager menuGManager;
    public bool fromCredits=false;
    
    public void ShowTutorial()
    {
        tutorialCanvas.SetActive(true);
    }

    public void ShowTutorialFromCredits()
    {
        fromCredits=true;
        tutorialCanvas.SetActive(true);
    }

    public void nextTutorialPage(){
        tutorial_1.SetActive(false);
        tutorial_2.SetActive(true);
    }

    public void previousTutorialPage(){
        tutorial_2.SetActive(false);
        tutorial_1.SetActive(true);
    }

    public void CloseTutorial(){
        tutorial_2.SetActive(false);
        tutorial_1.SetActive(true);
        tutorialCanvas.SetActive(false);

        PlayerPrefs.SetInt("tutorial",0);
        if (!fromCredits) {
            menuGManager.PlayGame();
        }else{
            fromCredits = true;
        }
    }
}
