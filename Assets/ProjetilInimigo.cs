using System;
using UnityEngine;

public class ProjetilInimigo : MonoBehaviour
{
    private float velocidade = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * velocidade * Time.deltaTime);
    }
    
    void OnBecameInvisible()  
    {  
        Destroy(gameObject);  
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
