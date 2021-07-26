using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CloudOnce;

public class GeneralGameControl : MonoBehaviour
{
    public AdTimingManager adManager;
    public BlockSpawn blockSpawn;
    public BarControl barControl;
    public GameUIManager guiManager;
    public MenuGUIManager menuManager;
    public SoundManager soundManager;
    public Shop shop;
    public GameObject inPauseBlocks;
    public GameObject inLostBlocks;

    public GameObject giveExtralifeBtn;
    public GameObject giveDoubleStarsBtn;

    public GameObject requestExtralifeBtn;
    public GameObject requestDoubleStarsBtn;

    public GameObject inGameObjects;
    public GameObject inGameCanvas;

    public ColorPalettesList colorPalettesList;

    public int [] scoresToIncreaseDifficulty;

    public Vector2 minMaxGravity;
    public int scoreOnMaxGravity;

    public Vector2 minMaxSpawnTime;
    public int scoreOnMaxSpawnTime;


    public GameObject pauseGUI;
    public GameObject leftScoreTxt;
    public int colorPaletteSelected;
    public Material [] materialsToModify;
    public Material [] materialsOfPowerUps;
    public Color [] powerUpColors;
    public Color [] guiColors;
    public Image giveLifeSprite;
    public Image giveDStarsSprite;

    public float heightToLost;
    bool paused = false;
    public bool inMenu = true;
    bool extraLifeGived = false;

    int score = 0;
    int stars = 0;

    int difficulty = 0;
    [Header("Persistance")]
    PlayerData playerDataGame;

    void Start(){

        //data
        shop.InitializeAll();
        if (SaveSystem.LoadData<PlayerData>("player.dta")==null) {
            SaveSystem.SaveData<PlayerData>(new PlayerData(0,0) ,"player.dta");
        }
        if (SaveSystem.LoadData<ProductsData>("ColorPalettes.dta")!=null) {
                ProductsData productsPersistance = SaveSystem.LoadData<ProductsData>("ColorPalettes.dta");
                colorPaletteSelected = productsPersistance.GetSelectedIndex();
        }
        //Physics.gravity = new Vector3(0, minMaxGravity.x, 0);
        UpdateMaterials();
    }

    public void UpdateMaterials(){
        for (int i=0; i<materialsToModify.Length; i++) {
            materialsToModify[i].color = colorPalettesList.colorPaletteData[colorPaletteSelected].blockColors[i];
        }
        for (int i=0; i<materialsOfPowerUps.Length; i++) {
            //materialsOfPowerUps[i].color = colorPalettesList.colorPaletteData[colorPaletteSelected].powerUpColors[i];
            materialsOfPowerUps[i].color = powerUpColors[i];
        }

        giveLifeSprite.color = materialsToModify[0].color;
        giveDStarsSprite.color = materialsToModify[1].color;

        blockSpawn.SetMaterials( materialsToModify);
        //guiManager.UpdateGUIColors(colorPalettesList.colorPaletteData[colorPaletteSelected].guiColors,
        //colorPalettesList.colorPaletteData[colorPaletteSelected].powerUpColors[0]);
        UpdateDarkWhiteColors();
    }

    public void ShowExtraLifeBtn(){
        giveExtralifeBtn.SetActive(true);
        inLostBlocks.SetActive(false);
        guiManager.DisplayRewardGUI();
    }

    public void ShowDoubleStarsBtn(){
        giveDoubleStarsBtn.SetActive(true);
        inLostBlocks.SetActive(false);
        guiManager.DisplayRewardGUI();
    }

    public void GiveDoubleStars(){
        pauseGUI.SetActive(true);
        giveDoubleStarsBtn.SetActive(false);
        inPauseBlocks.SetActive(true);
        guiManager.DisplayAfterRewardGUI();
        stars*=2;
        guiManager.starsText.text = stars.ToString();
    }

    public void GiveExtraLife(){
        extraLifeGived = true;
        guiManager.DisplayAfterRewardGUI();
        giveExtralifeBtn.SetActive(false);
        guiManager.ResetGUI();
        pauseGUI.SetActive(false);
        inPauseBlocks.SetActive(false);
        inLostBlocks.SetActive(false);

        inGameCanvas.SetActive(true);
        inGameObjects.SetActive(false);
        inGameObjects.SetActive(true);

        paused = false;
        Time.timeScale = 1;

        blockSpawn.ResetForExtraLife();
        guiManager.QuitLostGUI();
        barControl.RestartPos();
    }

