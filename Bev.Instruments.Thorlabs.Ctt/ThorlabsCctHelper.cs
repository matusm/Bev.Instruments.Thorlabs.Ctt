using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Thorlabs.ManagedDevice.CompactSpectrographDriver.Dataset;
using Thorlabs.ManagedDevice.CompactSpectrographDriver.Workflow;

namespace Bev.Instruments.Thorlabs.Ctt
{
    public partial class ThorlabsCct
    {
        private void QueryWavelengths()
        {
            SetHardwareAveraging(1);
            SetIntegrationTime(0.01);
            ISpectrumXY spectrum = AcquireSingleSpectrum();
            wavelengthsCache = spectrum.GetWavelengthsAsDoubles();
        }

        private string GetModelFromDeviceId(string deviceId)
        {
            // Return the substring before the first hyphen.
            // If deviceId is null or empty return it as-is.
            // If there's no hyphen, return the full deviceId.
            // If the hyphen is the first character, return empty string (no preceding token).
            if (string.IsNullOrEmpty(deviceId))
                return deviceId;
            int idx = deviceId.IndexOf('-');
            if (idx < 0)
                return deviceId;
            if (idx == 0)
                return string.Empty;
            return deviceId.Substring(0, idx);
        }

        private string GetSerialNumberFromDeviceId(string deviceId)
        {
            // Return the substring after the first hyphen.
            // If deviceId is null or empty return it as-is.
            // If there's no hyphen, return empty string (no following token).
            // If the hyphen is the last character, return empty string (no following token).
            if (string.IsNullOrEmpty(deviceId))
                return deviceId;
            int idx = deviceId.IndexOf('-');
            if (idx < 0)
                return string.Empty;
            if (idx == deviceId.Length - 1)
                return string.Empty;
            return deviceId.Substring(idx + 1);
        }

        private async Task ConnectToFirstAvailableSpectrometerAsync()
        {
            StartupHelperCompactSpectrometer startupHelper = new StartupHelperCompactSpectrometer();
            IEnumerable<string> discoveredDevices = await startupHelper.GetKnownDevicesAsync(CancellationToken.None);
            string deviceId = discoveredDevices.FirstOrDefault();
            spectrometer = startupHelper.GetCompactSpectrographById(deviceId);
        }

        private void ConnectToSpectrometerById(string deviceId)
        {
            StartupHelperCompactSpectrometer startupHelper = new StartupHelperCompactSpectrometer();
            startupHelper.GetKnownDevicesAsync(CancellationToken.None).Wait(); // Ensure devices are discovered
            spectrometer = startupHelper.GetCompactSpectrographById(deviceId);
        }
    }
}
