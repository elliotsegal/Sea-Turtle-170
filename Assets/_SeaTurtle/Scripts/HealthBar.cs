using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image currentHealthBar;

    private float healthPoint = 100;
    private float maxHealthPoint = 100;

    // Start is called before the first frame update
    private void Start()
    {
        UpdateHealthBar();
    }

    // Update is called once per frame
    private void UpdateHealthBar()
    {
        float ratio = healthPoint/maxHealthPoint;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    public void TakeDamage(float damage)
    {
    	healthPoint -= damage;
    	if (healthPoint < 0)
    	{
    		healthPoint = 0;
    		Debug.Log("Dead!");
    	}
    	UpdateHealthBar();
    }
    
    public void HealDamage(float damage)
    {
    	healthPoint += damage;
    	// if (healthPoint > maxHealthPoint)
    	// 	healthPoint = maxHealthPoint;
    	UpdateHealthBar();    	
    }
}
