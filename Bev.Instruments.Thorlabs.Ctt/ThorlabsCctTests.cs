using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thorlabs.ManagedDevice.CompactSpectrographDriver.Dataset;

namespace Bev.Instruments.Thorlabs.Ctt
{
    public partial class ThorlabsCct
    {
        public ISpectrumXY AcquireDarkSpectrum()
        {
            spectrometer.UpdateDarkSpectrumAsync(false, CancellationToken.None).Wait();
            ISpectrumXY spectrum = spectrometer.GetDarkSpectrum();
            return spectrum;
        }

        public ISpectrumXY AcquireBackgroundSpectrum()
        {
            spectrometer.UpdateBackgroundSpectrumAsync(false, CancellationToken.None).Wait();
            ISpectrumXY spectrum = spectrometer.GetBackgroundSpectrum();
            return spectrum;
        }

        public ISpectrumXY GetSingleSpectrum() => spectrometer.GetLatestSpectrum();
        public ISpectrumXY GetBackgroundSpectrum() => spectrometer.GetBackgroundSpectrum();
        public ISpectrumXY GetDarkSpectrum() => spectrometer.GetDarkSpectrum();

    }
}
