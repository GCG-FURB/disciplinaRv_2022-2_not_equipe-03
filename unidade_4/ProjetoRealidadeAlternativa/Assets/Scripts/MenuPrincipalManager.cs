using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelNiveis;
    [SerializeField] private TextMeshProUGUI quantidadeQRCode;
    [SerializeField] private Slider volumeSom;
    [SerializeField] 
    public void JogarNivel1()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
        PlayerPrefs.SetString("Nivel", "1");
        PlayerPrefs.SetString("Quantidade", quantidadeQRCode.text);
    }

    public void JogarNivel2()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
        PlayerPrefs.SetString("Nivel", "2");
        PlayerPrefs.SetString("Quantidade", quantidadeQRCode.text);
    }
    public void JogarNivel3()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
        PlayerPrefs.SetString("Nivel", "3");
        PlayerPrefs.SetString("Quantidade", quantidadeQRCode.text);
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

    public void AdicionarQuantidade()
    {
        int quantidade = 0;
        int.TryParse(quantidadeQRCode.text, out quantidade);

        if (quantidade != 20)
        {
            quantidadeQRCode.text = "" + (quantidade + 1);
        }
    }

    public void DiminuirQuantidade()
    {
        int quantidade = 0;
        int.TryParse(quantidadeQRCode.text, out quantidade);

        if (quantidade != 7)
        {
            quantidadeQRCode.text = "" + (quantidade - 1);
        }
    }
}
