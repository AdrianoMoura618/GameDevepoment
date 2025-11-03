using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidadeAndar = 5f;
    public float forcaPulo = 12f;
    
    [Header("Detecção de Chão")]
    public Transform pontoChecagemChao;
    public float raioChecagem = 0.2f;
    public LayerMask layerChao;
    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool estaNoChao;
    private float movimentoHorizontal;
    private bool estaParalisado = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (estaParalisado) return;
        
        movimentoHorizontal = 0;
        
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            movimentoHorizontal = -1;
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            movimentoHorizontal = 1;
        }
        
        if (movimentoHorizontal > 0)
            sprite.flipX = false;
        else if (movimentoHorizontal < 0)
            sprite.flipX = true;
        
        if ((Keyboard.current.spaceKey.wasPressedThisFrame || 
             Keyboard.current.wKey.wasPressedThisFrame || 
             Keyboard.current.upArrowKey.wasPressedThisFrame) && estaNoChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        }
    }
    
    void FixedUpdate()
    {
        if (estaParalisado)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }
        
        estaNoChao = Physics2D.OverlapCircle(pontoChecagemChao.position, raioChecagem, layerChao);
        
        rb.linearVelocity = new Vector2(movimentoHorizontal * velocidadeAndar, rb.linearVelocity.y);
    }
    
    public void Paralisar(float duracao)
    {
        StartCoroutine(ParalisarTemporario(duracao));
    }
    
    System.Collections.IEnumerator ParalisarTemporario(float duracao)
    {
        estaParalisado = true;
        sprite.color = Color.cyan;
        
        yield return new WaitForSeconds(duracao);
        
        estaParalisado = false;
        sprite.color = Color.white;
    }
    
    void OnDrawGizmosSelected()
    {
        if (pontoChecagemChao != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pontoChecagemChao.position, raioChecagem);
        }
    }
}