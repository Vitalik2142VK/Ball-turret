using System.Collections;
using UnityEngine;
using YG;

public class GameplayMonitor : MonoBehaviour
{
    [SerializeField, Range(0.5f, 2f)] private float _timeCheck = 1.5f;

    private WaitForSeconds _wait;
    //private bool _isGamePlaying;
    private bool _isMonitoringActive;

    private void OnDisable()
    {
        _isMonitoringActive = false;
    }

    private void Start()
    {
        _wait = new WaitForSeconds(_timeCheck);
        //_isGamePlaying = true;
        _isMonitoringActive = true;

        //StartCoroutine(CheckGameplayState());
    }

    private IEnumerator CheckGameplayState()
    {
        while (_isMonitoringActive == false)
        {
            //bool isGamePlaying = YandexGame.isGamePlaying;

            //if (isGamePlaying != _isGamePlaying)
            //{
            //    if (isGamePlaying)
            //        YandexGame.GameplayStart();
            //    else
            //        YandexGame.GameplayStop();

            //    _isGamePlaying = isGamePlaying;

            //    Console.GetLog($"Game is play == {isGamePlaying}");
            //}

            yield return _wait;
        }
    }
}