    public void UpdateDarkWhiteColors(){
        if (!PlayerPrefs.HasKey("Mode")) {
            PlayerPrefs.SetInt("Mode",1);
        }
        if (PlayerPrefs.GetInt("Mode")==1) {
            guiManager.UpdateGUIColors( guiColors[0], guiColors[1], guiColors[1], powerUpColors[0], false);
            materialsOfPowerUps[2].color = guiColors[0];

        }else{
            guiManager.UpdateGUIColors( guiColors[1], guiColors[0], guiColors[1], powerUpColors[0], true);
            materialsOfPowerUps[2].color = guiColors[1];

        }
    }
    void ResetScoresInGame(){
        guiManager.scoreText.text = "0";
        guiManager.starsText.text = "0";
    }

    public int GetColorCode(){
        return barControl.GetTag();
    }

    public Material GetBarMaterial(){
        return barControl.GetMaterial();
    }

#region pause, start and lose


    public void CheckForLost(float h, GameObject bar){
        if (h>=heightToLost) {
            PlayLostSound();
            barControl.alive = false;
            bar.gameObject.GetComponent<BarBehaviour>().DestroyBlock();
            StartCoroutine( OnLost());
            leftScoreTxt.SetActive(false);

            guiManager.HidePauseBtn();
        }
    }

    IEnumerator OnLost(){
        if (!Purchaser.adsPersistance.getAdsRemoved()) {
            adManager.ShowInterstitialAd();
        }

        yield return new WaitForSeconds(1.2f);

        if (extraLifeGived || !AdTiming.Agent.isRewardedVideoReady()) {
            requestExtralifeBtn.GetComponent<Button>().interactable = false;
            requestExtralifeBtn.transform.GetChild(0).GetComponent<Text>().color = guiColors[2];
            requestExtralifeBtn.transform.GetChild(1).GetComponent<Image>().color = guiColors[2];
            requestExtralifeBtn.transform.GetChild(2).GetComponent<Image>().color = guiColors[2];
        }else{
            requestExtralifeBtn.GetComponent<Button>().interactable = true;
            requestExtralifeBtn.transform.GetChild(0).GetComponent<Text>().color = guiColors[1];
            requestExtralifeBtn.transform.GetChild(1).GetComponent<Image>().color = guiColors[1];
            requestExtralifeBtn.transform.GetChild(2).GetComponent<Image>().color = guiColors[1];
        }
        extraLifeGived = false;

        if (!AdTiming.Agent.isRewardedVideoReady()) {
            requestDoubleStarsBtn.GetComponent<Button>().interactable = false;
            requestDoubleStarsBtn.transform.GetChild(0).GetComponent<Text>().color = guiColors[2];
            requestDoubleStarsBtn.transform.GetChild(1).GetComponent<Image>().color = guiColors[2];
            requestDoubleStarsBtn.transform.GetChild(2).GetComponent<Image>().color = guiColors[2];
        }else{
            requestDoubleStarsBtn.GetComponent<Button>().interactable = true;
            requestDoubleStarsBtn.transform.GetChild(0).GetComponent<Text>().color = guiColors[1];
            requestDoubleStarsBtn.transform.GetChild(1).GetComponent<Image>().color = guiColors[1];
            requestDoubleStarsBtn.transform.GetChild(2).GetComponent<Image>().color = guiColors[1];
        }

        paused = !paused;
        Time.timeScale = paused?0:1;
        guiManager.DisplayLostGUI();
        inLostBlocks.SetActive(true);

    }

    public void SaveActualPlayerData(){
        playerDataGame = SaveSystem.LoadData<PlayerData>("player.dta");

        if (score > playerDataGame.getScore()) {
            playerDataGame.setScore(score);
            Leaderboards.HighScore.SubmitScore(score);
        }
        playerDataGame.setStars(playerDataGame.getStars()+stars);
        SaveSystem.SaveData<PlayerData>(playerDataGame ,"player.dta");
    }
    public void RestartGame(){

        ResetScoresInGame();
        guiManager.ResetGUI();
        pauseGUI.SetActive(false);
        inPauseBlocks.SetActive(false);
        inLostBlocks.SetActive(false);

        inGameCanvas.SetActive(true);
        inGameObjects.SetActive(false);
        inGameObjects.SetActive(true);

        extraLifeGived = false;
        paused = false;
        Time.timeScale = 1;
        score = 0;
        stars = 0;
        blockSpawn.timeBetweenSpawn = minMaxSpawnTime.y;
        difficulty = 0;
        blockSpawn.SetDifficulty(difficulty);
        barControl.SetDifficulty(difficulty);

        Physics.gravity = new Vector3(0, minMaxGravity.x, 0);
        guiManager.scoreText.text = score.ToString();
        guiManager.starsText.text = stars.ToString();
        guiManager.QuitLostGUI();
        blockSpawn.ResetBlockSpawn();
        barControl.RestartPos();


    }

