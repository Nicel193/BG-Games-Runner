using System;

namespace Code.Runtime.Services.AdsService
{
    public interface IAdsService
    {
        void Initialize();
        void ShowRewardedAd(Action onVideoFinished);
    }
}