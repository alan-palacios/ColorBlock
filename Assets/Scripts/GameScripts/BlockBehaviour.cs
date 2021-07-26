using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    public float destroyLimit = -15;
    GameObject blockFract;
    GameObject blockComplete;
    public int powerUpCode = -1;

    // Update is called once per frame

    void Start(){
        blockComplete = transform.GetChild(0).gameObject;
        blockFract = transform.GetChild(1).gameObject;
    }
    void Update()
    {
            if (transform.position.y < destroyLimit) {
                Destroy(gameObject);
            }
    }


    public void DestroyBlock(){

        if (blockComplete !=null) {
            if (transform.childCount == 3) {
                transform.GetChild(2).gameObject.SetActive(false);
            }
            blockComplete.SetActive(false);

            Destroy(GetComponent<MeshCollider>());
            Destroy(GetComponent<SphereCollider>());
            //Destroy(GetComponent<Rigidbody>());
            blockFract.SetActive(true);
            blockFract.transform.position = blockComplete.transform.position;
            if (powerUpCode==-1) {
                foreach (Transform child in blockFract.transform) {
                    child.gameObject.GetComponent<Renderer>().material = blockComplete.GetComponent<Renderer>().material;
                }
            }
            foreach (Transform child in blockFract.transform) {
                //child.gameObject.GetComponent<Renderer>().material = blockComplete.GetComponent<Renderer>().material;
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(130f, transform.position, 5f, 0.4f);
            }
            StartCoroutine(DestroyAllChild());
            Destroy(blockComplete);
        }

    }



    IEnumerator DestroyAllChild(){

        while(blockFract!=null && blockFract.transform.GetChild(0).transform.localScale.x>0){
            foreach (Transform child in blockFract.transform) {
                Vector3 scaleDecrement =  Vector3.one*0.01f;
                child.localScale -= scaleDecrement;
                if (child.localScale.x<=0) {
                    Destroy(gameObject);
                    yield break;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

}
