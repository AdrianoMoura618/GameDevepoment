using UnityEngine;

public class DestruirForaDaTela : MonoBehaviour
{
    public float distanciaDestruicao = 20f;
    
    void Update()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) > distanciaDestruicao)
        {
            Destroy(gameObject);
        }
    }
}