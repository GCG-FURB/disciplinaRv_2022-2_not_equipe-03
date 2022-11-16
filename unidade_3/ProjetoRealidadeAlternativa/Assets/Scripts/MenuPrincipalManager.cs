using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelNiveis;
    public void JogarNivel1()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
        PlayerPrefs.SetString("Nivel", "1");
    }

    public void JogarNivel2()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
        PlayerPrefs.SetString("Nivel", "2");
    }
    public void JogarNivel3()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
        PlayerPrefs.SetString("Nivel", "3");
    }

    public void AbrirNiveis()
    {
        painelMenuInicial.SetActive(false);
        painelNiveis.SetActive(true);
    }

    public void FecharNiveis()
    {
        painelMenuInicial.SetActive(true);
        painelNiveis.SetActive(false);
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelMenuInicial.SetActive(true);
        painelOpcoes.SetActive(false);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
