using System;  
using TMPro;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
  
public class ControladorJogo : MonoBehaviour  
{  
    private float velocidadeInicial = .05f;  
    private float velocidadeMaxima = .4f;  
    public int pontuacao = 0;  
  
    private int inimigosVivos = 0;  
    private int totalInimigos = 0;  
  
    public GameObject prefabInimigo1;  
    public GameObject prefabInimigo2;  
    public int numColunas = 11;  
    public int numLinhas = 5;  
    private GameObject[,] gridInimigos;  
    public static ControladorJogo Controlador;  
      
    public float margemTopo = 1.5f;      // Reserved space for UI score at top  
    public float offsetDificuldade = 0f; // Increase this each level to push grid down (harder)  
      
    public TextMeshProUGUI textoPontuacao;  
  
    public bool NavePodeDisparar = true;  
  
    public int Vidas = 3;  
      
  
    void Start()  
    {  
    }  
  
    void Update()  
    {  
    }  
  
    private void Awake()  
    {  
        if (Controlador != null && Controlador != this)  
        {  
            Destroy(this);  
        }  
        else  
        {  
            Controlador = this;  
            DontDestroyOnLoad(gameObject);  
        }  
    }     
  
    public void AumentarPontuacao()  
    {  
        pontuacao += 100;  
        AtualizarUI();  
    }  
  
    public void GerarInimigosNaCena()  
    {  
        float distanciaX = 0.47f;  
        float distanciaY = .6f;  
  
        float alturaCamera = Camera.main.orthographicSize;  
        float larguraCamera = alturaCamera * Camera.main.aspect; 
  
        float topoGrid = alturaCamera - margemTopo - offsetDificuldade;  
        float yInicio = topoGrid - (numLinhas - 1) * distanciaY;  
  
        float larguraGrid = (numColunas - 1) * distanciaX;  
        float xInicio = -(larguraGrid / 2f);  
  
        GameObject[] prefabPorLinha = { prefabInimigo1, prefabInimigo1, prefabInimigo2 };  
  
        gridInimigos = new GameObject[numLinhas, numColunas];  
  
        for (int linha = 0; linha < numLinhas; linha++)  
        {  
            for (int coluna = 0; coluna < numColunas; coluna++)  
            {  
                Vector3 spawnPos = new Vector3(  
                    xInicio + coluna * distanciaX,  
                    yInicio + linha * distanciaY,  
                    0  
                );  
  
                GameObject inimigo = Instantiate(prefabPorLinha[linha % prefabPorLinha.Length], spawnPos, Quaternion.identity);  
                gridInimigos[linha, coluna] = inimigo;  
                inimigosVivos++;  
                totalInimigos++;  
            }  
        }  
    }  
  
    public GameObject ObterInimigoInferior(int coluna)    
    {    
        for (int linha = 0; linha < numLinhas; linha++)    
        {    
            GameObject inimigo = gridInimigos[linha, coluna];    
            if (inimigo != null)   
                return inimigo;    
        }    
        return null;   
    }  
      
    public void RemoverInimigoDaGrid(GameObject inimigo)    
    {    
        for (int linha = 0; linha < numLinhas; linha++)    
        {    
            for (int coluna = 0; coluna < numColunas; coluna++)    
            {    
                if (gridInimigos[linha, coluna] == inimigo)    
                {    
                    gridInimigos[linha, coluna] = null;    
                    inimigosVivos--;    
                    
                    if (inimigosVivos <= 0)    
                        PassarDeNivel();  
                      
                    AtualizarVelocidadeInimigos();  
                    return;    
                }    
            }    
        }    
    }  
      
    private void AtualizarVelocidadeInimigos()    
    {    
        float progresso = 1f - ((float)inimigosVivos / totalInimigos);    
        float novaVelocidade = Mathf.Lerp(velocidadeInicial, velocidadeMaxima, progresso);    
    
        Inimigo[] inimigos = FindObjectsByType<Inimigo>(FindObjectsSortMode.None);    
        foreach (Inimigo inimigo in inimigos)    
        {    
            inimigo.velocidadeMovimentoVertical = novaVelocidade;    
        }    
          
        Debug.Log($"Nova velocidade: {novaVelocidade}");  
    }  
  
    private void PassarDeNivel()  
    {  
        LimparCena();  
        GerarInimigosNaCena();  
    }  
  
    public void Derrota()  
    {  
        LimparCena();  
        SceneManager.LoadScene("Derrota");  
        Debug.Log("Perdeu!");  
    }  
  
    private void LimparCena()  
    {  
        totalInimigos = 0;  
        if (!NavePodeDisparar) NavePodeDisparar = true;  
    }  
      
    void AtualizarUI()    
    {    
        textoPontuacao.text = $"Pontuacao: {pontuacao}\nVidas: {ControladorJogo.Controlador.Vidas}";  
    }  
  
    public void ReduzirVida()  
    {  
        if(Vidas - 1 <= 0)  
            Derrota();  
          
        Vidas--;  
        AtualizarUI();
    }  
    
    private void OnEnable()  
    {  
        SceneManager.sceneLoaded += OnSceneLoaded;  
    }  
  
    private void OnDisable()  
    {  
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }  
  
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)    
    {    
        if (scene.name == "SampleScene")  
        {    
            inimigosVivos = 0;    
            totalInimigos = 0;  
            pontuacao = 0;  
            Vidas = 3;  
            textoPontuacao = FindFirstObjectByType<TextMeshProUGUI>();  
            GerarInimigosNaCena();    
        }    
    }

    public void AumentarPontuacaoChefe()
    {
        pontuacao += 500;
        AtualizarUI();
    }
}