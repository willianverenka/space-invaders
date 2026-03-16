using System;
using UnityEngine;

public class Invasao : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Inimigo"))
        {
            ControladorJogo.Controlador.ReduzirVida();
            ControladorJogo.Controlador.RemoverInimigoDaGrid(other.gameObject);  
            Destroy(other.gameObject);
        }
    }
}
