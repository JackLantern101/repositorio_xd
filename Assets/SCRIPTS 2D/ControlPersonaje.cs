using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPersonaje : MonoBehaviour
{


    //Funciones pueden ser (public - publicas)  (private - privadas)
    //Float es la manera de determinar el tipo de contendio que estara en la funcion, float corresponde a un valor numerico
    //Las variables, complende dos parte (1 el tipo) (2 el nombre o la definicion)
    //Estas se utilizadn para mas adelante en progracion atraves de definicion o nobre darle caracteristicas a los componentes


    public float velocidadPersonaje;
    public float impulsoSaltos;
    public float numeroSaltos;
    public LayerMask layerPisoEscenario;



    // hay diferentes componentes como Rigidbody2D LayerMask BoxCollider2D - que ya unity tiene dentro de sus scripts y lo que hacemos es decirle que busqe dentro del objeto ese elemento especificamente 

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool personajeMiraDerecha = true;
    private float saltosRestantes;
    private Animator animator;
    private bool tocaSuelo = true;
    private bool PersonajeVivo = true;

    // la funcion start, le dice al programa que recien se ejecute el onjero con el script, busque esos componentes y los ejecute
    // para este caso esta buscando el Rigidbody2D, BoxCollider2D,  que son compomentes que solumamnente necesita encontrar una unica vez

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = numeroSaltos;
        animator = GetComponent<Animator>();
    }

    // la funcion Update se esta ejecutando 1vez cada frame de juego, y le permite al programa en cada frame, hacer modificaciones
    // en este cado necesitamos que el prorma esta en cada frame haciendo cambios a la posicion del personaje por eso se ejecuta en Update

    void Update()
    {

        if (PersonajeVivo==true)
        {
            PersonajeMoviendose();
            PersonajeSalta();
            PersonajeAtaca();
            PersonajeAtaca2();
            PersonajeAtaca3();
            if (Input.GetKey(KeyCode.Y))
            {
            PersonajeMuere();
            }

        }

    }

    //esta es otro tipo de funcion, que permite hacer compraciones 

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, layerPisoEscenario);
        if (raycastHit.collider != null)
        {
            tocaSuelo = true;
        }
        else
        {
            tocaSuelo = false;
        }
        return raycastHit.collider != null;


    }

    // esta funcion es creada para calcular, el salto de nuestro persoanje, y es llamada en Update para que se ejecute en cada frame
    void PersonajeSalta()
    {
        
        if (EstaEnSuelo())
        {
            saltosRestantes = numeroSaltos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(Vector2.up * impulsoSaltos, ForceMode2D.Impulse);
        }

        if (tocaSuelo==true)
        {
            animator.SetBool("Saltar", false);
        }
        else
        {
            animator.SetBool("Saltar", true);
        }


    }

    // sustituri animaciones y techa de input

    void PersonajeAtaca()
    {
       
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("Atacar", true);
        }

        else
        {
            animator.SetBool("Atacar", false);
        }

    }

    void PersonajeAtaca2()
    {

        if (Input.GetKey(KeyCode.R))
        {
            animator.SetBool("Atacar2", true);
        }

        else
        {
            animator.SetBool("Atacar2", false);
        }

    }
    void PersonajeAtaca3()
    {

        if (Input.GetKey(KeyCode.T))
        {
            animator.SetBool("Atacar3", true);
        }

        else
        {
            animator.SetBool("Atacar3", false);
        }

    }

    public void PersonajeMuere()
    {
        rigidBody.velocity = new Vector2(0,0);
        animator.SetBool("Morir", true);
         PersonajeVivo = false;
         

    }


    // esta funcion es creada para cambiar la posicion en x del personaje

    void PersonajeMoviendose()
    {
        // Lógica de movimiento 
        float inputMovimiento = Input.GetAxis("Horizontal");

        if (inputMovimiento != 0f)
        {
            if (tocaSuelo == true)
            {
                animator.SetBool("Correr", true);
            }
        }
        else
        {
            animator.SetBool("Correr", false);
        }




        rigidBody.velocity = new Vector2(inputMovimiento * velocidadPersonaje, rigidBody.velocity.y);

        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        // Si se cumple condición
        if ((personajeMiraDerecha == true && inputMovimiento < 0) || (personajeMiraDerecha == false && inputMovimiento > 0))
        {
            // Ejecutar código de volteado
            personajeMiraDerecha = !personajeMiraDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
