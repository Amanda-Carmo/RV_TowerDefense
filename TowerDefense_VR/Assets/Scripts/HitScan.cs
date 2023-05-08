using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HitScan : MonoBehaviour
{
    public UnityEvent onTakeDamage;

    private bool targetEnabled = true;

    //-------------------------------------------------
    private void ApplyDamage()
    {
        OnDamageTaken();
    }


    //-------------------------------------------------
    private void FireExposure()
    {
        OnDamageTaken();
    }


    //-------------------------------------------------
    private void OnDamageTaken()
    {
        if ( targetEnabled )
        {
            Debug.Log("Hit!");
            onTakeDamage.Invoke();
            GetComponent<Animator>().SetTrigger("Hit");
            // StartCoroutine( this.animateHit() );
        }
    }
}




