namespace Devcell.Ads
{
    public interface IVideoAdService
    {
        string AdUnitId { get; }
        IVideoAdService Initialize();
        bool TryPlayAd();
    }
}