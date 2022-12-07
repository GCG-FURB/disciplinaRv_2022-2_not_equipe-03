using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using ZXing;
using static UnityEngine.Networking.UnityWebRequest;
using Random = System.Random;
using Random1 = UnityEngine.Random;
using Result = ZXing.Result;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField] private RawImage rawImageBackground;
    [SerializeField] private AspectRatioFitter aspectRatioFitter;
    [SerializeField] private TextMeshProUGUI textOut;
    [SerializeField] private RectTransform scanZone;
    [SerializeField] TextMeshProUGUI palavra;
    [SerializeField] TextMeshProUGUI muitoBemTexto;
    [SerializeField] TextMeshProUGUI letrasNaoContem;
    [SerializeField] GameObject fimFase;
    [SerializeField] TextMeshProUGUI palavrasEncontradasTexto;
    [SerializeField] AudioSource letraA;
    [SerializeField] AudioSource letraB;
    [SerializeField] AudioSource letraC;
    [SerializeField] AudioSource letraD;
    [SerializeField] AudioSource letraE;
    [SerializeField] AudioSource letraF;
    [SerializeField] AudioSource letraG;
    [SerializeField] AudioSource letraH;
    [SerializeField] AudioSource letraI;
    [SerializeField] AudioSource letraJ;
    [SerializeField] AudioSource letraK;
    [SerializeField] AudioSource letraL;
    [SerializeField] AudioSource letraM;
    [SerializeField] AudioSource letraN;
    [SerializeField] AudioSource letraO;
    [SerializeField] AudioSource letraP;
    [SerializeField] AudioSource letraQ;
    [SerializeField] AudioSource letraR;
    [SerializeField] AudioSource letraS;
    [SerializeField] AudioSource letraT;
    [SerializeField] AudioSource letraU;
    [SerializeField] AudioSource letraV;
    [SerializeField] AudioSource letraW;
    [SerializeField] AudioSource letraX;
    [SerializeField] AudioSource letraY;
    [SerializeField] AudioSource letraZ;
    [SerializeField] AudioSource palavraMAR;
    [SerializeField] AudioSource palavraVASO;
    [SerializeField] AudioSource palavraBOLA;
    [SerializeField] AudioSource palavraLUA;
    [SerializeField] AudioSource palavraRATO;
    [SerializeField] AudioSource palavraAMOR;
    [SerializeField] AudioSource palavraTOCA;
    [SerializeField] AudioSource palavraVOAR;
    [SerializeField] AudioSource palavraFLOR;
    [SerializeField] AudioSource palavraDADO;
    [SerializeField] AudioSource palavraCARRO;
    [SerializeField] AudioSource palavraFLAUTA;
    [SerializeField] AudioSource palavraPAPEL;
    [SerializeField] AudioSource palavraPLANTA;
    [SerializeField] AudioSource palavraVELAS;
    private string palavraString;
    private string palavraInteiraVerifica;
    private string levelDoJogo;
    private int quantidadeQRCode;
    private static int acertos;
    private string[] arrayLetras;
    private static int fases;
    private List<string> listaPalavrasNivel1 = new List<string> { "MAR", "VASO", "BOLA", "LUA", "RATO" };
    private List<string> listaPalavrasNivel2 = new List<string> { "AMOR", "TOCA", "VOAR", "FLOR", "DADO" };
    private List<string> listaPalavrasNivel3 = new List<string> { "CARRO", "FLAUTA", "PAPEL", "PLANTA", "VELAS" };
    private List<string> palavrasEncontradas = new List<string> (10);
    private List<string> letrasHistorico = new List<string>(50);

    private bool isCamAvaible;
    private WebCamTexture cameraTexture;
    private RectTransform rectTransform;
    private Vector3 posicaoInicial;

    public void JogarNovamente()
    {
        acertos = 0;
        palavrasEncontradas = new List<string>(10);
        palavrasEncontradas = new List<string>(50);
        fases = 0;
        MudarTexto();
        fimFase.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (palavra.text == "")
        {
            MudarTexto();
        }
        SetUpCamera();
        posicaoInicial = textOut.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
        Debug.Log("Posição x: "+ textOut.transform.position.x);
        if(textOut.transform.position.x > 10)
        {
            OnClickEditTextOut();
        } else if (textOut.rectTransform.position.x < -10)
        {
            textOut.text = "";
        }

    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            isCamAvaible = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                cameraTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
            }
        }

        cameraTexture.Play();
        rawImageBackground.texture = cameraTexture;
        isCamAvaible = true;
    }

    private void UpdateCameraRender()
    {
        if (isCamAvaible == false)
        {
            return;
        }
        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        int orientation = -cameraTexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void OnClickScan()
    {
        Scan();
    }

    public void OnClickEditTextOut()
    {
        if (palavraString.Contains(textOut.text) && !letrasHistorico.Contains(textOut.text))
        { 
            string palavraFinal = "";
            string palavraNova = "";
            char[] characters = palavra.text.ToCharArray();
            char[] charactersTodas = palavraInteiraVerifica.ToCharArray();
            
            for (int i = 0; i < charactersTodas.Length; i++)
            {
                if (charactersTodas[i].ToString() == textOut.text)
                {
                    palavraNova += "<color=green>" + charactersTodas[i].ToString() + "</color>";
                    palavraFinal += "<color=green>"+ charactersTodas[i].ToString() + "</color>";
                    acertos++;
                }
                else{
                    palavraNova += charactersTodas[i].ToString();
                    palavraFinal += characters[i].ToString();
                }
            }
            palavraInteiraVerifica = palavraNova;
            palavra.text = palavraFinal;
        }
        else if (!letrasHistorico.Contains(textOut.text))
        {
            letrasNaoContem.text = letrasNaoContem.text + textOut.text + " ";
        }

        letrasHistorico.Add(textOut.text);

        if (acertos == palavraString.Length)
        {
            palavrasEncontradas.Add(palavraString);
            StartCoroutine(Mensagem()); 
        }
        else
        {
            textOut.text = "";
        }

    }

    private void AudioPalavra()
    {
        if(palavraString == "MAR")
        {
            palavraMAR.Play();
        } else if (palavraString == "VASO")
        {
            palavraVASO.Play();
        }
        else if (palavraString == "BOLA")
        {
            palavraBOLA.Play();
        }
        else if (palavraString == "LUA")
        {
            palavraLUA.Play();
        }
        else if (palavraString == "RATO")
        {
            palavraRATO.Play();
        }
        else if (palavraString == "AMOR")
        {
            palavraAMOR.Play();
        }
        else if (palavraString == "TOCA")
        {
            palavraTOCA.Play();
        }
        else if (palavraString == "VOAR")
        {
            palavraVOAR.Play();
        }
        else if (palavraString == "FLOR")
        {
            palavraFLOR.Play();
        }
        else if (palavraString == "DADO")
        {
            palavraDADO.Play();
        }
        else if (palavraString == "CARRO")
        {
            palavraCARRO.Play();
        }
        else if (palavraString == "FLAUTA")
        {
            palavraFLAUTA.Play();
        }
        else if (palavraString == "PAPEL")
        {
            palavraPAPEL.Play();
        }
        else if (palavraString == "PLANTA")
        {
            palavraPLANTA.Play();
        }
        else if (palavraString == "VELAS")
        {
            palavraVELAS.Play();
        }
    }

    IEnumerator Mensagem()
    {
        fases += 1;
        textOut.rectTransform.position = posicaoInicial;
        textOut.text = "";
        muitoBemTexto.rectTransform.position = posicaoInicial;
        muitoBemTexto.text = "<color=green>MUITO BEM!</color>";
        AudioPalavra();
        yield return new WaitForSeconds(3);
        muitoBemTexto.text = "";
        acertos = 0;
        textOut.text = "";
        levelDoJogo = PlayerPrefs.GetString("Nivel");
        MudarTexto();

        if (levelDoJogo == "1")
        {
            if(fases == 2)
            {
                fases = 0;
                fimFase.SetActive(true);
                palavrasEncontradasTexto.text = "Palavras encontradas: " + String.Join(Environment.NewLine, palavrasEncontradas);
            }
        } else if (levelDoJogo == "2")
        {
            if (fases == 3)
            {
                fases = 0;
                fimFase.SetActive(true);
                palavrasEncontradasTexto.text = "Palavras encontradas: " + String.Join(Environment.NewLine, palavrasEncontradas);
            }
        }
        else if (levelDoJogo == "3")
        {
            if (fases == 4)
            {
                fases = 0;
                fimFase.SetActive(true);
                palavrasEncontradasTexto.text = "Palavras encontradas: " + String.Join(Environment.NewLine, palavrasEncontradas);
            }
        }
    }

    private void SelecionarLetra(int numero)
    {
        textOut.rectTransform.position = posicaoInicial;
        textOut.text = arrayLetras[numero-1];
        if(textOut.text == "A")
        {
            letraA.Play();
        } else if (textOut.text == "B")
        {
            letraB.Play();
        }
        else if (textOut.text == "C")
        {
            letraC.Play();
        }
        else if (textOut.text == "D")
        {
            letraD.Play();
        }
        else if (textOut.text == "E")
        {
            letraE.Play();
        }
        else if (textOut.text == "F")
        {
            letraF.Play();
        }
        else if (textOut.text == "G")
        {
            letraG.Play();
        }
        else if (textOut.text == "H")
        {
            letraH.Play();
        }
        else if (textOut.text == "I")
        {
            letraI.Play();
        }
        else if (textOut.text == "J")
        {
            letraJ.Play();
        }
        else if (textOut.text == "K")
        {
            letraK.Play();
        }
        else if (textOut.text == "L")
        {
            letraL.Play();
        }
        else if (textOut.text == "M")
        {
            letraM.Play();
        }
        else if (textOut.text == "N")
        {
            letraN.Play();
        }
        else if (textOut.text == "O")
        {
            letraO.Play();
        }
        else if (textOut.text == "P")
        {
            letraP.Play();
        }
        else if (textOut.text == "Q")
        {
            letraQ.Play();
        }
        else if (textOut.text == "R")
        {
            letraR.Play();
        }
        else if (textOut.text == "S")
        {
            letraS.Play();
        }
        else if (textOut.text == "T")
        {
            letraT.Play();
        }
        else if (textOut.text == "U")
        {
            letraU.Play();
        }
        else if (textOut.text == "V")
        {
            letraV.Play();
        }
        else if (textOut.text == "W")
        {
            letraW.Play();
        }
        else if (textOut.text == "X")
        {
            letraX.Play();
        }
        else if (textOut.text == "Y")
        {
            letraY.Play();
        }
        else if (textOut.text == "Z")
        {
            letraZ.Play();
        }
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);
            if(result.Text == "401")
            {
                SelecionarLetra(1);
            } else if (result.Text == "402")
            {
                SelecionarLetra(2);
            } else if (result.Text == "403")
            {
                SelecionarLetra(3);
            } else if (result.Text == "404")
            {
                SelecionarLetra(4);
            } else if (result.Text == "405")
            {
                SelecionarLetra(5);
            }  else if (result.Text == "406")
            {
                SelecionarLetra(6);
            } else if (result.Text == "407")
            {
                SelecionarLetra(7);
            }
            else if (result.Text == "408")
            {
                SelecionarLetra(8);
            } else if (result.Text == "409")
            {
                SelecionarLetra(9);
            }
            else if (result.Text == "410")
            {
                SelecionarLetra(10);
            } else if (result.Text == "411")
            {
                SelecionarLetra(11);
            } else if (result.Text == "412")
            {
                SelecionarLetra(12);
            } else if (result.Text == "413")
            {
                SelecionarLetra(13);
            } else if (result.Text == "414")
            {
                SelecionarLetra(14);
            } else if (result.Text == "415")
            {
                SelecionarLetra(15);
            } else if (result.Text == "416")
            {
                SelecionarLetra(16);
            } else if (result.Text == "417")
            {
                SelecionarLetra(17);
            } else if (result.Text == "418")
            {
                SelecionarLetra(18);
            } else if (result.Text == "419")
            {
                SelecionarLetra(19);
            } else if (result.Text == "420")
            {
                SelecionarLetra(20);
            }
            else
            {
                Debug.Log("Não achou numero");
            }
        }
        catch
        {
            Debug.Log("Falha ao ler");
        }
    }

    private void CriarArrayRandomizado(int quantidade, string palavra)
    {
        //Variaveis 
        char[] letrasPalavra = palavra.ToCharArray();
        arrayLetras = new string[quantidade];
        int contador = 0;

        //Adicionar letras da palavra ao array
        foreach(char i in letrasPalavra)
        {
            arrayLetras[contador] = Char.ToString(i);
            contador++;
        }

        //Adicionar letras aleatórias ao array
        for(int i = 0; i < quantidade; i++)
        {
            if (arrayLetras[i] == null)
            {
                arrayLetras[i] = alfanumericoAleatorio(1);
            }
        }

        //Embaralhar array
        Random random = new Random();
        arrayLetras = arrayLetras.OrderBy(x => random.Next()).ToArray();
    }

    public string alfanumericoAleatorio(int tamanho)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var random = new Random();
        var result = new string(
            Enumerable.Repeat(chars, tamanho)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());
        return result;
    }

    public void MudarTexto()
    {
        levelDoJogo = PlayerPrefs.GetString("Nivel");
        int.TryParse(PlayerPrefs.GetString("Quantidade"), out quantidadeQRCode);

        bool palavraIgual = true;
        string palavraRandom = "";
        letrasHistorico = new List<string>(50);
        if (levelDoJogo == "1")
        {
            while (palavraIgual)
            {
                int random = Random1.Range(0, listaPalavrasNivel1.Count);
                palavraRandom = listaPalavrasNivel1[random];
                if (palavraRandom != palavraString && !palavrasEncontradas.Contains(palavraRandom))
                {
                    palavraIgual = false;
                }
            }
            CriarArrayRandomizado(quantidadeQRCode, palavraRandom);
            palavra.text = palavraRandom;
            palavraString = palavraRandom;
            palavraInteiraVerifica = palavraRandom;
            letrasNaoContem.text = "";
        }
        if (levelDoJogo == "2")
        {
            while (palavraIgual)
            {
                int random = Random1.Range(0, listaPalavrasNivel2.Count);
                palavraRandom = listaPalavrasNivel2[random];
                if (palavraRandom != palavraString && !palavrasEncontradas.Contains(palavraRandom))
                {
                    palavraIgual = false;
                }
            }
            CriarArrayRandomizado(quantidadeQRCode, palavraRandom);
            char[] letras = palavraRandom.ToArray();
            int primLetra = Random1.Range(0, palavraRandom.Length-1);
            int segLetra = Random1.Range(0, palavraRandom.Length - 1);
            letras[primLetra] = '_';
            letras[segLetra] = '_';

            palavra.text = new string(letras);
            palavraString = palavraRandom;
            palavraInteiraVerifica = palavraRandom;
            letrasNaoContem.text = "";
        }
        if (levelDoJogo == "3")
        {
            while (palavraIgual)
            {
                int random = Random1.Range(0, listaPalavrasNivel3.Count);
                palavraRandom = listaPalavrasNivel3[random];
                if (palavraRandom != palavraString && !palavrasEncontradas.Contains(palavraRandom))
                {
                    palavraIgual = false;
                }
            }
            CriarArrayRandomizado(quantidadeQRCode, palavraRandom);
            char[] letras = palavraRandom.ToArray();
            for(int i = 0; i < palavraRandom.Length; i++)
            {
                letras[i] = '_';
            }
            palavraString = palavraRandom;
            palavra.text = new string(letras);
            palavraInteiraVerifica = palavraRandom;
            letrasNaoContem.text = "";
        }
    }
}
