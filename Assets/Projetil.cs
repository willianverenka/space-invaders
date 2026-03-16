using System;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;
    void Start()
    {
        ControladorJogo.Controlador.NavePodeDisparar = false;
    }

    void Update()
    {
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);
    }
    
    void OnBecameInvisible()
    {
        ControladorJogo.Controlador.NavePodeDisparar = true;
        Destroy(gameObject);  
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Inimigo"))
            Destroy(gameObject);
    }
}
