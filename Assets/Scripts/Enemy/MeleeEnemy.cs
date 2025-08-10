using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown && PlayerInSight())
        {
            //Attack
        }
    }

    private bool PlayerInSight()
    {
        
        return false;
    }
}
