/* ---------------------------------------------------------------------------
 * File: ThorlabsCct.cs
 * Library: Bev.Instruments.Thorlabs.Ctt
 * Description: Adapter for Thorlabs compact spectrograph devices. Implements
 *              the IArraySpectrometer facade for upstream consumers.
 * Notes: Header added to source file. See project license for copyright.
 * Created: 2025-11-16
 * --------------------------------------------------------------------------- */

using System;
using System.Threading;
using Thorlabs.ManagedDevice.CompactSpectrographDriver;
using Thorlabs.ManagedDevice.CompactSpectrographDriver.Dataset;
using Bev.Instruments.ArraySpectrometer.Abstractions;

namespace Bev.Instruments.Thorlabs.Ctt
{
    public partial class ThorlabsCct : IArraySpectrometer
    {
        private ICompactSpectrographDriver spectrometer;
        private double[] wavelengthsCache; // values must be ordered ascending

        public ThorlabsCct()
        {
            ConnectToFirstAvailableSpectrometerAsync().Wait();
            QueryWavelengths();
        }

        public ThorlabsCct(string deviceId)
        {
            ConnectToSpectrometerById(deviceId);
            QueryWavelengths();
        }

        public string DeviceId => spectrometer.DeviceId;
        public string InstrumentManufacturer => "Thorlabs";
        public string InstrumentType => GetModelFromDeviceId(spectrometer.DeviceId);
        public string InstrumentSerialNumber => GetSerialNumberFromDeviceId(spectrometer.DeviceId);
        public string InstrumentFirmwareVersion => spectrometer.FirmwareVersion;
        public string InstrumentElectronicsId => spectrometer.ElectronicsId;

        public double[] Wavelengths => wavelengthsCache;
        public double MinimumWavelength => wavelengthsCache[0];
        public double MaximumWavelength => wavelengthsCache[wavelengthsCache.Length-1];
        public double SaturationLevel => (double)0xFFFF;

        public bool IsShutterOpen => spectrometer.ShutterOpen;
        public bool IsSaturated => spectrometer.IsSaturated;
        public bool IsLedIndicatorOn => spectrometer.LedIndicatorOn;
        public DateTime DeviceStartDate => spectrometer.HardwareStarted;
        public float Temperature => GetTemperature();
        public int AdcBits => spectrometer.ResolutionAdc;
        public float AdcOffset => spectrometer.ManualOffset;
        public float AdcGain => spectrometer.ManualGain;

        // Integration time in seconds
        public double GetIntegrationTime()
        {
            float exposureTime = spectrometer.ManualExposure;
            return exposureTime/1000.0;
        }

        // Integration time in seconds
        public void SetIntegrationTime(double timeSeconds)
        {
            double timeMs = timeSeconds * 1000.0;
            if(timeMs < 0.01) timeMs = 0.01;
            if(timeMs > 30000) timeMs = 30000;
            spectrometer.SetManualExposureAsync(exposure: (float)timeMs, CancellationToken.None).Wait();
        }

        public double[] GetIntensityData()
        {
            ISpectrumXY spectrum = AcquireSingleSpectrum();
            return spectrum.GetIntensitiesAsDoubles();
        }

        public void CloseShutter()
        {
            spectrometer.SetShutterAsync(open: false, CancellationToken.None).Wait();
            Thread.Sleep(40);
        }

        public void OpenShutter()
        {
            spectrometer.SetShutterAsync(open: true, CancellationToken.None).Wait();
            Thread.Sleep(40);
        }

        public int GetHardwareAveraging() => spectrometer.HwAverage;

        // Frames other than 1 is not recommended!
        // Intensity values are returned as int16, so averaging might result in precision loss.
        public void SetHardwareAveraging(int frames)
        {
            if (frames < 1) frames = 1;
            if (frames > 10000) frames = 10000;
            spectrometer.SetHwAverageAsync(frames, CancellationToken.None).Wait();
        }

        public float GetTemperature()
        {
            spectrometer.FetchTemperatureElectronicsAsync(CancellationToken.None).Wait();
            return spectrometer.TemperatureElectronics;
        }

        public void SwitchLedIndicatorOn()
        {
            spectrometer.SetLedIndicatorAsync(on: true, CancellationToken.None).Wait();
        }

        public void SwitchLedIndicatorOff()
        {
            spectrometer.SetLedIndicatorAsync(on: false, CancellationToken.None).Wait();
        }

        public ISpectrumXY AcquireSingleSpectrum()
        {
            spectrometer.AcquireSingleSpectrumAsync(CancellationToken.None).Wait();
            ISpectrumXY spectrum = spectrometer.GetLatestSpectrum();
            return spectrum;
        }

    }
}
