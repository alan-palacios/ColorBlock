using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour
{
    public GeneralGameControl gameControl;
    public GameObject scoreToChangeText;
    public GameObject barPrefab;
    GameObject bar;

    [Header("Movement")]
    private Touch touch;
    public float speedModifier;
    public float editorSpeedModifier;
    public float incrementAtError;

    [Header("Limits")]
    public float xLimits;
    public int tryChangeColorLimit;
    public float minHeight;
    float yHeight;
    float zPos = 0;

    [Header("Materials")]
    public Material [] materials;
    int materialIndex;

    [Header("ScoreControl")]
    int scoreToChange;
    int localScore = 0;
    bool damaged = false;
    bool goDown = false;
    public bool alive = true;
    float difficulty =0;

    IEnumerator slowTimeCorutine;

    public void RestartPos(){
        yHeight = minHeight;
        transform.position = new Vector3( 0, yHeight, zPos);
        alive=true;
        if (bar!=null) {
            Destroy(bar);
        }
        bar = Instantiate(barPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        bar.GetComponent<BarBehaviour>().bc = this;
        bar.transform.SetParent(transform, false);

        localScore = 0;
        scoreToChangeText.SetActive(true);
        scoreToChange = Random.Range(3,5);
        scoreToChangeText.GetComponent<TextMesh>().text = scoreToChange.ToString();
        UpdateMaterial();
    }

    void FixedUpdate()
    {
        if (alive) {
            BarMovement();
        }
        if (damaged) {
            yHeight += incrementAtError;
            transform.position = new Vector3(
                transform.position.x,
                yHeight,
                zPos
            );

            damaged = false;
        }
        if (goDown) {
            yHeight -= incrementAtError*2;
            if (yHeight<minHeight) {
                yHeight= minHeight;
            }
            transform.position = new Vector3(
                transform.position.x,
                yHeight,
                zPos
            );

            goDown = false;
        }
    }

    void BarMovement(){
        if (Application.isEditor) {
            float amtToMove = Input.GetAxis("Horizontal") * editorSpeedModifier * Time.deltaTime;
            transform.Translate(Vector3.right * amtToMove);
            transform.position = new Vector3(ValidateXLimits(transform.position.x), yHeight, zPos);
        }


        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0 ) {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved) {
                    transform.position = new Vector3(
                        ValidateXLimits(transform.position.x + touch.deltaPosition.x * speedModifier),
                        yHeight,
                        zPos
                    );
                }
            }
        }
    }

    void UpdateMaterial(){
        for (int i = 0; i<tryChangeColorLimit; i++) {
            //materialIndex = Random.Range(0,materials.Length);
            if (difficulty==0) materialIndex = Random.Range(0,materials.Length-2);
            if (difficulty==1) materialIndex = Random.Range(0,materials.Length-1);
            if (difficulty>=2) materialIndex = Random.Range(0,materials.Length);

            if ( ( (materialIndex+1).ToString() != bar.tag ) || i==tryChangeColorLimit-1 ) {
                Renderer renderer = bar.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                renderer.material = materials[materialIndex];
                bar.tag = (materialIndex+1).ToString();
                i=tryChangeColorLimit;
            }
        }

    }

    public void SetDifficulty(int d){
        difficulty = d;
    }

    public int GetTag(){
        return materialIndex+1;
    }

    public Material GetMaterial(){
        if (bar!= null && bar.transform.GetChild(0).gameObject.GetComponent<Renderer>()!=null) {
            return bar.transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        }
        return materials[0];
    }

    void CatchCorrectBlock(Collision col){
        gameControl.UpdateScore();

        col.gameObject.GetComponent<BlockBehaviour>().DestroyBlock();
        localScore += 1;

        if (localScore>= scoreToChange) {
            UpdateMaterial();
            scoreToChange = Random.Range(3,5);
            localScore = 0;
        }
        scoreToChangeText.GetComponent<TextMesh>().text = (scoreToChange-localScore).ToString();
    }

    public void OnBarCollisionEnter(Collision col)
    {
        //gameControl.PlayLostSound();
        if (col.gameObject.tag == bar.tag) {
            gameControl.PlayScoreSound();
            CatchCorrectBlock(col);
        }else if( col.gameObject.tag == "PowerUp"){
            gameControl.PlayScoreSound();
            col.gameObject.GetComponent<BlockBehaviour>().DestroyBlock();
            int powerUpCode = col.gameObject.GetComponent<BlockBehaviour>().powerUpCode;
            switch (powerUpCode) {
                case 0:
                    CatchCorrectBlock(col);
                    gameControl.UpdateStars();
                    break;
                case 1:
                    goDown = true;
                    break;
                case 2:
                    gameControl.DestroyWithBomb();
                    break;
                case 3:
                    if (slowTimeCorutine != null)
                        StopCoroutine (slowTimeCorutine);
                     slowTimeCorutine = SlowTime();
                     StartCoroutine(slowTimeCorutine);
                    break;
            }
        }else{
            gameControl.PlayErrorSound();
            damaged = true;
            gameControl.ErrorGUI(true);
            col.gameObject.GetComponent<BlockBehaviour>().DestroyBlock();
            gameControl.CheckForLost(yHeight, bar);
        }
    }

    IEnumerator SlowTime(){
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0.7f;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0.9f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
    }

    float ValidateXLimits(float x){
        if (x<-xLimits) {
            return -xLimits;
        }
        if (x>xLimits) {
            return xLimits;
        }
        return x;
    }


}
