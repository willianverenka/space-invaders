using UnityEngine;

public class NaveChefe : MonoBehaviour
{
    public float velocidade = 2f;
    public int pontuacao = 500;

    private float direcao;
    private float limiteX;

    void Start()
    {
        limiteX = Camera.main.orthographicSize * Camera.main.aspect + 1f;

        direcao = Random.value > 0.5f ? 1f : -1f;

        float spawnX = direcao > 0 ? -limiteX : limiteX;
        float spawnY = Camera.main.orthographicSize - 0.5f;
        transform.position = new Vector3(spawnX, spawnY, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.right * direcao * velocidade * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) > limiteX)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("ProjetilPlayer")) return;

        ControladorJogo.Controlador.AumentarPontuacaoChefe();
        Destroy(gameObject);
    }
}