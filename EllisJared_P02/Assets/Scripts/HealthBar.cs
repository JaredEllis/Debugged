using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{ 
    public GameObject healthBar;

    public void SetHP(float normalizedValue)
    {
        healthBar.transform.localScale = new Vector3(normalizedValue, 1, 1);
    }
    
}
