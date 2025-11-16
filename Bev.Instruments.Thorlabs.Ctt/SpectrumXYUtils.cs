using System;
using Thorlabs.ManagedDevice.CompactSpectrographDriver.Dataset;

namespace Bev.Instruments.Thorlabs.Ctt
{
    public static class SpectrumXYUtils
    {
        public static double[] GetIntensitiesAsDoubles(this ISpectrumXY spectrum)
        {
            if (spectrum == null)
            {
                throw new ArgumentNullException(nameof(spectrum));
            }
            return spectrum.Intensity;
        }

        public static double[] GetWavelengthsAsDoubles(this ISpectrumXY spectrum)
        {
            if (spectrum == null)
            {
                throw new ArgumentNullException(nameof(spectrum));
            }
            return spectrum.Wavelength;
        }

        public static double GetMaxIntensity(this ISpectrumXY spectrum)
        {
            if (spectrum == null)
            {
                throw new ArgumentNullException(nameof(spectrum));
            }
            double maxIntensity = double.MinValue;
            foreach (var intensity in spectrum.Intensity)
            {
                if (intensity > maxIntensity)
                {
                    maxIntensity = intensity;
                }
            }
            return maxIntensity;
        }
    }
}
