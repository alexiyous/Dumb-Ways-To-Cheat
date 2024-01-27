using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// [ExecuteInEditMode()]
public class SusEyeChange : MonoBehaviour
{
    public float current = 1;
    public float maximum = 0.8f;
    public float closing;
    

    // Start is called before the first frame update
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()   
    {
        float closedAmount = current;
        if (closedAmount <= 0.8f && closedAmount >= 0 )
        {
            transform.localScale = new Vector3(0.8f,closedAmount,1);
        }
        else closedAmount = 0.8f;
        
    }
}
