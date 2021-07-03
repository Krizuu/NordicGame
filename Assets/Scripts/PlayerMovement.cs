using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body; //Okreœlamy nazwê dla zmiennej przechowuj¹cej model fizyczny postaci
    public Animator animator;

    public Joystick joystick;
    public Button button_fight;

    float moveLimiter = 0.7f; //zmienna ograniczaj¹ca ruch po ukosie

    public float runSpeed = 5.0f;

    Vector2 movement;

    private bool button_fightPressed;

    // Start is called before the first frame update
    void Start()
    {
        body = GameObject.Find("Player").GetComponent<Rigidbody2D>(); //Pobiera komponent i przechowuje go w wczeœniej przygotowanej zmiennej, dziêki temu nie musimy rêcznie przypisywaæ komponentu.
        button_fight.onClick.AddListener(OnButtonFightClick);
    }

    // Update is called once per frame
    void Update()
    {
        //movement.x = Input.GetAxisRaw("Horizontal"); //Gdy gracz naciœnie przycisk przypisany do metody GetAxisRaw klasy Input (s¹ to strza³ki poziome albo "A", "D") to w przypadku naciœniêcia
        //strza³ki w lewo albo "A" przypisana zostanie wartoœæ -1 natomiast dla strza³ki w prawo lub "D" bêdzie to wartoœæ 1
        movement.x = joystick.Horizontal;
        //movement.y = Input.GetAxisRaw("Vertical"); //Analogicznie jak dla poprzedniej funkcji; strza³ka w górê lub "W" to wartoœæ 1 natomiast strza³ka w dó³ lub "S" to wartoœæ -1. 
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
        if (movement.x != 0 && movement.y != 0) //SprawdŸ czy gracz idzie po ukosie
        {
            //Zmniejsz prêdkoœæ gracza
            movement.x *= moveLimiter;
            movement.y *= moveLimiter;
        }
        body.velocity = new Vector2(movement.x * runSpeed, movement.y * runSpeed); //parametr velocity przyjmuje wektor sk³adaj¹cy siê z x oraz y. Dla x poruszamy siê w poziomie a dla y w pionie
    }

    void OnButtonFightClick()
    {
        button_fightPressed = true;
    }
}
