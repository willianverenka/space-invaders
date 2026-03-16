using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDerrota : MonoBehaviour
{
    public TextMeshProUGUI textoPontuacao;
    
    void Start()
    {
        textoPontuacao.text = $"Pontuação: {ControladorJogo.Controlador.pontuacao}";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
