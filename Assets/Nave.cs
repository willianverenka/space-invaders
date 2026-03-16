using System;
using UnityEngine;

public class Nave : MonoBehaviour
{
    public float velocidade = 10f;  
    public float limiteHorizontalTela = 8.5f; 
    public GameObject prefabProjetil;    
    public Transform localOrigemDisparo;

    private short vezesAtingido = 0;
    
    private Rigidbody2D rb;
    void Start()
    {
        
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");  
  
        Vector3 direcao = new Vector3(horizontalInput, 0, 0);  
        transform.Translate(direcao * (velocidade * Time.deltaTime));  
  
        Vector3 currentPos = transform.position;  
        currentPos.x = Mathf.Clamp(currentPos.x, -limiteHorizontalTela, limiteHorizontalTela);  
        transform.position = currentPos;
        
        if (Input.GetKey(KeyCode.Space) && ControladorJogo.Controlador.NavePodeDisparar)
        {
            Disparar();  
        }
    }
    
    void Disparar()  
    {  
        Instantiate(prefabProjetil, localOrigemDisparo.position, localOrigemDisparo.rotation);  
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ProjetilInimigo"))
        {
            ControladorJogo.Controlador.ReduzirVida();
        }
    }
}
