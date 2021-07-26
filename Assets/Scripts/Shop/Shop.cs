using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using System;

public class Shop : MonoBehaviour
{
          [SerializeField]
          public ColorPalettesList colorPalettesList;
          public ProductsData productsPersistance;

          [Header("References")]
          private int characterSelected=0;

          private int selectedMskIndex=0;
          private int buyMskIndex=1;
          private int imagesIndex=2;
          private int selectBtnIndex=3;
          private int btnIndex=4;
          public int columns;

          public GameObject storeItem;
          public GameObject charactersGroup;

          [Header("Display Settings")]
          public Text totalStars;
          public float xPadding;
          public float yPadding;
          public float upMargin;
          public float itemHeight;
          public float minScrollHeight;
          public float minScrollWidth;
          public Vector2 initCoords = new Vector2(-200, 865);
          public Color selectedColor;

          public GeneralGameControl gameControl;

              public void CloseShop(){
                        transform.gameObject.SetActive(false);
              }

              public void SortColorPalettesList(){
                  ColorPaletteData temp;
                    // traverse 0 to array length
                    for (int i = 0; i < colorPalettesList.colorPaletteData.Length - 1; i++) {

                        // traverse i+1 to array length
                        for (int j = i + 1; j < colorPalettesList.colorPaletteData.Length; j++) {
                            // compare array element with
                            // all next element
                            if (colorPalettesList.colorPaletteData[i].order > colorPalettesList.colorPaletteData[j].order) {
                                temp = colorPalettesList.colorPaletteData[i];
                                colorPalettesList.colorPaletteData[i] = colorPalettesList.colorPaletteData[j];
                                colorPalettesList.colorPaletteData[j] = temp;
                            }
                        }
                    }
              }

              public void InitializeAll(){
                  SortColorPalettesList();
                  if (SaveSystem.LoadData<ProductsData>("ColorPalettes.dta")!=null) {
                          productsPersistance = SaveSystem.LoadData<ProductsData>("ColorPalettes.dta");
                          if (colorPalettesList.colorPaletteData.Length!=productsPersistance.GetProductsLength()) {
                              productsPersistance.UpdateProductsList(colorPalettesList.colorPaletteData.Length);
                              SaveSystem.SaveData<ProductsData>(productsPersistance ,"ColorPalettes.dta");
                          }

                  }else{
                          Debug.Log("generando products data");
                          InitializeProductData();
                  }
                  GenerateProducts();
              }

              public void OpenStore(){
                transform.gameObject.SetActive(true);
              }

              public void UpdateSelectedItem(){
                  charactersGroup.transform.GetChild(productsPersistance.GetSelectedIndex()).gameObject.transform.GetChild(selectedMskIndex).gameObject.GetComponent<Image>().color = selectedColor;
              }

              public void GenerateProducts(){
                    int rows;
                    if (colorPalettesList.colorPaletteData.Length%columns == 0) {
                              rows = colorPalettesList.colorPaletteData.Length/columns;
                    }else{
                              rows = Mathf.FloorToInt(colorPalettesList.colorPaletteData.Length/columns) + 1;
                    }
                    float scrollHeight = rows* itemHeight;
                    if (scrollHeight < minScrollHeight) {
                              scrollHeight = minScrollHeight;
                    }

                    charactersGroup.GetComponent<RectTransform>().sizeDelta = new Vector2( minScrollWidth, scrollHeight);
                    initCoords = new Vector2(-xPadding, scrollHeight/2 - upMargin);
                    charactersGroup.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, - scrollHeight/2 - 500, 0);
                    DeleteProducts();

                    int index = 0;
                    int j = 0;
                    int i = 0;

                    while(index<colorPalettesList.colorPaletteData.Length){
                              GameObject item = GameObject.Instantiate(storeItem, new Vector3(initCoords.x + i*xPadding, initCoords.y - j*yPadding, 0) , Quaternion.identity);


                              int temp = index;
                              item.transform.SetParent( charactersGroup.transform, false);

                              GameObject imagesParent = item.transform.GetChild(imagesIndex).gameObject;
                              for (int x=0; x<4; x++) {
                                  imagesParent.transform.GetChild(x).GetComponentInChildren<Image>().color= colorPalettesList.colorPaletteData[index].blockColors[x];

                              }

                              if (productsPersistance.GetIndexInfo(index)==0) {
                                  SetSelectedItem(item);
                                  item.transform.GetChild(selectBtnIndex).GetComponent<Button>().onClick.AddListener( () => SelectButton(temp) );
                              }else if(productsPersistance.GetIndexInfo(index)==1){
                                  SetSelectItem(item);
                                  item.transform.GetChild(selectBtnIndex).GetComponent<Button>().onClick.AddListener( () => SelectButton(temp) );
                              }else{
                                  SetBuyItem(item, index);
                                  item.transform.GetChild(selectBtnIndex).gameObject.SetActive(false);
                                  item.transform.GetChild(btnIndex).GetComponent<Button>().onClick.AddListener( () => BuyButton(temp) );
                                  //item.transform.GetChild(btnIndex).GetComponent<Button>().onClick.AddListener( () => gameControl.soundCtrl.PlayClickSound() );
                              }

                              //item.transform.GetChild(selectBtnIndex).GetComponent<Button>().onClick.AddListener( () => gameControl.soundCtrl.PlayClickSound() );


                              i++;
                              index++;
                              if (i==columns) {
                                        i=0;
                                        j++;
                              }

                    }

