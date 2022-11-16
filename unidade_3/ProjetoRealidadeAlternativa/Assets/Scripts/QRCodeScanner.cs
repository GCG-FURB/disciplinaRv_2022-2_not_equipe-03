using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField] private RawImage rawImageBackground;
    [SerializeField] private AspectRatioFitter aspectRatioFitter;
    [SerializeField] private TextMeshProUGUI textOut;
    [SerializeField] private RectTransform scanZone;
    [SerializeField] TextMeshProUGUI palavra;
    [SerializeField] TextMeshProUGUI letrasNaoContem;

    private bool isCamAvaible;
    private WebCamTexture cameraTexture;

    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
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
        if (palavra.text.Contains(textOut.text))
        {
            string palavraFinal = "";
            char[] characters = palavra.text.ToCharArray();
            for(int i = 0; i < characters.Length; i++)
            {
                if (characters[i].ToString() == textOut.text)
                {
                    palavraFinal += "<color=green>"+ characters[i].ToString() + "</color>";
                }
                else{
                    palavraFinal += characters[i].ToString();
                }
            }
            palavra.text = palavraFinal;
        }
        else
        {
            letrasNaoContem.text = letrasNaoContem.text + textOut.text + " ";
        }
        textOut.text = "";
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);
            if (result != null)
            {
                textOut.text = result.Text;
            }
            else
            {
                textOut.text = "FAILED TO READ QR_CODE";
            }
        }
        catch
        {
            textOut.text = "FAILED IN TRY";
        }
    }
}
