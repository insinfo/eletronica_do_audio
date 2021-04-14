using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eletronica_do_Audio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
        }
            
        private void btCalc_Click(object sender, RoutedEventArgs e)
        {
            tbCapacitance.Text = Math.Round( EquationsInfo.CalcCapacitanceOffCrossOver(Convert.ToDouble(tbImpedanceTransducer.Text), Convert.ToDouble(tbFrequency.Text)),0).ToString() + "uf"; 

        }
        
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;            
            var selectedTab = (TabItem)tabControl.SelectedItem;
            
            if (selectedTab != null)
            {               
                if (selectedTab.Name == "Tab1")
                {
                    tbInfo.Text = EquationsInfo.CapacitanceOffCrossOver;
                    imageIlustration.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"imagens\CalcCapacitanceOffCrossOver.png"));
                }
                else if (selectedTab.Name == "Tab2")
                {
                    tbInfo.Text = "";
                    imageIlustration.Source = null;
                }
                else if (selectedTab.Name == "Tab3")
                {
                    tbInfo.Text = ""; 
                    imageIlustration.Source = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"imagens\codigo_de_cores_resistores.png"));
                }
            }         
        }

        private void btConvertCapValue_Click(object sender, RoutedEventArgs e)
        {
            if (cbCapMetricUnit.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem) cbCapMetricUnit.SelectedItem;

                if (selectedItem.Content.ToString() == "Microfarad")
                {
                    
                    tbFarad.Text = string.Format("{0:N6}", Convert.ToDouble(tbCapValueInput.Text) / 1000000);
                    tbMicroFarad.Text = string.Format("{0:N2}", Convert.ToDouble(tbCapValueInput.Text));
                    tbPicoFarad.Text = string.Format("{0:N2}", Convert.ToDouble(tbCapValueInput.Text) * 1000000);
                    tbNanoFarad.Text = string.Format("{0:N2}", Convert.ToDouble(tbCapValueInput.Text) * 1000);
                    tbCapCodigoValue.Text = capacitanceToCode(Convert.ToDouble(tbCapValueInput.Text),0);
                }
                if (selectedItem.Content.ToString() == "Picofarad")
                {

                    tbFarad.Text = string.Format("{0:N6}", Convert.ToDouble(tbCapValueInput.Text) / 1000000000000);
                    tbMicroFarad.Text = string.Format("{0:N2}", Convert.ToDouble(tbCapValueInput.Text) * 0.000001);
                    tbPicoFarad.Text = string.Format("{0:N2}", Convert.ToDouble(tbCapValueInput.Text));
                    tbNanoFarad.Text = string.Format("{0:N2}", Convert.ToDouble(tbCapValueInput.Text) * 0.001);
                    tbCapCodigoValue.Text = capacitanceToCode(Convert.ToDouble(tbCapValueInput.Text), 2);
                }
                if (selectedItem.Content.ToString() == "Nanofarad")
                {

                    tbFarad.Text = string.Format("{0:N6}", Convert.ToDouble(tbCapValueInput.Text) / 1000000000);
                    tbMicroFarad.Text = string.Format("{0:N2}", Convert.ToDouble(tbCapValueInput.Text) * 0.001);
                    tbPicoFarad.Text = string.Format("{0:N2}", Convert.ToDouble(tbCapValueInput.Text) * 1000);
                    tbNanoFarad.Text = string.Format("{0:N2}", Convert.ToDouble(tbCapValueInput.Text));
                    tbCapCodigoValue.Text = capacitanceToCode(Convert.ToDouble(tbCapValueInput.Text), 1);
                }
                if (selectedItem.Content.ToString() == "Codigo")
                {
                    tbCapCodigoValue.Text = tbCapValueInput.Text;
                    double digitoMultiplicador = Convert.ToInt64(tbCapValueInput.Text.Substring(2, 1));
                    double codigoCapacitor = Convert.ToInt64(tbCapValueInput.Text);
                    int quantidadeDeDigito = tbCapValueInput.Text.Length;

                    if (quantidadeDeDigito == 3)
                    {

                        codigoCapacitor = Math.Floor(codigoCapacitor / 10);
                        digitoMultiplicador = Math.Pow(10, digitoMultiplicador);

                        tbPicoFarad.Text = string.Format("{0:N2}", codigoCapacitor * digitoMultiplicador);//pF
                        tbMicroFarad.Text = string.Format("{0:N2}",(codigoCapacitor * digitoMultiplicador) /1000000);//uF
                        tbNanoFarad.Text = string.Format("{0:N2}", (codigoCapacitor*digitoMultiplicador)/1000);//nF
                    }
                    else
                    {
                        digitoMultiplicador = 1;
                        tbPicoFarad.Text = string.Format("{0:N2}", codigoCapacitor * digitoMultiplicador); //pF
                        tbMicroFarad.Text = string.Format("{0:N2}",(codigoCapacitor*digitoMultiplicador)/1000000); //uF
                        tbNanoFarad.Text = string.Format("{0:N2}", (codigoCapacitor *digitoMultiplicador)/1000); //nF
                    }

                }
            }
                       

        }

        double calculatemult3(double a, double b)
        {
            double result = 0;

            if (a == 0) result = b;//Microfarad
            else if (a == 1) result = b /= 1000;//Nanofarad
            else if (a == 2) result = b /= 1000000;//Picofarad
            else if (a == 3) result = b /= 1000000000;
            else if (a == 4) result = b /= 1000000000000;

            return result;
        }
        //Converte o valor de capacitancia para codigo de capacitor de ceramica
        string capacitanceToCode(double inputValue,int MULTIPLO=0)
        {
            string result = "";

            var value = Convert.ToInt64(calculatemult3((MULTIPLO + 2), inputValue * 1000000000000));
            result = value.ToString();
            if ((result.Length < 2) || (result.Length > 11))
            {
                result = "fora da faixa";
            }
            else
            {
                result = result.Substring(0, 2) + ((result.Length - 2));
            }
            return result;
        }

        private void btCapValuesClear_Click(object sender, RoutedEventArgs e)
        {
            tbCapValueInput.Text = "";
            tbFarad.Text = "";
            tbMicroFarad.Text = "";
            tbPicoFarad.Text = "";
            tbNanoFarad.Text = "";
            tbCapCodigoValue.Text = "";
        }

      
    }
}
