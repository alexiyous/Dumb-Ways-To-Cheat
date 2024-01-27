using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SusBar : MonoBehaviour
{
    public int maximum;
    public float current;
    public Image mask;
    public float delay = 3;
    public float timer;
    public float interval;
    bool Curang = true;

    
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        button();
        if (Curang == false)
        {
            decrementFill();
        }

        if(Curang == true)    
        {
            incrementFill(1,Curang);
        }
    
        
    }

    void button()
    {
        if(Input.GetKey(KeyCode.C))
            {
                Curang = true;
            }
    }

    void GetCurrentFill()
    {
        float fillAmount = (float) current / (float) maximum;
        mask.fillAmount = fillAmount;
    }

    void incrementFill(float susValue, bool isCheating)
    {
        
        timer += Time.deltaTime;
        
        if (timer <= 2)
        {
            current = (float)current + susValue;
            GetCurrentFill();
            if (current >= 999)
            {
                current = 1000;
            }
            
        }
        else {
            Curang = false;
            timer = 0;
            }
          
    }
        
    

    void decrementFill()
    {
        current = (float)current - 1f;
        GetCurrentFill();
        if(current <= 0)
        {
            current =0;
        }
    }
}