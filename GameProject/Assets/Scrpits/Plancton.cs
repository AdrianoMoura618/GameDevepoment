using UnityEngine;

public class Plancton : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D outro)
    {
        Debug.Log("Plâncton colidiu com: " + outro.gameObject.name + " (Tag: " + outro.tag + ")");
        
        if (outro.CompareTag("Player"))
        {
            Debug.Log("✅ Plâncton detectou o Player!");
            
            if (GameManager.instance != null)
            {
                GameManager.instance.PerderVida();
                Debug.Log("❌ Player perdeu 1 vida por causa do Plâncton!");
            }
            
            Destroy(gameObject);
            Debug.Log("✅ Plâncton destruído!");
        }
    }
}