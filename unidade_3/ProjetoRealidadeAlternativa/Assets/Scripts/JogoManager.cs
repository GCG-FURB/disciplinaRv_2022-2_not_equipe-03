using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JogoManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string nomeDoLevelDeJogo;
   
    public void Voltar()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }
}
