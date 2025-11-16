using System;

namespace Bev.Instruments.Thorlabs.Ctt
{
    public static class Exposure
    {
        public static double GetOptimalExposureTime(this ThorlabsCct cct) => cct.GetOptimalExposureTime(0.95 * (double)0xFFFF, false);

        public static double GetOptimalExposureTime(this ThorlabsCct cct, bool debug) => cct.GetOptimalExposureTime(0.95 * (double)0xFFFF, debug);

        public static double GetOptimalExposureTime(this ThorlabsCct cct, double targetSignal, bool debug)
        {
            double maxIntegrationTime = 30.0; // seconds
            double minIntegrationTime = 0.00001; // seconds

            double optimalIntegrationTime = 0;
            double integrationTime = minIntegrationTime; // seconds
            cct.SetIntegrationTime(integrationTime);
            double minExposureSignal = cct.AcquireSingleSpectrum().GetMaxIntensity();

            while (integrationTime < maxIntegrationTime)
            {
                cct.SetIntegrationTime(integrationTime);
                double maxSignal = cct.AcquireSingleSpectrum().GetMaxIntensity();

                if (debug)
                {
                    Console.WriteLine($">>> debug {cct.GetIntegrationTime():F5} s -> {maxSignal:F0}");
                }
                if (maxSignal >= 0.49 * targetSignal)
                {
                    // Estimate optimal integration time by linear extrapolation
                    optimalIntegrationTime = cct.GetIntegrationTime() * (targetSignal / (maxSignal - minExposureSignal));
                    break;
                }
                integrationTime *= 2;
            }
            var finalIntegrationTime = RoundToSignificantDigits(optimalIntegrationTime, 2);
            if (finalIntegrationTime > maxIntegrationTime)
            {
                finalIntegrationTime = maxIntegrationTime;
            }
            cct.SetIntegrationTime(finalIntegrationTime);
            if (debug)
            {
                double maxSignal = cct.AcquireSingleSpectrum().GetMaxIntensity();
                Console.WriteLine($">>> debug final {cct.GetIntegrationTime():F5} s -> {maxSignal:F0}");
            }
            return finalIntegrationTime;
        }

        private static double RoundToSignificantDigits(double number, int digits)
        {
            int sign = Math.Sign(number);
            if (sign < 0) number *= -1;
            if (number == 0) return 0;
            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(number))) + 1);
            return sign * scale * Math.Round(number / scale, digits);
        }
    }
}
