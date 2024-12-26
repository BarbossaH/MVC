using View;
using UnityEngine.UI;

namespace View
{
    //audio setting ui
    public class SettingsView : BaseView
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            Find<Button>("bg/closeBtn").onClick.AddListener(OnCloseBtnClicked);
            Find<Toggle>("bg/soundToggle").onValueChanged.AddListener(OnSoundToggleClicked);
            Find<Slider>("bg/bgmSlider").onValueChanged.AddListener(OnBgmSliderChanged);
            Find<Slider>("bg/effectSlider").onValueChanged.AddListener(OnSoundEffectsSliderChanged);

            // to read the value of sound manager, because it could store the value into the configuration files.
            Find<Toggle>("bg/soundToggle").isOn = GameManager.SoundManager.IsStopped; 
            Find<Slider>("bg/bgmSlider").value = GameManager.SoundManager.BGMVolume;
            Find<Slider>("bg/effectSlider").value = GameManager.SoundManager.EffectVolume;
        }

        private void OnCloseBtnClicked()
        {
            GameManager.ViewManager.CloseView(ViewId); //close self
        }

        private void OnSoundToggleClicked(bool isMuted)
        {
            GameManager.SoundManager.IsStopped = isMuted;
        }

        private void OnBgmSliderChanged(float value)
        {
            GameManager.SoundManager.BGMVolume = value;
        }

        private void OnSoundEffectsSliderChanged(float value)
        {
            GameManager.SoundManager.EffectVolume = value;
        }
    }
}