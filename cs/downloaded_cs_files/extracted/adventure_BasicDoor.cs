// Universidad Estatal a Distancia
// Introducción a Unity
// Autor: Lic. Juan Pablo Navarro Fennell

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoor : MonoBehaviour
{
    // Miembros de clase pública
    public GameObject puerta;
    public Color colorRequerido;
    public float distanciaMover = 3f;

    // Miembros de clase protegidos
    protected Renderer puertaRender;
    protected bool abierto;

    // Método que se ejecuta cuando aparece este objeto en pantalla
    void Start()
    {
        puertaRender = puerta.GetComponent<Renderer>();
        puertaRender.material.color = colorRequerido;
    }

    // Método que se ejecuta cuando este objeto interseca otro objeto en el juego.
    private void OnTriggerEnter(Collider other)
    {
        // Consultamos si el objeto que interseca tiene la etiqueta del jugador.
        if (!abierto && other.CompareTag("Player"))
        {

            if(GameManager.instance.GetColor() == colorRequerido) 
            {
                puerta.transform.Translate(Vector3.down * distanciaMover);
                abierto = true;
            }
        }
    }
}   
