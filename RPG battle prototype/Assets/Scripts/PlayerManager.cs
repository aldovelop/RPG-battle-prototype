using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public int damage;
    public int defense;
    public float critMultiplier;
    public float damageDelay;
    public string currentStyle;

    float damageDealt;

    int hitChance;
    int missChance;

    public PlayerHealth healthBar;

    public EnemyManager enemyManager;

    public OutfitSelect outfitSelect;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        if (outfitSelect.CalculateStyle() == 0)
        {
            currentStyle = "CUTE";
        }
        else if (outfitSelect.CalculateStyle() == 1)
        {
            currentStyle = "CASUAL";
        }
        else if (outfitSelect.CalculateStyle() == 2)
        {
            currentStyle = "EDGY";
        }

        // Calculating style effectiveness:
        // if(currentStyle == "CUTE" && enemyManager.currentStyle == )

        StartCoroutine(DamageProcess());
    }

    private void Update()
    {
        if (currentHealth <= 0f)
        {
            GameManager.Instance.YouWin = false;
            GameManager.Instance.BattleOver = true;
        }
    }

    // Enemy attacks:
    IEnumerator DamageProcess()
    {
        while (true)
        {
            hitChance = Random.Range(1, 10);
            missChance = Random.Range(1, 10) - 2;

            if (hitChance > missChance)
            {
                damageDealt = enemyManager.damage * 100 / (100 + defense);
            }
            else if (hitChance == missChance)
            {
                damageDealt = enemyManager.damage * critMultiplier;
            }
            else
            {
                damageDealt = 0f;
            }

            currentHealth -= damageDealt;
            healthBar.SetHealth(currentHealth);

            Debug.Log("Ouch");

            yield return new WaitForSeconds(enemyManager.damageDelay);
        }
    }
}