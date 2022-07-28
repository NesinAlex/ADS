namespace Devcell.Ads
{
    public interface IBannerAdService
    {
        bool ShowBanner();
        bool HideBanner();
        void Initialize();
    }
}