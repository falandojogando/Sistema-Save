using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portao : MonoBehaviour
{
    public Transform portaoObj;
    public float alturaAtual ,alturaNormal, alturaLevando;
    public float velocidade;
    public bool estaLevantado;
    void Start()
    {
        alturaAtual = alturaNormal;
    }
    void Update()
    {
        if (estaLevantado)
        {
            if (alturaAtual - 0.1f < portaoObj.localPosition.y)
            {
                alturaAtual = Mathf.Lerp(alturaAtual, alturaLevando, Time.deltaTime * velocidade);
                portaoObj.localPosition = new Vector3(portaoObj.localPosition.x, alturaAtual, portaoObj.localPosition.z);
            }
        }
    }

    public void AbrirPortao()
    {
        Invoke("EstaLevantandoTRUE", 0.5f); // DELAY PARA LEVANTAR O PORTÃO
    }

    void EstaLevantandoTRUE()
    {
        estaLevantado = true;
        SaveController.instance.SalvarPortao(this.gameObject); //SALVA O PORTAO 
    }
}
