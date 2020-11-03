using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float vel = 1f;
    Rigidbody rb;

    void Awake()
    {
        if (PlayerPrefs.HasKey("POSX")){ // VERIFICANDO SE EXISTE A KEY POSX E SE EXISTIR ELE CRIE UM NOVO VECTOR3 USANDO AS KEYS SALVAS
            transform.position = new Vector3( PlayerPrefs.GetFloat("POSX") , PlayerPrefs.GetFloat("POSY"), PlayerPrefs.GetFloat("POSZ"));
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Z = Input.GetAxisRaw("Vertical");


        Vector3 v = (transform.forward * Z + transform.right * X).normalized;
        rb.MovePosition(transform.position + v/3);
    }
    private void Update()
    {
        if (transform.position.y < -2f)
        {
            transform.position = new Vector3(2.86f, 1.24f, -2.94f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Portao")
        {
            other.GetComponent<Portao>().AbrirPortao();
        }
    }

    private void OnApplicationQuit() // FUNÇAO QUE É CHAMADA AO FECHAR O JOGO
    {
        // SALVANDO AS POSIÇÃO X, Y, Z EM KEYS COM PLAYERSPREFS
        PlayerPrefs.SetFloat("POSX", transform.position.x);
        PlayerPrefs.SetFloat("POSY", transform.position.y);
        PlayerPrefs.SetFloat("POSZ", transform.position.z);
    }
}
