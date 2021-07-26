using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGUIManager : MonoBehaviour
{
    public GeneralGameControl gameControl;
    public Tutorial tutorial;
    public GameObject destructibleBtnPrefab;
    public GameObject inMenuObjects;
    public GameObject menuCanvas;
    public GameObject creditsCanvas;
    public GameObject creditBtn;
    public GameObject closeShopBtn;

    public GameObject playBtn;
    GameObject playBlock;
    public float yPosPlay;

    public GameObject shopBtn;
    GameObject shopBlock;
    public float yPosShop;

    PlayerData playerDataMenu;
    public Text highScore;
    public Text totalStars;



    public void changeDarkWhiteMode(){
        int darkMode = PlayerPrefs.GetInt("Mode");
        if (darkMode==1) {
            PlayerPrefs.SetInt("Mode",0);
        }else{
            PlayerPrefs.SetInt("Mode",1);
        }
        gameControl.UpdateDarkWhiteColors();
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Mode")) {
            PlayerPrefs.SetInt("Mode",0);
        }
        if (!PlayerPrefs.HasKey("tutorial")) {
            PlayerPrefs.SetInt("tutorial",1);
        }


        playBlock = Instantiate(destructibleBtnPrefab, new Vector3(0, yPosPlay, 0), Quaternion.identity);
        playBlock.transform.parent = inMenuObjects.transform;
        Renderer renderer = playBlock.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        renderer.material = gameControl.materialsToModify[0];

        shopBlock = Instantiate(destructibleBtnPrefab, new Vector3(0, yPosShop, 0), Quaternion.identity);
        shopBlock.transform.parent = inMenuObjects.transform;

        renderer = shopBlock.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        renderer.material = gameControl.materialsToModify[1];

        UpdatePlayerData();
    }



    void UpdatePlayerData(){
        if (SaveSystem.LoadData<PlayerData>("player.dta")==null) {
            playerDataMenu = new PlayerData(0,0);
            SaveSystem.SaveData<PlayerData>(playerDataMenu ,"player.dta");
        }else{
            playerDataMenu = SaveSystem.LoadData<PlayerData>("player.dta");
        }
        highScore.text = playerDataMenu.getScore().ToString();
        totalStars.text = playerDataMenu.getStars().ToString();
    }


    /*public void GiveCoins(){
        PlayerData aux = new PlayerData(playerDataMenu.getScore(), playerDataMenu.getStars()+200);
        SaveSystem.SaveData<PlayerData>(aux ,"player.dta");
        UpdatePlayerData();
    }*/

    public void DisplayMenu(){
        menuCanvas.SetActive(true);
        creditBtn.SetActive(true);
        closeShopBtn.SetActive(false);
        UpdatePlayerData();

        Start();
    }

    public void PlayGame(){

        if (PlayerPrefs.GetInt("tutorial")==1) {
            tutorial.ShowTutorial();
        }else{
            gameControl.RestartGame();
            menuCanvas.SetActive(false);
            creditBtn.SetActive(false);
            playBlock.GetComponent<BlockBehaviour>().DestroyBlock();
            shopBlock.GetComponent<BlockBehaviour>().DestroyBlock();
        }
    }

    public void OpenShop(){

        shopBlock.GetComponent<BlockBehaviour>().DestroyBlock();
        playBlock.GetComponent<BlockBehaviour>().DestroyBlock();
        menuCanvas.SetActive(false);
        creditBtn.SetActive(false);
        closeShopBtn.SetActive(true);

    }

    public void ShowCredits(){
        creditsCanvas.SetActive(!creditsCanvas.activeSelf);
    }

    public void OpenURL(){
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Cursed+Pinky+Dreams");
    }



}
