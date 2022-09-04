using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector2 followSpot; //variavel armazena a posicao que foi clicada
    [SerializeField] private float movementSpeed = 5f; //velocidade do player
    //[SerializeField] private float perspectiveScale = 1f, scaleRatio = 1.5f;
    private Animator animator;
    private Vector2 stuckDistanceCheck;
    private void Start()
    {
        followSpot = transform.position; //ao iniciar a posicao a ser seguida recebe a posicao inicial pra começar parado
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //criando variavel que recebe a posição do ponto da tela em que foi clicado
        if (Input.GetMouseButtonDown(0)) //se clicar com botão esquerdo
        { 
            followSpot = new Vector2(mousePosition.x, mousePosition.y); //o ponto a ser seguido recebe x e y do local em que foi clicado
        }
        transform.position = Vector2.MoveTowards(transform.position, followSpot, movementSpeed * Time.deltaTime); //posicao do player recebe o follow spot, mas para não teleportar usa o MoveTowards e multiplica pelo delta time pra suavizar a movimentação
        //AdjustPerspective();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        float distance = Vector2.Distance(transform.position, followSpot);

        /*
        if (Vector2.Distance(stuckDistanceCheck, transform.position) == 0) 
        { 
            animator.SetFloat("distance", 0f); 
            return; 
        }
        */

        animator.SetFloat("distance", distance);

        if(distance > 0.01)
        {
            Vector3 direction = transform.position - new Vector3(followSpot.x, followSpot.y, transform.position.y);
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            animator.SetFloat("angle", angle);
            //stuckDistanceCheck = transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        followSpot = transform.position;
    }

    /*
    private void AdjustPerspective()
    {
        Vector3 scale = transform.localScale;
        scale.x = perspectiveScale * (scaleRatio - transform.position.y);
        scale.y = perspectiveScale * (scaleRatio - transform.position.y);
        transform.localScale = scale;
        //Debug.Log(perspectiveScale / transform.position.y * scaleRatio);
    }
    */
}
