using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int score;
    private int stars;
    public PlayerData(int score, int stars){
              this.score = score;
              this.stars = stars;
   }

   public int getScore(){
             return score;
   }
   public int getStars(){
             return stars;
   }

   public void setScore( int score){
             this.score = score;
   }
   public void setStars(int stars){
             this.stars = stars;
   }

   public void Reiniciar(){
             score=0;
             stars=0;
   }
}
