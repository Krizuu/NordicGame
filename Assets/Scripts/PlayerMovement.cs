using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body; //Okre�lamy nazw� dla zmiennej przechowuj�cej model fizyczny postaci
    public Animator animator;

    public Joystick joystick;
    public Button button_fight;

    float moveLimiter = 0.7f; //zmienna ograniczaj�ca ruch po ukosie

    public float runSpeed = 5.0f;

    Vector2 movement;

    private bool button_fightPressed;

    // Start is called before the first frame update
    void Start()
    {
        body = GameObject.Find("Player").GetComponent<Rigidbody2D>(); //Pobiera komponent i przechowuje go w wcze�niej przygotowanej zmiennej, dzi�ki temu nie musimy r�cznie przypisywa� komponentu.
        button_fight.onClick.AddListener(OnButtonFightClick);
    }

    // Update is called once per frame
    void Update()
    {
        //movement.x = Input.GetAxisRaw("Horizontal"); //Gdy gracz naci�nie przycisk przypisany do metody GetAxisRaw klasy Input (s� to strza�ki poziome albo "A", "D") to w przypadku naci�ni�cia
        //strza�ki w lewo albo "A" przypisana zostanie warto�� -1 natomiast dla strza�ki w prawo lub "D" b�dzie to warto�� 1
        movement.x = joystick.Horizontal;
        //movement.y = Input.GetAxisRaw("Vertical"); //Analogicznie jak dla poprzedniej funkcji; strza�ka w g�r� lub "W" to warto�� 1 natomiast strza�ka w d� lub "S" to warto�� -1. 
        //Brak wykonanej akcji oznacza 0
        movement.y = joystick.Vertical;
        animator.SetFloat("Horizontal_move", movement.x);
        animator.SetFloat("Vertical_move", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.sqrMagnitude != 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Fight"))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if(button_fightPressed && !animator.GetCurrentAnimatorStateInfo(0).IsName("Fight"))
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("Fight");
            button_fightPressed = false;
        }
    }

    private void FixedUpdate()
    {
        if (movement.x != 0 && movement.y != 0) //Sprawd� czy gracz idzie po ukosie
        {
            //Zmniejsz pr�dko�� gracza
            movement.x *= moveLimiter;
            movement.y *= moveLimiter;
        }
        body.velocity = new Vector2(movement.x * runSpeed, movement.y * runSpeed); //parametr velocity przyjmuje wektor sk�adaj�cy si� z x oraz y. Dla x poruszamy si� w poziomie a dla y w pionie
    }

    void OnButtonFightClick()
    {
        button_fightPressed = true;
    }
}