    public void RestartGameSavingData(){
        SaveActualPlayerData();
        RestartGame();
    }
    public void PauseGame(){
        paused = !paused;
        pauseGUI.SetActive(!pauseGUI.activeSelf);
        inPauseBlocks.SetActive(!inPauseBlocks.activeSelf);
        Time.timeScale = paused?0:1;
    }
#endregion

#region during game functions

    public void DestroyWithBomb(){
        blockSpawn.DestroyOnlyBlocks();
    }

    public void PlayLostSound(){
        soundManager.PlaySound(1);
    }
    public void PlayErrorSound(){
        soundManager.PlaySound(3);
    }
    public void PlayPowerUpSound(){
        soundManager.PlaySound(2);
    }
    public void PlayScoreSound(){
        soundManager.PlaySound(4);
    }
    public void UpdateScore(){
        score+=1;
        bool increaseDif = false;
        for (int i=0; i<scoresToIncreaseDifficulty.Length; i++) {
                if (score == scoresToIncreaseDifficulty[i]) {
                    increaseDif = true;
                    i = scoresToIncreaseDifficulty.Length;
                }
        }
        if (increaseDif) {
            difficulty +=1;
            blockSpawn.SetDifficulty(difficulty);
            barControl.SetDifficulty(difficulty);
        }
        float parameter = Mathf.InverseLerp(0, scoreOnMaxGravity, score);
        Physics.gravity = new Vector3(0, minMaxGravity.x+(minMaxGravity.y-minMaxGravity.x)*parameter, 0);

        float timeParam = Mathf.InverseLerp(0, scoreOnMaxSpawnTime, score);
        blockSpawn.timeBetweenSpawn = minMaxSpawnTime.y-(minMaxSpawnTime.y-minMaxSpawnTime.x)*timeParam;

        guiManager.scoreText.text = score.ToString();
    }
    public void UpdateStars(){
        stars+=1;
        guiManager.starsText.text = stars.ToString();
    }

    public void UpdateStarsText(){
        playerDataGame = SaveSystem.LoadData<PlayerData>("player.dta");
        stars = playerDataGame.getStars();
        guiManager.starsText.text = stars.ToString();
    }

    public void ErrorGUI(bool b){
        guiManager.getDamage = b ;
    }
#endregion

#region navigation
    public void GoToMenu(){
        SaveActualPlayerData();
        pauseGUI.SetActive(false);
        inPauseBlocks.SetActive(false);
        inGameCanvas.SetActive(false);
        inGameObjects.SetActive(false);
        paused = false;
        Time.timeScale = 1;
        guiManager.QuitLostGUI();
        guiManager.HidePauseBtn();
        menuManager.DisplayMenu();
        blockSpawn.DestroyChildsWithoutExplosion();
    }

    public void BorrarDatos(){
        SaveSystem.DeleteFile("adsPersistance.dta");
        SaveSystem.DeleteFile("ColorPalettes.dta");
        SaveSystem.DeleteFile("player.dta");
        PlayerPrefs.SetInt("tutorial",1);
        PlayerPrefs.SetInt("Mode",0);
        PlayerPrefs.SetInt("enterTimes",0);
        PlayerPrefs.SetInt("displayReviewReq",1);
    }

    public void DebugRemoveAds(){
        Debug.Log("you have removed ads");
        AdsPersistance adsPersistance = new AdsPersistance(true);
        SaveSystem.SaveData<AdsPersistance>(adsPersistance ,"adsPersistance.dta");
    }
    public void DebugSetAds(){
        Debug.Log("ads active");
        AdsPersistance adsPersistance = new AdsPersistance(false);
        SaveSystem.SaveData<AdsPersistance>(adsPersistance ,"adsPersistance.dta");
    }

#endregion


}
