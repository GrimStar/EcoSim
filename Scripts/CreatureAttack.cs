using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAttack : MonoBehaviour, IAttack
{
    bool isAttacking = false;
    
    public void AttackTarget(Transform target)
    {
        IHaveStrength _myStats = GetComponent<IHaveStrength>();
        if(_myStats != null) {
            if (!isAttacking)
            {
                
                isAttacking = true;
                StartCoroutine(AttackCooldown());

                IHaveHealth _stats = target.GetComponent<IHaveHealth>();
                if (_stats != null)
                {
                    if (_stats.Health > 0)
                    {
                        //Do damage
                        IDamagable _takeDamage = target.GetComponent<IDamagable>();
                        if (_takeDamage != null)
                        {
                            _takeDamage.ITakeDamage(_myStats.Strength, _stats);
                        }
                    }
                }
            }                          
        }
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }
}
