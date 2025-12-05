using At.Matus.OpticalSpectrumLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testolator
{
    internal static class SpecInfo
    {
        public static string GetInfoString(this IOpticalSpectrum spectrum)
        {
            Dictionary<string, string> dict = spectrum.MetaData.Records;
            StringBuilder sb = new StringBuilder();
            foreach (var key in dict.Keys)
            {
                sb.AppendLine($"{key} = {dict[key]}");
            }
            return sb.ToString();
        }


    }
}
