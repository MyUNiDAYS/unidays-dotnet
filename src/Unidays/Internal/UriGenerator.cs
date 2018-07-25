using System;
using System.Text;

namespace Unidays.Internal
{
    internal class UriGenerator
    {
        private const string TrackingUrl = "https://tracking.myunidays.com/v1.2/redemption";
        private const string TrackingGifUrl = TrackingUrl + "/gif";
        private readonly bool generateTestUris;

        public UriGenerator(bool generateTestUris)
        {
            this.generateTestUris = generateTestUris;
        }

        public Uri GeneratePixelUrl(DirectTrackingDetails directTrackingDetails) => new Uri(new StringBuilder()
            .AppendTrackingParameters(directTrackingDetails)
            .AppendTestParameter(generateTestUris)
            .Insert(0, TrackingGifUrl)
            .ToString());

        public Uri GeneratePixelUrl(string key, DirectTrackingDetails directTrackingDetails) => new Uri(new StringBuilder()
            .AppendTrackingParameters(directTrackingDetails)
            .AppendSignature(key)
            .AppendTestParameter(generateTestUris)
            .Insert(0, TrackingGifUrl)
            .ToString());

        public Uri GenerateServerUrl(string key, DirectTrackingDetails directTrackingDetails) => new Uri(new StringBuilder()
            .AppendTrackingParameters(directTrackingDetails)
            .AppendSignature(key)
            .AppendTestParameter(generateTestUris)
            .Insert(0, TrackingUrl)
            .ToString());
    }
}
