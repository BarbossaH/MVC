using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// sound manager
    /// </summary>
    public class SoundManager
    {
        private readonly AudioSource _bgmSource = GameObject.Find("game").GetComponent<AudioSource>(); //播发bgm音频组件
        
        private readonly Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

        private float _bgmBGMVolume=1;

        public float BGMVolume
        {
            get => _bgmBGMVolume;
            set
            {
                _bgmBGMVolume = value;
                _bgmSource.volume = _bgmBGMVolume;
            }
        }
        private float _effectVolume=1; //sound effect volume

        public float EffectVolume
        {
            get => _effectVolume;
            set
            {
                _effectVolume = value;
                
            }
        }
        private bool _isStopped;

        public bool IsStopped
        {
            get { return _isStopped; }
            set
            {
                _isStopped = value;
                if (_isStopped)
                {
                    _bgmSource.Pause();
                    // _bgmSource.mute = true;// just cannot hear the voice but the audio clip continues play in the background
                }
                else
                {
                    _bgmSource.Play(); //it will restart the audio clip
                    // _bgmSource.UnPause(); // it will resume the playback of the audio from the point where it was paused
                }
            }
        }
        public void PlayBGM(string name)
        {
            if(_isStopped) return;
            
            if (!_audioClips.ContainsKey(name))
            {
                AudioClip audioClip = Resources.Load<AudioClip>($"Sounds/{name}");
                _audioClips.Add(name, audioClip);
            }
            _bgmSource.clip = _audioClips[name];
            _bgmSource.Play(); //play the sounds
        }
    }
}