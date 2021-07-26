using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public Text scoreText;
    public Text starsText;
    public Text shopText;
    public Text gameoverText;
    public Text contText;

    public Image starImage;
    public GameObject scoreToChangeText;
    public bool getDamage = false;
    public Image bloodImage;
    public Color damageColor;
    public GameObject lostGUI;
    public GameObject pauseBtn;

    public Image toggleBtnSprite;
    public Image removeSprite;
    public Image muteSprite;
    public Image creditsSprite;
    public Image leaderboardSprite;
    public Image backSprite;
    public Image pauseSprite;



    public SpriteRenderer bgSprite;
    public SpriteRenderer bgSprite2;
    public SpriteRenderer bgSprite3;
    public GameObject generalBg;

    public Sprite removeW;
    public Sprite removeB;
    public Sprite toggleW;
    public Sprite toggleB;

    public Image sun;
    public Image moon;

    public Material bg1;
    public Material bg2;
    public Material particles;
    public Shop shop;
    public GameObject reviewCanvas;

    void Start(){
        if (!PlayerPrefs.HasKey("enterTimes")) {
            PlayerPrefs.SetInt("enterTimes",0);
        }
        if (!PlayerPrefs.HasKey("displayReviewReq")) {
            PlayerPrefs.SetInt("displayReviewReq",1);
        }
        PlayerPrefs.SetInt("enterTimes",PlayerPrefs.GetInt("enterTimes")+1);

        if (PlayerPrefs.GetInt("displayReviewReq")==1 && (PlayerPrefs.GetInt("enterTimes")==5 || PlayerPrefs.GetInt("enterTimes")==15) ) {
            reviewCanvas.SetActive(true);
        }

    }

    public void ReviewInGooglePlay(){
        reviewCanvas.SetActive(false);
        PlayerPrefs.SetInt("displayReviewReq",0);
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.CursedPinkyDreams.ColorStack");
    }

    public void CloseReviewRequest(){
        reviewCanvas.SetActive(false);
        if (PlayerPrefs.GetInt("enterTimes")==15) {
            PlayerPrefs.SetInt("displayReviewReq",0);
        }
    }

    void Update()
    {
        if (getDamage)
        {
            bloodImage.color = Color.Lerp(bloodImage.color, damageColor, 7 * Time.deltaTime);
            if (bloodImage.color.a >= 0.8) //Almost Opaque, close enough
            {
                getDamage = false;
            }
        }
        if (!getDamage)
        {
            Color Transparent = new Color(1, 1, 1, 0);
            bloodImage.color = Color.Lerp(bloodImage.color, Transparent, 3 * Time.deltaTime);
        }
    }

    public void HidePauseBtn(){
        pauseBtn.SetActive(false);
    }

    public void ResetGUI(){
        getDamage = false;
        bloodImage.color = new Color(1, 1, 1, 0);
    }

    public void DisplayLostGUI(){
        lostGUI.SetActive(true);
        pauseBtn.SetActive(false);
    }
    public void QuitLostGUI(){
        lostGUI.SetActive(false);
        pauseBtn.SetActive(true);
    }

    public void DisplayAfterRewardGUI(){
        generalBg.SetActive(false);
    }
    public void DisplayRewardGUI(){
        generalBg.SetActive(true);
        lostGUI.SetActive(false);
    }

    public void UpdateGUIColors(Color txtColor, Color bgColor, Color barCounter, Color starColor, bool dark){
        scoreText.color = txtColor;
        starsText.color = txtColor;
        shopText.color = txtColor;
        gameoverText.color = txtColor;
        contText.color = txtColor;


        shop.selectedColor = txtColor;
        shop.UpdateSelectedItem();

        scoreToChangeText.GetComponent<Renderer>().material.color = barCounter;
        bg1.color = bgColor;
        bg2.color = bgColor;

        Color tempColor = bgColor;
        tempColor.a = 0.8f;
        bgSprite.color = tempColor;
        bgSprite2.color = tempColor;
        bgSprite3.color = tempColor;

        starImage.color = starColor;
        sun.color=txtColor;
        moon.color=txtColor;
        muteSprite.color = txtColor;
        creditsSprite.color = txtColor;
        leaderboardSprite.color = txtColor;
        backSprite.color = txtColor;
        pauseSprite.color = txtColor;

        particles.color = txtColor;

        if (dark) {
            toggleBtnSprite.sprite = toggleW;
            removeSprite.sprite = removeW;
        }else{
            toggleBtnSprite.sprite = toggleB;
            removeSprite.sprite = removeB;
        }

    }
}
