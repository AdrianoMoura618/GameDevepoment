using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IniciarJogo()
    {
        SceneManager.LoadScene("Fase1");
    }
    
    public void Sair()
    {
        Application.Quit();
    }
}