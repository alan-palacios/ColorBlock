using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBehaviour : BlockBehaviour
{
    public BarControl bc;

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        bc.OnBarCollisionEnter(col);
    }
}
