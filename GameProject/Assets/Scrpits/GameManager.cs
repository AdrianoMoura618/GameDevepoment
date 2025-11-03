using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Objetivos")]
    public int aguasVivasNecessarias = 30;
    public float tempoLimite = 30f;
    public int vidasIniciais = 3;
    
    [Header("Estado Atual")]
    public int aguasVivasColetadas = 0;
    public float tempoRestante;
    public int vidas;
    public bool jogoAtivo = false;
    
    [Header("UI")]
    public TextMeshProUGUI txtAguasVivas;
    public TextMeshProUGUI txtTempo;
    public TextMeshProUGUI txtVidas;
    public GameObject painelVitoria;
    public GameObject painelDerrota;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        IniciarJogo();
    }
    
    void Update()
    {
        if (!jogoAtivo) return;
        
        tempoRestante -= Time.deltaTime;
        AtualizarUI();
        
        if (tempoRestante <= 0)
        {
            tempoRestante = 0;
            TempoEsgotado();
        }
    }
    
    public void IniciarJogo()
    {
        aguasVivasColetadas = 0;
        tempoRestante = tempoLimite;
        vidas = vidasIniciais;
        jogoAtivo = true;
        
        if (painelVitoria) painelVitoria.SetActive(false);
        if (painelDerrota) painelDerrota.SetActive(false);
        
        AtualizarUI();
        Debug.Log("Jogo iniciado! Vidas: " + vidas);
    }
    
    public void ColetarAguaViva()
    {
        aguasVivasColetadas++;
        AtualizarUI();
        
        Debug.Log("Águas-vivas: " + aguasVivasColetadas + "/" + aguasVivasNecessarias);
        
        if (aguasVivasColetadas >= aguasVivasNecessarias)
        {
            Vitoria();
        }
    }

    public void PerderVida()
    {
        vidas--;
        Debug.Log("❌ Perdeu 1 vida! Vidas restantes: " + vidas);
        AtualizarUI();
        
        if (vidas <= 0)
        {
            GameOver();
        }
    }
    
    void TempoEsgotado()
    {
        jogoAtivo = false;
        Debug.Log("⏰ Tempo esgotado!");
        
        if (aguasVivasColetadas >= aguasVivasNecessarias)
        {
            Vitoria();
        }
        else
        {
            PerderVida();
            
            if (vidas > 0)
            {
                MostrarTentarNovamente();
            }
        }
    }
    
    void Vitoria()
    {
        jogoAtivo = false;
        Debug.Log("🎉 VITÓRIA! Coletou " + aguasVivasColetadas + " águas-vivas!");
        
        if (painelVitoria)
        {
            painelVitoria.SetActive(true);
        }
    }
    
    void GameOver()
    {
        jogoAtivo = false;
        Debug.Log("💀 GAME OVER! Sem vidas!");
        
        if (painelDerrota)
        {
            painelDerrota.SetActive(true);
        }
    }
    
    void MostrarTentarNovamente()
    {
        Debug.Log("Ainda tem " + vidas + " vidas! Pode tentar novamente!");
        
        if (painelDerrota)
        {
            painelDerrota.SetActive(true);
        }
    }
    
    void AtualizarUI()
    {
        if (txtAguasVivas)
            txtAguasVivas.text = "Águas-Vivas: " + aguasVivasColetadas + " / " + aguasVivasNecessarias;
        
        if (txtTempo)
        {
            int segundos = Mathf.CeilToInt(tempoRestante);
            txtTempo.text = "Tempo: " + segundos + "s";
            
            if (segundos <= 10)
                txtTempo.color = Color.red;
            else
                txtTempo.color = Color.white;
        }
                if (txtVidas)
        {
            txtVidas.text = "Vidas: " + vidas;
            
            if (vidas <= 1)
                txtVidas.color = Color.red;
            else if (vidas <= 2)
                txtVidas.color = Color.yellow;
            else
                txtVidas.color = Color.white;
        }
    }
    
    public void ProximaFase()
    {
        int cenaAtual = SceneManager.GetActiveScene().buildIndex;
        int proximaCena = cenaAtual + 1;
        
        if (proximaCena < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(proximaCena);
        }
        else
        {
            Debug.Log("Última fase completada! Voltando ao menu...");
            VoltarMenu();
        }
    }
    
    public void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void VoltarMenu()
    {
        SceneManager.LoadScene(0); 
    }
}