using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawn : MonoBehaviour
{
    public GameObject block;
    public GameObject [] powerUpPrefabs;
    public GeneralGameControl gameControl;

    Material [] blockMaterials;
    public float timeBetweenSpawn;

    [Header("Falling")]
        public float [] xPositions;
        float lastXPos = -10f;
        public float yHeight;
        public int tryChangePosLimit;
        public int tryChangeColorLimit;
        public float treesholdForDoubleBlock;
    [Header("Probabilities")]
        public float doubleBlockProb;
        public float powerUpProb;
        public float starProb;
        public float originalDeformedBlockProb;
        float deformedBlockProb;

    int difficulty=0;
    int materialIndex;
    int lastMaterialIndex;
    private List<float> positionsToFill = new List<float>();
    private int nextPosCode = 1; //left 0, 1 middle, 2 right

    public void ResetBlockSpawn(){
        deformedBlockProb = originalDeformedBlockProb;
        lastMaterialIndex = Random.Range(0,4);
        StartCoroutine(SpawnIngredient());
        foreach (Transform child in transform) {
            child.gameObject.GetComponent<BlockBehaviour>().DestroyBlock();
        }
    }

    public void DestroyOnlyBlocks(){
        foreach (Transform child in transform) {
            if (child.gameObject.tag != "PowerUp") {
                child.gameObject.GetComponent<BlockBehaviour>().DestroyBlock();
            }
        }
    }
    public void ResetForExtraLife(){
        foreach (Transform child in transform) {
            child.gameObject.GetComponent<BlockBehaviour>().DestroyBlock();
        }
        StartCoroutine(SpawnIngredient());
    }

    public void DestroyChildsWithoutExplosion(){
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    IEnumerator SpawnIngredient(){
        yield return null;
        CalculateXPos();

        if (Random.value <= powerUpProb) {
            int powUpIndx = 0;
            if (Random.value >starProb) {
                powUpIndx = Random.Range(1,powerUpPrefabs.Length);
            }
            GameObject powerUpParent = Instantiate(powerUpPrefabs[powUpIndx], new Vector3(lastXPos, yHeight, 0), Quaternion.identity);
            powerUpParent.GetComponent<BlockBehaviour>().powerUpCode = powUpIndx;
            powerUpParent.transform.parent = transform;
            powerUpParent.tag = "PowerUp";
        }else{
            //Double Block Generation
            if(Random.value<doubleBlockProb && difficulty>5 && (lastXPos<=-treesholdForDoubleBlock || lastXPos>=treesholdForDoubleBlock) ){
                float xDoubleBlockPos = 0;
                if (lastXPos<0) {
                    xDoubleBlockPos = Random.Range(xPositions[3], xPositions[4]);
                }else{
                    xDoubleBlockPos = Random.Range(xPositions[0],xPositions[1]);
                }
                GameObject sideBlockParent = Instantiate(block, new Vector3(xDoubleBlockPos, yHeight, 0), Quaternion.identity);
                sideBlockParent.transform.parent = transform;
                UpdateMaterial(sideBlockParent);
            }

            GameObject blockParent = Instantiate(block, new Vector3(lastXPos, yHeight, 0), Quaternion.identity);
            blockParent.transform.parent = transform;
            if (UpdateMaterial(blockParent) != gameControl.GetColorCode()){
                    if (difficulty>2) {
                        if (Random.value <deformedBlockProb) blockParent.transform.localScale = new Vector3(1,2,1);
                    }
                    if (difficulty>3) {
                        if (Random.value <deformedBlockProb) blockParent.transform.localScale = new Vector3(Random.Range(1.2f,1.8f),1,1);

                    }
                    if (difficulty>4) {
                        deformedBlockProb = originalDeformedBlockProb +difficulty/30;
                        //if (Random.value <0.4f) blockParent.transform.localScale = new Vector3(2,2,1);
                    }
            }
        }

        yield return new WaitForSeconds(timeBetweenSpawn);
        StartCoroutine(SpawnIngredient());
    }

    int UpdateMaterial(GameObject g){
        for (int i = 0; i<tryChangeColorLimit; i++) {
            if (difficulty==0) materialIndex = Random.Range(0,blockMaterials.Length-2);
            if (difficulty==1) materialIndex = Random.Range(0,blockMaterials.Length-1);
            if (difficulty>=2) materialIndex = Random.Range(0,blockMaterials.Length);

            if ( ( materialIndex!= lastMaterialIndex ) || i==tryChangeColorLimit-1 ) {
                Renderer renderer = g.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                renderer.material = blockMaterials[materialIndex];
                //particle system
                renderer = g.transform.GetChild(2).gameObject.GetComponent<Renderer>();
                renderer.material = blockMaterials[materialIndex];
                g.tag = (materialIndex+1).ToString();
                lastMaterialIndex = materialIndex;
                return materialIndex+1;
            }
        }
        return materialIndex+1;
    }

    public void SetMaterials(Material [] ms){
        blockMaterials = ms;
    }

    public void SetDifficulty(int d){
        difficulty = d;
    }

    public void CalculateXPos(){
         int ind=1;

         if (nextPosCode==0) {
             ind = Random.Range(2, xPositions.Length);
         }else if(nextPosCode==1){
             ind = Random.Range(0, 2)*(xPositions.Length-1);
         }else{ // 2
             ind = Random.Range(0, 3);

         }

        if (ind<2) {
            nextPosCode = 0;
        }else if(ind==2){
            nextPosCode=1;
        }else{
            nextPosCode=2;
        }
        lastXPos = xPositions[ind];

    }

}
