using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletronica_do_Audio
{
    class EquationsInfo
    {
        public static string CapacitanceOffCrossOver = @"Através das equações abaixo, podemos calcular o valor do capacitor C, em micro Farads, em função da
impedância nominal do transdutor, representada por Rh, e da freqüência de corte Fc.";

        //Metodo recebe como argumento a impedância nominal do transdutor e freqüência de corte e retorna o valor do capacitor em micro Farads
        public static double CalcCapacitanceOffCrossOver(double impedanceTransducer, double cutoffFrequency)
        {

            return 1000000 / (2 * Math.PI * impedanceTransducer * cutoffFrequency);
        }

    }
}
