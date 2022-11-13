using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DestroyAfterAnimation : MonoBehaviour
{
    [SerializeField] public Animator animatorRef;
    public float delay = 0f;
 
     void Start () {
        Debug.Log("Effect Spawned!");
         Destroy (gameObject, animatorRef.GetCurrentAnimatorStateInfo(0).length + delay); 
     }

}
