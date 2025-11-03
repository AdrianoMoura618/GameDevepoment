using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject aguaVivaPrefab;
    public GameObject garyPrefab;
    public GameObject planctonPrefab;
    
    [Header("Configurações")]
    public bool spawnarGary = false;
    public bool spawnarPlancton = false;
    
    [Header("Intervalos")]
    public float intervaloAguaViva = 1.5f;
    public float intervaloGary = 8f;
    public float intervaloPlancton = 6f;
    
    [Header("Área de Spawn")]
    public float largura = 15f;
    public float alturaMinima = -2f;
    public float alturaMaxima = 3f;
    public Vector2 centroSpawn = new Vector2(0, 0);
    
    private float proximoSpawnAguaViva;
    private float proximoSpawnGary;
    private float proximoSpawnPlancton;
    
    void Start()
    {
        proximoSpawnAguaViva = Time.time + intervaloAguaViva;
        proximoSpawnGary = Time.time + intervaloGary;
        proximoSpawnPlancton = Time.time + intervaloPlancton;
    }
    
    void Update()
    {
        if (GameManager.instance == null || !GameManager.instance.jogoAtivo)
            return;
        
        if (Time.time >= proximoSpawnAguaViva)
        {
            SpawnarAguaViva();
            proximoSpawnAguaViva = Time.time + intervaloAguaViva;
        }
        
        if (spawnarGary && Time.time >= proximoSpawnGary)
        {
            SpawnarGary();
            proximoSpawnGary = Time.time + intervaloGary;
        }
        
        if (spawnarPlancton && Time.time >= proximoSpawnPlancton)
        {
            SpawnarPlancton();
            proximoSpawnPlancton = Time.time + intervaloPlancton;
        }
    }
    
    void SpawnarAguaViva()
    {
        Vector3 pos = GetPosicaoAleatoria();
        Instantiate(aguaVivaPrefab, pos, Quaternion.identity);
    }
    
    void SpawnarGary()
    {
        Vector3 pos = GetPosicaoAleatoria();
        pos.y = alturaMinima; 
        Instantiate(garyPrefab, pos, Quaternion.identity);
    }
    
    void SpawnarPlancton()
    {
        Vector3 pos = GetPosicaoAleatoria();
        pos.y = alturaMinima; 
        Instantiate(planctonPrefab, pos, Quaternion.identity);
    }
    
    Vector3 GetPosicaoAleatoria()
    {
        float x = centroSpawn.x + Random.Range(-largura / 2, largura / 2);
        float y = Random.Range(alturaMinima, alturaMaxima);
        return new Vector3(x, y, 0);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 tamanho = new Vector3(largura, alturaMaxima - alturaMinima, 0);
        Vector3 centro = new Vector3(centroSpawn.x, (alturaMaxima + alturaMinima) / 2, 0);
        Gizmos.DrawWireCube(centro, tamanho);
    }
}