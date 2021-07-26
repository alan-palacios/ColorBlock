using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ProductsData
{
    // 0 selected
    // 1 owned
    // 2 to buy
    private int [] products;

    public ProductsData(int [] products){
                this.products = new int[products.Length];
              Array.Copy(products,this.products,products.Length);
   }

   public int GetProductsLength(){
       return products.Length;
   }
   public int[] GetProducts(){
             return products;
   }

   public void SetProducts(int[] products){
             this.products = products;
   }

   public void SetIndexInfo(int index, int info){
       products[index]=info;
   }
   public int GetIndexInfo(int index){
       return products[index];
   }

   public int GetSelectedIndex(){
       for (int i=0; i<products.Length; i++) {
           if (products[i]==0) {
               return i;
           }
       }
       return 0;
   }

   public void SetSelectedIndex(int index){
       for (int i=0; i<products.Length; i++) {
           if (products[i]==0) {
               products[i]=1;
           }
       }
       products[index]=0;

   }

   public void UpdateProductsList(int newLongitude){
       int[] tempArray = new int[newLongitude];
       for (int i=0; i<tempArray.Length; i++) {
           if (i<products.Length) {
               tempArray[i] = products[i];
           }else{
               tempArray[i]=2;
           }
       }
       products = new int[newLongitude];
       Array.Copy(tempArray,products,newLongitude);
   }


}
