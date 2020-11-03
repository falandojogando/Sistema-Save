using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveController : MonoBehaviour
{

    public static SaveController instance;
    public GameObject[] portoesTotais;
    public List<GameObject> portoesSalvos;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        portoesTotais = GameObject.FindGameObjectsWithTag("Portao"); // PORTAO TOTAIS RECEBEM TODOS OS GAMEOBJECTS QUE TEM A TAG PORTAO

        if (PlayerPrefs.HasKey("PORTAOCOUNT"))
        {
            if (PlayerPrefs.GetInt("PORTAOCOUNT") > 0)
            {
                for (int i = 0; i < PlayerPrefs.GetInt("PORTAOCOUNT"); i++)
                {
                    portoesSalvos.Add(portoesTotais[PlayerPrefs.GetInt($"PORTAO{i}")]);
                }
            }
        }
    }

    void Start()
    {
        foreach (var item in portoesSalvos)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, item.GetComponent<Portao>().alturaLevando , item.transform.localPosition.z ) ; //AQUI O PORTAO RECEBE RECEBE A ALTURA NO EIXO Y DE QUANDO ELE É LEVANTADO
        }
    }

    public void SalvarPortao(GameObject g)
    {
        if (!portoesSalvos.Contains(g)) //SE NÃO TIVER NA ARRAY DE PORTOES SALVOS VAI PODER SER ADICIONADA SÓ 1 Vez 
        {
            portoesSalvos.Add(g);
        }
    }

    private void OnApplicationQuit() // FUNÇAO QUE É CHAMADA AO FECHAR O JOGO
    {
        List<int> temp = new List<int>();


        foreach (var item in portoesSalvos)
        {
            for (int i = 0; i < portoesTotais.Length; i++)
            {
                if (portoesTotais[i] == item) // VAI SALVAR OS INDICES DOS PORTOES TOTAIS BASEANDO-SE NOS PORTOES SALVOS
                {
                    temp.Add(i);
                }
            }
        }

        //FAZ UM LAÇO E SALVA TODOS OS ITENS DA LISTA TEMP QUE FOI CRIADA PARA ARMAZENAR OS PORTOES SE BASEANDO PELO PORTOES TOTAIS
        PlayerPrefs.SetInt("PORTAOCOUNT", temp.Count); //SALVA O TAMANHO DA LIST
        for (int i = 0; i < temp.Count; i++)
        {
            PlayerPrefs.SetInt($"PORTAO{i}", temp[i]);
        }
        
    }
}
