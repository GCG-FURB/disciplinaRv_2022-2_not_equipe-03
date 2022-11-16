using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Palavra : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mudarPalavra;
    [SerializeField] TextMeshProUGUI letrasNaoFazemParte;
    private string levelDoJogo;
    private List<string> listaPalavrasNivel1 = new List<string> {"MAR", "VASO", "BOLA", "LUA", "RATO"};
    private List<string> listaPalavrasNivel2 = new List<string> { "AMOR", "TOCA", "VOAR", "FLOR", "DADO" };
    private List<string> listaPalavrasNivel3 = new List<string> { "CARRO", "FLAUTA", "PAPEL", "PLANTA", "VELAS" };


    // Start is called before the first frame update
    void Start()
    {
        if(mudarPalavra.text == "")
        {
            MudarTexto();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setNivel(string nivel)
    {
        levelDoJogo = nivel;
    }

    public void MudarTexto()
    {
        levelDoJogo = PlayerPrefs.GetString("Nivel");
        if (levelDoJogo == "1")
        {
            int random = Random.Range(0, listaPalavrasNivel1.Count);
            mudarPalavra.text = listaPalavrasNivel1[random];
            letrasNaoFazemParte.text = "";
        }
        if (levelDoJogo == "2")
        {
            int random = Random.Range(0, listaPalavrasNivel2.Count);
            mudarPalavra.text = listaPalavrasNivel2[random];
            letrasNaoFazemParte.text = "";
        }
        if (levelDoJogo == "3")
        {
            int random = Random.Range(0, listaPalavrasNivel3.Count);
            mudarPalavra.text = listaPalavrasNivel3[random];
            letrasNaoFazemParte.text = "";
        }
    }

}
