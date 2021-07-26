    using System;
    using System.Linq;
    using UnityEngine;

    public class AdTimingEvents : MonoBehaviour
    {

        void Awake()
        {
            gameObject.name = "AdTimingEvents";
            DontDestroyOnLoad(gameObject);
        }

        private static event Action _onSdkInitSuccessEvent;
        public static event Action onSdkInitSuccessEvent
        {
            add
            {
                if (_onSdkInitSuccessEvent == null || !_onSdkInitSuccessEvent.GetInvocationList().Contains(value))
                {
                    _onSdkInitSuccessEvent += value;
                }
            }

            remove
            {
                if (_onSdkInitSuccessEvent.GetInvocationList().Contains(value))
                {
                    _onSdkInitSuccessEvent -= value;
                }
            }
        }

        public void onSdkInitSuccess() {
            if (_onSdkInitSuccessEvent != null)
            {
                _onSdkInitSuccessEvent();
            }
        }

        private static event Action<string> _onSdkInitFailedEvent;
        public static event Action<string> onSdkInitFailedEvent
        {
            add
            {
                if (_onSdkInitFailedEvent == null || !_onSdkInitFailedEvent.GetInvocationList().Contains(value))
                {
                    _onSdkInitFailedEvent += value;
                }
            }

            remove
            {
                if (_onSdkInitFailedEvent.GetInvocationList().Contains(value))
                {
                    _onSdkInitFailedEvent -= value;
                }
            }
        }

        public void onSdkInitFailed(string error)
        {
            if (_onSdkInitFailedEvent != null)
            {
                _onSdkInitFailedEvent(error);
            }
        }

        private static event Action<bool> _onRewardedVideoAvailabilityChangedEvent;

        public static event Action<bool> onRewardedVideoAvailabilityChangedEvent
        {
            add
            {
                if (_onRewardedVideoAvailabilityChangedEvent == null || !_onRewardedVideoAvailabilityChangedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoAvailabilityChangedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoAvailabilityChangedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoAvailabilityChangedEvent -= value;
                }
            }
        }

        public void onRewardedVideoAvailabilityChanged(string available)
        {
            if (_onRewardedVideoAvailabilityChangedEvent != null)
            {
                bool isAvailable = (available == "true") ? true : false;
                _onRewardedVideoAvailabilityChangedEvent(isAvailable);
            }
        }

        private static event Action<string> _onRewardedVideoShowedEvent;

        public static event Action<string> onRewardedVideoShowedEvent
        {
            add
            {
                if (_onRewardedVideoShowedEvent == null || !_onRewardedVideoShowedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoShowedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoShowedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoShowedEvent -= value;
                }
            }
        }

        public void onRewardedVideoShowed(string scene)
        {
            if (_onRewardedVideoShowedEvent != null)
            {
                _onRewardedVideoShowedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoShowFailedEvent;

        public static event Action<string> onRewardedVideoShowFailedEvent
        {
            add
            {
                if (_onRewardedVideoShowFailedEvent == null || !_onRewardedVideoShowFailedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoShowFailedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoShowFailedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoShowFailedEvent -= value;
                }
            }
        }

        public void onRewardedVideoShowFailed(string error)
        {
            if (_onRewardedVideoShowFailedEvent != null)
            {
                _onRewardedVideoShowFailedEvent(error);
            }
        }

        private static event Action<string> _onRewardedVideoClosedEvent;

        public static event Action<string> onRewardedVideoClosedEvent
        {
            add
            {
                if (_onRewardedVideoClosedEvent == null || !_onRewardedVideoClosedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoClosedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoClosedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoClosedEvent -= value;
                }
            }
        }

        public void onRewardedVideoClosed(string scene)
        {
            if (_onRewardedVideoClosedEvent != null)
            {
                _onRewardedVideoClosedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoStartedEvent;

        public static event Action<string> onRewardedVideoStartedEvent
        {
            add
            {
                if (_onRewardedVideoStartedEvent == null || !_onRewardedVideoStartedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoStartedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoStartedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoStartedEvent -= value;
                }
            }
        }

        public void onRewardedVideoStarted(string scene)
        {
            if (_onRewardedVideoStartedEvent != null)
            {
                _onRewardedVideoStartedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoEndedEvent;

        public static event Action<string> onRewardedVideoEndedEvent
        {
            add
            {
                if (_onRewardedVideoEndedEvent == null || !_onRewardedVideoEndedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoEndedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoEndedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoEndedEvent -= value;
                }
            }
        }

        public void onRewardedVideoEnded(string scene)
        {
            if (_onRewardedVideoEndedEvent != null)
            {
                _onRewardedVideoEndedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoRewardedEvent;

        public static event Action<string> onRewardedVideoRewardedEvent
        {
            add
            {
                if (_onRewardedVideoRewardedEvent == null || !_onRewardedVideoRewardedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoRewardedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoRewardedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoRewardedEvent -= value;
                }
            }
        }

        public void onRewardedVideoRewarded(string scene)
        {
            if (_onRewardedVideoRewardedEvent != null)
            {
                _onRewardedVideoRewardedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoClickedEvent;

        public static event Action<string> onRewardedVideoClickedEvent
        {
            add
            {
                if (_onRewardedVideoClickedEvent == null || !_onRewardedVideoClickedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoClickedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoClickedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoClickedEvent -= value;
                }
            }
        }
    
        public void onRewardedVideoClicked(string scene)
        {
            if (_onRewardedVideoClickedEvent != null)
            {
                _onRewardedVideoClickedEvent(scene);
            }
        }


        private static event Action<bool> _onInterstitialAvailabilityChangedEvent;

        public static event Action<bool> onInterstitialAvailabilityChangedEvent
        {
            add
            {
                if (_onInterstitialAvailabilityChangedEvent == null || !_onInterstitialAvailabilityChangedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialAvailabilityChangedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialAvailabilityChangedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialAvailabilityChangedEvent -= value;
                }
            }
        }

        public void onInterstitialAvailabilityChanged(string available) {
            if (_onInterstitialAvailabilityChangedEvent != null) {
                bool isAvailable = (available == "true") ? true : false;
                _onInterstitialAvailabilityChangedEvent(isAvailable);
            }
        }

        private static event Action<string> _onInterstitialShowedEvent;

        public static event Action<string> onInterstitialShowedEvent
        {
            add
            {
                if (_onInterstitialShowedEvent == null || !_onInterstitialShowedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialShowedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialShowedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialShowedEvent -= value;
                }
            }
        }

        public void onInterstitialShowed(string scene)
        {
            if (_onInterstitialShowedEvent != null)
            {
                _onInterstitialShowedEvent(scene);
            }
        }

        private static event Action<string> _onInterstitialClosedEvent;

        public static event Action<string> onInterstitialClosedEvent
        {
            add
            {
                if (_onInterstitialClosedEvent == null || !_onInterstitialClosedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialClosedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialClosedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialClosedEvent -= value;
                }
            }
        }

        public void onInterstitialClosed(string scene)
        {
            if (_onInterstitialClosedEvent != null)
            {
                _onInterstitialClosedEvent(scene);
            }
        }

        private static event Action<string> _onInterstitialShowFailedEvent;

        public static event Action<string> onInterstitialShowFailedEvent
        {
            add
            {
                if (_onInterstitialShowFailedEvent == null || !_onInterstitialShowFailedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialShowFailedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialShowFailedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialShowFailedEvent -= value;
                }
            }
        }

        public void onInterstitialShowFailed(string error)
        {
            if (_onInterstitialShowFailedEvent != null)
            {
                _onInterstitialShowFailedEvent(error);
            }
        }

        private static event Action<string> _onInterstitialClickedEvent;

        public static event Action<string> onInterstitialClickedEvent
        {
            add
            {
                if (_onInterstitialClickedEvent == null || !_onInterstitialClickedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialClickedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialClickedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialClickedEvent -= value;
                }
            }
        }

        public void onInterstitialClicked(string scene)
        {
            if (_onInterstitialClickedEvent != null)
            {
                _onInterstitialClickedEvent(scene);
            }
        }

    //************ BannerAds callback
    private static event Action _onBannerLoadSuccessEvent;

    public static event Action onBannerLoadSuccessEvent {
        add
        {
            if (_onBannerLoadSuccessEvent == null || !_onBannerLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onBannerLoadSuccessEvent += value;
            }
        }

        remove
        {
            if (_onBannerLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onBannerLoadSuccessEvent -= value;
            }
        }
    }

    public void onBannerLoadSuccess() {
        if (_onBannerLoadSuccessEvent != null) {
            _onBannerLoadSuccessEvent();
        }
    }

    private static event Action<string> _onBannerLoadFailedEvent;

    public static event Action<string> onBannerLoadFailedEvent
    {
        add
        {
            if (_onBannerLoadFailedEvent == null || !_onBannerLoadFailedEvent.GetInvocationList().Contains(value))
            {
                _onBannerLoadFailedEvent += value;
            }
        }

        remove
        {
            if (_onBannerLoadFailedEvent.GetInvocationList().Contains(value))
            {
                _onBannerLoadFailedEvent -= value;
            }
        }
    }

    public void onBannerLoadFailed(string error)
    {
        if (_onBannerLoadFailedEvent != null)
        {
            _onBannerLoadFailedEvent(error);
        }
    }

    private static event Action _onBannerClickedEvent;

    public static event Action onBannerClickedEvent
    {
        add
        {
            if (_onBannerClickedEvent == null || !_onBannerClickedEvent.GetInvocationList().Contains(value))
            {
                _onBannerClickedEvent += value;
            }
        }

        remove
        {
            if (_onBannerClickedEvent.GetInvocationList().Contains(value))
            {
                _onBannerClickedEvent -= value;
            }
        }
    }

    public void onBannerClicked()
    {
        if (_onBannerClickedEvent != null)
        {
            _onBannerClickedEvent();
        }
    }
}