                    //SelectButton(PlayerMove.characterIndex);
                    SelectButton(productsPersistance.GetSelectedIndex());

              }

              public void SetSelectItem(GameObject item){
                  item.transform.GetChild(selectedMskIndex).gameObject.SetActive(false);
                  item.transform.GetChild(buyMskIndex).gameObject.SetActive(false);
                  item.transform.GetChild(btnIndex).gameObject.SetActive(false);
              }
              public void SetSelectedItem(GameObject item){
                  item.transform.GetChild(selectedMskIndex).gameObject.SetActive(true);
                  item.transform.GetChild(selectedMskIndex).gameObject.GetComponent<Image>().color = selectedColor;
                  item.transform.GetChild(buyMskIndex).gameObject.SetActive(false);
                  item.transform.GetChild(btnIndex).gameObject.SetActive(false);
              }
              public void SetBuyItem(GameObject item, int index){
                  item.transform.GetChild(selectedMskIndex).gameObject.SetActive(false);
                  item.transform.GetChild(buyMskIndex).gameObject.SetActive(false);
                  item.transform.GetChild(btnIndex).gameObject.SetActive(true);
                  item.transform.GetChild(btnIndex).GetComponentInChildren<Text>().text= colorPalettesList.colorPaletteData[index].price.ToString();
                  item.transform.GetChild(btnIndex).GetComponentInChildren<Text>().alignment = TextAnchor.MiddleRight;
              }

              public void SelectButton(int i){
                        GameObject oldItem = charactersGroup.transform.GetChild (characterSelected).gameObject;
                        SetSelectItem(oldItem);
                        //charactersGroup.transform.GetChild (characterSelected).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.SetActive(true);

                        GameObject newItem = charactersGroup.transform.GetChild (i).gameObject;
                        SetSelectedItem(newItem);

                        characterSelected = i;
                        gameControl.colorPaletteSelected = characterSelected;
                        gameControl.UpdateMaterials();
                        productsPersistance.SetSelectedIndex(characterSelected);
                        SaveSystem.SaveData<ProductsData>(productsPersistance ,"ColorPalettes.dta");
              }

              public void BuyButton(int i){
                  if (productsPersistance.GetIndexInfo(i)==2) {
                      GameObject item = charactersGroup.transform.GetChild (i).gameObject;
                      int price = Int32.Parse(item.transform.GetChild(btnIndex).GetComponentInChildren<Text>().text);
                      if(SaveSystem.LoadData<PlayerData>("player.dta").getStars() >= price ){
                          //buy and save
                          PlayerData pd = SaveSystem.LoadData<PlayerData>("player.dta");
                          pd.setStars(pd.getStars()-price);
                          SaveSystem.SaveData<PlayerData>(pd,"player.dta");
                          productsPersistance.SetIndexInfo(i,1);
                          SaveSystem.SaveData<ProductsData>(productsPersistance ,"ColorPalettes.dta");
                          gameControl.UpdateStarsText();
                          //UI response
                          item.transform.GetChild(btnIndex).GetComponent<Button>().onClick.RemoveListener( () => BuyButton(i) );
                          item.transform.GetChild(selectBtnIndex).gameObject.SetActive(true);
                          item.transform.GetChild(selectBtnIndex).GetComponent<Button>().onClick.AddListener( () => SelectButton(i) );
                          SelectButton(i);

                      }
                  }

              }

              public void DeleteProducts(){

                        int childCount = charactersGroup.transform.childCount;
                        for (int i = 0; i< childCount; i++) {
                                  DestroyImmediate(charactersGroup.transform.GetChild (0).gameObject) ;
                        }
              }

              public void InitializeProductData(){
                  int[] charactersInfo = new int[colorPalettesList.colorPaletteData.Length];
                  for (int i=0; i<charactersInfo.Length; i++) {
                      charactersInfo[i]=2;
                  }
                  charactersInfo[0]=0;
                  productsPersistance = new ProductsData(charactersInfo);
                  characterSelected=0;
                  SaveSystem.SaveData<ProductsData>(productsPersistance ,"ColorPalettes.dta");
              }

              [Serializable]
              public struct Product{
                       public int price;
                       public Sprite img;
                       public GameObject model;
             }
}
