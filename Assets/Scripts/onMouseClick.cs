using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseClick : MonoBehaviour
{
    public int incCall = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray,out hit))
        //     {
        //         Debug.Log("coba");
        //     }
        // }
    }

    void OnMouseDown()
    {
         Debug.Log("Pssst");
            incCall = incCall + 1;
            if (incCall == 5)
            {
                Debug.Log("Kenapa sih ?");
                incCall = 0;
            }
    }
}
