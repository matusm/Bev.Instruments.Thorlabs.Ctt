using System;
using Bev.Instruments.Thorlabs.Ctt;
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
            Console.WriteLine($"Manufacturer:       {cct.InstrumentManufacturer}");
            Console.WriteLine($"Type:               {cct.InstrumentType}");
            Console.WriteLine($"SerialNumber:       {cct.InstrumentSerialNumber}");
            Console.WriteLine($"Firmware:           {cct.InstrumentFirmwareVersion}");
            Console.WriteLine($"Electronics:        {cct.InstrumentElectronicsId}");
            Console.WriteLine($"Minimum Wavelength: {cct.MinimumWavelength} nm");
            Console.WriteLine($"Maximum Wavelength: {cct.MaximumWavelength} nm");
            Console.WriteLine();

            Console.WriteLine($"Integration Time:   {cct.GetIntegrationTime()} s");
            Console.WriteLine($"Is Shutter Open:    {cct.IsShutterOpen}");
            Console.WriteLine($"Temperature:        {cct.Temperature} °C");
            cct.SetIntegrationTime(0.5);
            Console.WriteLine($"Integration Time set to: {cct.GetIntegrationTime()} s");

            ISpectrumXY spec;
            double[] wavelengths;
            double[] intensities;

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
