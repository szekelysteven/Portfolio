using UnityEngine;
//this script will control the game at a global level
public class WorldControl : MonoBehaviour
{
    public AudioSource ambiantSource;
    //control ambiant audio, have it start at random points each game load
    void Start()
    {
        ambiantSource.time = Random.Range(0f, ambiantSource.clip.length);
        ambiantSource.Play();

    }
}
