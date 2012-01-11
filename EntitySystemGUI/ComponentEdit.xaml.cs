using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EntitySystemGUI
{
    /// <summary>
    /// Interaction logic for ComponentEdit.xaml
    /// </summary>
    public partial class ComponentEdit : Window
    {
        private OutputFileEntity propertie;
        public string editingcomponentname;

        public ComponentEdit(OutputFileEntity prop)
        {
            this.propertie = prop;
            InitializeComponent();
        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            UIElement[] elements = new UIElement[stackPanel1.Children.Count];
            stackPanel1.Children.CopyTo(elements, 0);

            elements = elements.ToList().FindAll(p => p is TextBox).ToArray();


            //recuperando o dinamic component que vai ser gravado no arquivo
            DinamicComponentPresenter dinamic = propertie.getDinamicComponentbyName(editingcomponentname);
            
            foreach (var item in elements)
            {
                TextBox tt = item as TextBox;
                dinamic.val[tt.Name.Replace("_", "")] = tt.Text;
                
            }
            
            this.Close();
        }
    }
}
