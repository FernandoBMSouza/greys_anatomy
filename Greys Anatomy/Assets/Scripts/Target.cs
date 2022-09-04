using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector2 followSpot; //variavel armazena a posicao que foi clicada
    [SerializeField] private float movementSpeed = 5f; //velocidade do player
    //[SerializeField] private float perspectiveScale = 1f, scaleRatio = 1.5f;
    private void Start()
    {
        followSpot = transform.position; //ao iniciar a posicao a ser seguida recebe a posicao inicial pra começar parado
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
