using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : BasicEnemy
{
    public float moveSpeed;   // Algemene bewegingssnelheid van de vijand
    private Vector3 targetPosition;
    private bool movingToStartPosition = true;
    private float currentWaveSpeedMultiplier;
    public int currentWave = 1; // Dit moet in je game manager worden ingesteld

    private Vector3 direction;
    private int movementType;

   protected override void Start()
    {
        // Bepaal de willekeurige richting
        movementType = Random.Range(1, 3); // 1 voor links-rechts, 2 voor links-rechts + naar beneden
        SetInitialPosition();
        SetMovementDirection();

        // Bereken de snelheid op basis van de golf
        currentWaveSpeedMultiplier = 1 + (currentWave / 4f);
        moveSpeed *= currentWaveSpeedMultiplier;
    }

    protected override void Update()
    {
        if (movingToStartPosition)
        {
            MoveToStartPosition();
        }
        else
        {
            MoveInDirection();
        }
    }

    // Stel de initiële positie in
    private void SetInitialPosition()
    {
        float xPos = Random.Range(-8f, 8f);  // Stel de breedte van het speelveld in
        float yPos = Random.Range(3f, 5f);   // Stel de hoogte in waar de vijand start

        targetPosition = new Vector3(xPos, yPos, 0f);
        transform.position = new Vector3(xPos, 6f, 0f); // Startpositie boven het scherm
    }

    // Beweeg de vijand naar zijn startpositie
    private void MoveToStartPosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Als de vijand dichtbij genoeg is, start de bewegingsroutine
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingToStartPosition = false;
        }
    }

    // Stel de bewegingsrichting in
    private void SetMovementDirection()
    {
        if (movementType == 1)
        {
            // Beweeg horizontaal van links naar rechts
            direction = Random.value > 0.5f ? Vector3.right : Vector3.left;
        }
        else if (movementType == 2)
        {
            // Beweeg horizontaal + naar beneden
            direction = new Vector3(Random.value > 0.5f ? 1 : -1, -1, 0f).normalized;
        }
    }

    // Beweeg in de bepaalde richting
    private void MoveInDirection()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Houd de vijand binnen het scherm
        if (transform.position.x > 8f || transform.position.x < -8f)
        {
            direction.x = -direction.x;  // Draai de horizontale beweging om
        }

/*        // Optioneel: vernietig de vijand als hij de onderkant van het scherm bereikt
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }*/
    }
}
