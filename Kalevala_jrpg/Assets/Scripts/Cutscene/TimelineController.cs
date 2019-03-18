using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelineController : MonoBehaviour
{
    public PlayableDirector director;
    
    void OnEnable()
    {
        //director.paused += OnPlayableDirectorPaused;
        OnPlayableDirectorPaused(director);
    }

    void OnPlayableDirectorPaused(PlayableDirector aDirector)
    {
        if (director == aDirector)
            aDirector.Pause();

    }

    void OnDisable()
    {
        director.paused -= OnPlayableDirectorPaused;
    }
}
