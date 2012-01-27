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
using ShapeConnectors;

namespace EntitySystemGUI
{

   
    /// <summary>
    /// Interaction logic for NewComponentWindow.xaml
    /// </summary>
    public partial class NewComponentWindow : Window
    {

        
        
        private List<ComponentItem> items = new List<ComponentItem>();
        public NewComponentWindow()
        {
            InitializeComponent();
        }
        private ShapeConnectors.Component component;

        public ShapeConnectors.Component NewComponent
        {
            get { return component; }
            set { component = value; }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Label itemname = new Label();
            itemname.Width = wrapPanel1.Width*(0.2f);
            itemname.Content = "Item Name";
            TextBox itemval = new TextBox();
            itemval.Width = wrapPanel1.Width * (0.2f);

            Label itemnamet = new Label();
            itemnamet.Width = wrapPanel1.Width * (0.2f);
            itemnamet.Content = "Type";
            TextBox itemvalt = new TextBox();
            itemvalt.Width = wrapPanel1.Width * (0.2f);




            ComponentItem citem = new ComponentItem();
            citem.lb = itemname;
            citem.tb = itemval;

            citem.lbtype = itemnamet;
            citem.tbtype = itemvalt;

            items.Add(citem);

            wrapPanel1.Children.Add(itemname);
            wrapPanel1.Children.Add(itemval);
            wrapPanel1.Children.Add(itemnamet);
            wrapPanel1.Children.Add(itemvalt);

        }


        private void removeItem(ComponentItem it)
        {


            wrapPanel1.Children.Remove(it.lb);
            wrapPanel1.Children.Remove(it.tb);
            wrapPanel1.Children.Remove(it.lbtype);
            wrapPanel1.Children.Remove(it.tbtype);
            items.Remove(it); 
        
        
        }


        private void removeItem(object sender, RoutedEventArgs e)
        {

            if (items.Count>0)
            {
                wrapPanel1.Children.Remove(items.Last().lb);
                wrapPanel1.Children.Remove(items.Last().tb);
                wrapPanel1.Children.Remove(items.Last().lbtype);
                wrapPanel1.Children.Remove(items.Last().tbtype);

                items.Remove(items.Last()); 
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            component = new ShapeConnectors.Component();
            component.Name = textBox1.Text;


            foreach (var item in items)
            {
                component.Items.Add(new ShapeConnectors.ComponentItem(item.tb.Text, item.tbtype.Text));
            }

            
        }
    }

    public class ComponentItem
    {
        public Label lb;
        public TextBox tb;

        public Label lbtype;
        public TextBox tbtype;


        public void Hide()
        {
            lb.Visibility = Visibility.Hidden;
            tb.Visibility = Visibility.Hidden;
            lbtype.Visibility = Visibility.Hidden;
            tbtype.Visibility = Visibility.Hidden;
        }
        public void Show()
        {
            lb.Visibility = Visibility.Visible;
            tb.Visibility = Visibility.Visible;
            lbtype.Visibility = Visibility.Visible;
            tbtype.Visibility = Visibility.Visible;
        }

    };
}
