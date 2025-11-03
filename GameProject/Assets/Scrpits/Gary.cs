using UnityEngine;

public class Gary : MonoBehaviour
{
    public float duracaoParalisia = 2f;
    
    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            PlayerController player = outro.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Paralisar(duracaoParalisia);
            }
            
            Destroy(gameObject);
        }
    }
}