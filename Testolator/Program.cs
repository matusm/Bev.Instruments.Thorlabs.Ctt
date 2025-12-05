using System;
using System.Threading;
using Bev.Instruments.Thorlabs.Ctt;
using At.Matus.OpticalSpectrumLib;
using Thorlabs.ManagedDevice.CompactSpectrographDriver.Dataset;

namespace Testolator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var cct = new ThorlabsCct("CCT10-M01275370");
            var cct = new ThorlabsCct();

            Console.WriteLine($"DeviceId:           {cct.DeviceId}");
            Console.WriteLine($"DeviceStarted:      {cct.DeviceStartDate}");
            Console.WriteLine($"Manufacturer:       {cct.InstrumentManufacturer}");
            Console.WriteLine($"Type:               {cct.InstrumentType}");
            Console.WriteLine($"SerialNumber:       {cct.InstrumentSerialNumber}");
            Console.WriteLine($"Firmware:           {cct.InstrumentFirmwareVersion}");
            Console.WriteLine($"Electronics:        {cct.InstrumentElectronicsId}");
            Console.WriteLine($"Minimum Wavelength: {cct.MinimumWavelength} nm");
            Console.WriteLine($"Maximum Wavelength: {cct.MaximumWavelength} nm");
            Console.WriteLine($"ADC resolution:     {cct.AdcBits} bit");
            Console.WriteLine($"ADC offset:         {cct.AdcOffset} mV");
            Console.WriteLine($"ADC gain:           {cct.AdcGain} dB");
            Console.WriteLine($"Integration Time:   {cct.GetIntegrationTime()} s");
            Console.WriteLine($"Is Shutter Open:    {cct.IsShutterOpen}");
            Console.WriteLine($"Temperature:        {cct.Temperature} °C");
            Console.WriteLine($"Hardware averaging: {cct.GetHardwareAveraging()}");

            cct.SetHardwareAveraging(1);


            Console.WriteLine();
            cct.SetIntegrationTime(0.5);
            Console.WriteLine($"Integration Time set to: {cct.GetIntegrationTime()} s");

            Console.WriteLine();
            cct.OpenShutter();

            MeasuredOpticalSpectrum spec1 = new MeasuredOpticalSpectrum(cct.Wavelengths);
            MeasuredOpticalSpectrum spec2 = new MeasuredOpticalSpectrum(cct.Wavelengths);

            cct.OpenShutter();
            spec1.UpdateSignal(cct.GetIntensityData());
            spec1.UpdateSignal(cct.GetIntensityData());
            spec1.AddMetaDataRecord("Name", "Name of first spectrum");    
            spec1.AddMetaDataRecord("Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            spec1.AddMetaDataRecord("IntegrationTime_s", cct.GetIntegrationTime().ToString("F3"));
            spec1.AddMetaDataRecord("HardwareAveraging", cct.GetHardwareAveraging().ToString());
            spec1.AddMetaDataRecord("ShutterOpen", cct.IsShutterOpen.ToString());

            cct.SetIntegrationTime(0.3);

            cct.CloseShutter();
            spec2.UpdateSignal(cct.GetIntensityData());
            spec2.UpdateSignal(cct.GetIntensityData());
            spec2.UpdateSignal(cct.GetIntensityData());
            spec2.AddMetaDataRecord("Name", "Name of second spectrum");
            spec2.AddMetaDataRecord("Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            spec2.AddMetaDataRecord("IntegrationTime_s", cct.GetIntegrationTime().ToString("F3"));
            spec2.AddMetaDataRecord("HardwareAveraging", cct.GetHardwareAveraging().ToString());
            spec2.AddMetaDataRecord("ShutterOpen", cct.IsShutterOpen.ToString());

            var spec3 = SpecMath.Subtract(spec1, spec2);
            spec3.AddMetaDataRecord("Name", "Subtracted spectrum (spec1 - spec2)");

            Console.WriteLine();
            Console.WriteLine(spec1.GetInfoString());
            Console.WriteLine();
            Console.WriteLine(spec2.GetInfoString());
            Console.WriteLine();
            Console.WriteLine(spec3.GetInfoString());





            //cct.GetOptimalExposureTime(true);
            //Console.WriteLine($"Optimal Integration Time set to: {cct.GetIntegrationTime()} s");


            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine();
            //    cct.SetHardwareAveraging(1);
            //    spec = cct.AcquireSingleSpectrum();
            //    wavelengths = spec.Wavelength;
            //    intensities = spec.Intensity;
            //    Console.WriteLine($"Acquired:        {spec.Acquired}");
            //    Console.WriteLine($"HardwareAverage: {spec.HardwareAverage}");
            //    Console.WriteLine($"Exposure:        {spec.SensorExposureMs} ms");
            //    Console.WriteLine($"Tick:            {spec.Tick}");
            //    Console.WriteLine($"Pixel#:          {wavelengths.Length}");
            //    Console.WriteLine($"Temperature:     {cct.GetTemperature()} °C");
            //    Console.WriteLine($"MaxIntensity     {spec.GetMaxIntensity()}");
            //    Console.WriteLine($"IsSaturated      {cct.IsSaturated}");
            //}

            //Console.WriteLine();
            //cct.SetHardwareAveraging(4);
            //spec = cct.AcquireSingleSpectrum();
            //wavelengths = spec.Wavelength;
            //intensities = spec.Intensity;

            //Console.WriteLine($"Acquired:        {spec.Acquired}");
            //Console.WriteLine($"HardwareAverage: {spec.HardwareAverage}");
            //Console.WriteLine($"Exposure:        {spec.SensorExposureMs} ms");
            //Console.WriteLine($"Tick:            {spec.Tick}");
            //Console.WriteLine($"Pixel#:          {wavelengths.Length}");
            //Console.WriteLine($"Temperature:     {cct.GetTemperature()} °C");
            //Console.WriteLine($"MaxIntensity     {spec.GetMaxIntensity()}");
            //Console.WriteLine($"IsSaturated      {cct.IsSaturated}");

            //for (int i = 0; i < wavelengths.Length; i++)
            //{
            //    Console.WriteLine($"{wavelengths[i]:F3} nm, {intensities[i]}");
            //}

        }
    }
}
