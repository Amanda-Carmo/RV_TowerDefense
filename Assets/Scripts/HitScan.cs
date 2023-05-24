using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HitScan : MonoBehaviour
{
    public bool dummy = false;
    public UnityEvent onTakeDamage;
    public GameObject targetAnimatorObject;
    private Animator targetAnimator;
    private bool targetEnabled = true;


    void Awake() {
        targetAnimator = targetAnimatorObject.GetComponent<Animator>();
    }

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
        if (dummy) {
            onTakeDamage.Invoke();
        } else {
            if ( targetEnabled )
            {
                targetEnabled = false;
                onTakeDamage.Invoke();
                targetAnimator.SetTrigger("Hit");
                Invoke("reEnable", 1.0f);
            }
        }
    }

    private void reEnable(){
        targetEnabled = true;
    }
}




