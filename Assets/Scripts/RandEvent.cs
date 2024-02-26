using UnityEngine;

public class RandEvent : MonoBehaviour
{
    public bool CircusAlrdPlayed = false;
    public AudioSource Circus;
    public float timer = 0f;
    public int interval;

    void Start()
    {
        // Générer un intervalle initial aléatoire entre 30 et 60 secondes
        interval = Random.Range(30, 61);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;

            // Générer un nouvel intervalle aléatoire entre 30 et 60 secondes
            interval = Random.Range(30, 61);

            int randomNumber = Random.Range(0, 101);

            if (randomNumber >= 35 && randomNumber <= 60)
            {
                CircusPlay();
            }
        }
    }

    void CircusPlay()
    {
        if (CircusAlrdPlayed == false)
        {
            Circus.Play();
            CircusAlrdPlayed = true;
        }
    }
}
