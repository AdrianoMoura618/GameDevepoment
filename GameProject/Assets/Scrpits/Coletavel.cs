using UnityEngine;

public class Coletavel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ColetarAguaViva();
            }
            
            Destroy(gameObject);
        }
    }
}