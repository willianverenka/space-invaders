using UnityEngine;  
  
public class Inimigo : MonoBehaviour  
{  
    private bool morreu = false;  
    private float velocidadeMovimentoHorizontal = .5f;  
    public float velocidadeMovimentoVertical = .1f;  
    private static float direcaoX = 1f;  
    private float limiteHorizontalTela;  
    public GameObject prefabProjetil;      
    public Transform localOrigemDisparo;     
    private float cadenciaDisparo = 1f;        
    private float timerProximoTiro = 0f;  
  
    void Start()  
    {  
        limiteHorizontalTela = Camera.main.orthographicSize * Camera.main.aspect - 0.5f;  
    }  
  
    void Update()  
    {  
        if (morreu) return;    
          
        MoverHorizontalmente();  
        MoverVerticalmente();  
          
        if (Time.time >= timerProximoTiro)  
        {  
            AtivarDisparoAleatorio();   
            timerProximoTiro = Time.time + cadenciaDisparo;  
        }  
    }  
  
    private void MoverVerticalmente()  
    {  
        transform.Translate(Vector3.down * (velocidadeMovimentoVertical * Time.deltaTime));  
        Vector3 currentPos = transform.position;  
  
        transform.position = currentPos;  
    }  
  
    private void MoverHorizontalmente()  
    {  
        transform.Translate(Vector3.right * direcaoX * velocidadeMovimentoHorizontal * Time.deltaTime);    
    
        Vector3 currentPos = transform.position;    
          
        if (currentPos.x >= limiteHorizontalTela)    
        {    
            direcaoX = -1f;    
        }    
        else if (currentPos.x <= -limiteHorizontalTela)    
        {    
            direcaoX = 1f;    
        }    
    
        currentPos.x = Mathf.Clamp(currentPos.x, -limiteHorizontalTela, limiteHorizontalTela);    
        transform.position = currentPos;  
    }  
      
    private void OnCollisionEnter2D(Collision2D other)  
    {  
        if (morreu) return;  
  
        if (!other.gameObject.CompareTag("ProjetilPlayer"))  
            return;  
          
        ControladorJogo.Controlador.AumentarPontuacao();  
        ControladorJogo.Controlador.RemoverInimigoDaGrid(gameObject);  
  
        morreu = true;  
          
        GetComponent<Animator>().SetTrigger("Morte");  
        GetComponent<Collider2D>().enabled = false;  
        Destroy(gameObject, 1f);  
    }  
  
    void AtivarDisparoAleatorio()  
    {  
        int colunaAleatoria = Random.Range(0, ControladorJogo.Controlador.numColunas);    
        GameObject atirador = ControladorJogo.Controlador.ObterInimigoInferior(colunaAleatoria);  
        if (atirador != gameObject) return;  
        Disparar();  
    }  
      
    void Disparar()    
    {    
        Instantiate(prefabProjetil, localOrigemDisparo.position, localOrigemDisparo.rotation);    
    }  
}