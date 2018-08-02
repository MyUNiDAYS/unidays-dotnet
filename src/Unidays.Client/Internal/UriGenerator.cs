using System;
using System.Text;

namespace Unidays.Client.Internal
{
    internal class UriGenerator
    {
        private const string TrackingUrl = "https://tracking.myunidays.com/v1.2/redemption";
        private const string TrackingScriptUrl = TrackingUrl + "/js";
        private readonly bool generateTestUris;

        public UriGenerator(bool generateTestUris)
        {
            this.generateTestUris = generateTestUris;
        }

        public Uri GenerateScriptUrl(DirectTrackingDetails directTrackingDetails) => new Uri(new StringBuilder()
            .AppendTrackingParameters(directTrackingDetails)
            .AppendTestParameter(generateTestUris)
            .Insert(0, TrackingScriptUrl)
            .ToString());

        public Uri GenerateScriptUrl(string key, DirectTrackingDetails directTrackingDetails) => new Uri(new StringBuilder()
            .AppendTrackingParameters(directTrackingDetails)
            .AppendSignature(key)
            .AppendTestParameter(generateTestUris)
            .Insert(0, TrackingScriptUrl)
            .ToString());

        public Uri GenerateServerUrl(string key, DirectTrackingDetails directTrackingDetails) => new Uri(new StringBuilder()
            .AppendTrackingParameters(directTrackingDetails)
            .AppendSignature(key)
            .AppendTestParameter(generateTestUris)
            .Insert(0, TrackingUrl)
            .ToString());
    }
}
