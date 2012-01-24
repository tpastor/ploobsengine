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
using System.IO;
using System.Xml.Serialization;
using ShapeConnectors;
using System.Security.Permissions;
using Microsoft.Win32;
using EntitySystemGUI.Importers;

namespace EntitySystemGUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

        // flag for enabling "New thumb" mode
        bool isAddNewAction = false;

        // flag for enabling a new tag
        bool isAddNewTag = false;

        // flag for enabling "New link" mode
        bool isAddNewLink = false;
        // flag that indicates that the link drawing with a mouse started
        bool isLinkStarted = false;
        // variable to hold the thumb drawing started from
        MyThumb linkedThumb;
        // Line drawn by the mouse before connection established

        List<TreeViewItem> treeitem;//adiciona projetos nesse


        ComponentEdit cedit;
        string AddComponentName;

        LineGeometry link;

        EntityProperty entityproperty;

        Dictionary<String, Component> LoadedComponents = new Dictionary<string, Component>();
        TreeViewItem ent; // o componente

        OutputFileEntity entityfile = new OutputFileEntity("Entity1");

        IComponentImporter importer;

        


		public MainWindow()
		{
			this.InitializeComponent();

            
           

            treeitem = new List<TreeViewItem>();


            entityproperty = new EntityProperty();
            entityproperty.Name = myThumb1.Title;

            ent = new TreeViewItem();

            ent.Header = entityproperty.Name;

            

            treeitem.Add(ent);
            
            



            treeView1.ItemsSource = treeitem;


            
            
			// Insert code required on object creation below this point.
		}


        public void AddComponent(Object toadd)
        {
            
         
            ent.Items.Add(toadd);
            
        
        }

        private void canvas1_Drop(object sender, DragEventArgs e)
        {
            //AddComponent();

        }

       

        public Component DeserializeComponent(string name)
        {
            FileStream fs = new FileStream(name, FileMode.Open);
            XmlSerializer xs = new XmlSerializer(typeof(Component));
            Component actualcomponent = (Component)xs.Deserialize(fs);

            fs.Close();
            return actualcomponent;

        }

        public Tag DeserializeTag(string name)
        {
            FileStream fs = new FileStream(name, FileMode.Open);
            XmlSerializer xs = new XmlSerializer(typeof(Tag));
            Tag actualtag = (Tag)xs.Deserialize(fs);

            fs.Close();
            return actualtag;

        }

        [SecurityPermission(SecurityAction.LinkDemand,
            Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void SerializeContainer(EntityContainer cont, string filename)
        {


            entityfile.CreateXml(filename);

            //FileStream fs = new FileStream("..\\..\\Saved\\" + filename, FileMode.Create);
            //XmlSerializer xs = new XmlSerializer(typeof(DinamicComponentPresenter));
            //xs.Serialize(fs, cont);
            //fs.Close();

            

        }

        private void listView1_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Do work. Can special-case logic based on Copy, Move, etc. 
                string[] fileNames = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                foreach (var item in fileNames)
                {
                    string filename="";
                    Component cc = new Component();
                    Tag tt = new Tag();
                    Label lb1 = new Label();

                    lb1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    Image img = new Image();
                    if (item.Contains("Component"))
                    {
                       filename="Images\\Component.png";
                       cc = DeserializeComponent(item);
                       LoadedComponents.Add(cc.Name, cc);
                       img.Tag = cc.Name+"Component";
                        lb1.Content= cc.Name;
                    }
                    else if (item.Contains("Tag"))
                    {
                        filename = "Images\\Tag.png";
                        tt = DeserializeTag(item);
                        img.Tag = tt.Name + "Tag";
                        lb1.Content = tt.Name;
                    }



                    if ( filename.Length>0)
                    {                 
                     
                        BitmapImage src = new BitmapImage();
                        src.BeginInit();
                        src.UriSource = new Uri(filename, UriKind.Relative);
                        src.EndInit();





                        StackPanel stk1 = new StackPanel();

                        img.Source = src;
                        
                        img.Width = 50;
                        img.Height = 30;
                        img.Margin = new Thickness(10);
                        img.MouseLeftButtonDown += ListViewItem_MouseLeftButtonDown;
                        stk1.Children.Add(img);
                        stk1.Children.Add(lb1);


                        listView1.Items.Add(stk1);
                        

                        
                    }
                }
            
            
            }

            e.Handled = true; 
        }

      
        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image bt = (Image)sender;

            AddComponentName = bt.Tag.ToString();


            if (AddComponentName.Contains("Component"))
            {
                isAddNewAction = true;
                isAddNewTag = false;
            }
            else if (AddComponentName.Contains("Tag"))
            {
                isAddNewAction = false;
                isAddNewTag = true;
            }
            
            
            Mouse.OverrideCursor = Cursors.SizeAll;
        }

        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAddNewLink && isLinkStarted)
            {
                // Set the new link end point to current mouse position
                link.EndPoint = e.GetPosition(canvas1);
                e.Handled = true;
            }

        }

        private void onDragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            // Exit dragging operation during adding new link
            if (isAddNewLink) return;

            MyThumb thumb = e.Source as MyThumb;

            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);

            // Update links' layouts for active thumb
            thumb.UpdateLinks();

        }

        private void tabControl1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // If adding new action...
            if (isAddNewAction || isAddNewTag)
            {
                string imagename = "/Images/Component.png"; 
                if (isAddNewTag)
                {
                    imagename = "/Images/Tag.png";
                    AddComponentName = AddComponentName.Replace("Tag", "");
                }
                else
                {
                    imagename = "/Images/Component.png";
                    AddComponentName = AddComponentName.Replace("Component", "");
                }
                // Create new thumb object based on a static template "BasicShape1"
                // specifying thumb text as "action" and icon as "/Images/gear_connection.png"
                MyThumb newThumb = new MyThumb(
                    Application.Current.Resources["BasicShape1"] as ControlTemplate, AddComponentName,
                    imagename,
                    e.GetPosition(canvas1),
                    onDragDelta);
                //newThumb.MouseDown += new MouseButtonEventHandler(newThumb_MouseDown);

                newThumb.MouseDoubleClick += new MouseButtonEventHandler(newThumb_MouseDoubleClick);

                // Put newly created thumb on the canvas
                canvas1.Children.Add(newThumb);

                // resume common layout for application
                isAddNewAction = false;
                isAddNewTag = false;
                Mouse.OverrideCursor = null;
                //                btnNewAction.IsEnabled = btnNewLink.IsEnabled = true;
                e.Handled = true;
            }

            
        }

        void newThumb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            cedit = new ComponentEdit(entityfile);

            MyThumb thumb = sender as MyThumb;

            cedit.Left = e.GetPosition(canvas1).X;
            cedit.Top = e.GetPosition(canvas1).Y;

            cedit.stackPanel1.Children.Clear();

            
            Component clicked = LoadedComponents[thumb.Title];

            foreach (var item in clicked.Items)
            {
                Label lb1 = new Label();
                lb1.Content = item.name;

                TextBox tb1 = new TextBox();
                tb1.Name = "_"+item.name ;

                cedit.stackPanel1.Children.Add(lb1);
                cedit.stackPanel1.Children.Add(tb1);
            }

            cedit.editingcomponentname = thumb.Title;
            cedit.Title = "Editing Component " + thumb.Title;
            cedit.Owner = this;
            cedit.Show();
        }

       

        private void tabControl1_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Mouse.OverrideCursor = null;

            // If "Add link" mode enabled and line drawing started (line placed to canvas)
            if (isAddNewLink && isLinkStarted)
            {
                // declare the linking state
                bool linked = false;
                // We released the button on MyThumb object
                if (e.Source.GetType() == typeof(MyThumb))
                {
                    MyThumb targetThumb = e.Source as MyThumb;
                    // define the final endpoint of line
                    link.EndPoint = e.GetPosition(canvas1);
                    // if any line was drawn (avoid just clicking on the thumb)
                    if (link.EndPoint != link.StartPoint && linkedThumb != targetThumb && linkedThumb!=null)
                    {
                        // establish connection
                        linkedThumb.LinkTo(targetThumb, link);
                        // set linked state to true
                        linked = true;

                        DinamicComponentPresenter prop = new DinamicComponentPresenter();

                        Component cc = LoadedComponents[linkedThumb.Title];

                        prop.Name = cc.Name;
                        foreach (var item in cc.Items)
                        {
                            prop.val.Add(item.name, "");
                        }

                        entityfile.Components.Add(prop);
                        
                        ComponentProperty cp = new ComponentProperty(prop);
                        cp.Name = linkedThumb.Title;

                        AddComponent(cp);
                    }
                }
                // if we didn't manage to approve the linking state
                // button is not released on MyThumb object or double-clicking was performed
                if (!linked)
                {
                    // remove line from the canvas
                    connectors.Children.Remove(link);
                    // clear the link variable
                    link = null;
                }

                // exit link drawing mode
                isLinkStarted = isAddNewLink = false;
                // configure GUI
                //btnNewAction.IsEnabled = btnNewLink.IsEnabled = true;
                Mouse.OverrideCursor = null;
                e.Handled = true;
            }
            
        }

        private void dockPanel1_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            Mouse.OverrideCursor = Cursors.Cross;
            // Is adding new link and a thumb object is clicked...
           
                if (!isLinkStarted)
                {
                    if (link == null || link.EndPoint != link.StartPoint)
                    {
                        Point position = e.GetPosition( canvas1);

                        
                        link = new LineGeometry(position, position);
                        connectors.Children.Add(link);
                        
                        isLinkStarted = true;
                        isAddNewLink = true;
                        linkedThumb = e.Source as MyThumb;
                        e.Handled = true;

                       
                        
                    }
                }
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog diag = new SaveFileDialog();
            diag.Filter = "XML Files|*.xml";
            diag.Title = "Save an XML File";
            
            diag.ShowDialog();

            SerializeContainer(null, diag.FileName);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XML Files|*.xml";
            open.Title = "Open an XML File";
            open.ShowDialog();
        }

        private void ImportEvent(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "C# Files|*.cs";
            open.Title = "Open an C# File";
            open.ShowDialog();


            //Criando um importer de C#
            importer = new CSharpComponentImporter();



            
            foreach (var item in open.FileNames)
            {
                AddComponentToTreeView( importer.ImportfromFile(item));
                
            }

        }

        private void AddComponentToTreeView(Component component)
        {

           
                string filename = "Images\\Component.png";
                Image img = new Image();
                Label lb1 = new Label();
                LoadedComponents.Add(component.Name, component);
                img.Tag = component.Name + "Component";
                lb1.Content = component.Name;
            

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(filename, UriKind.Relative);
            src.EndInit();





            StackPanel stk1 = new StackPanel();

            img.Source = src;

            img.Width = 50;
            img.Height = 30;
            img.Margin = new Thickness(10);
            img.MouseLeftButtonDown += ListViewItem_MouseLeftButtonDown;
            stk1.Children.Add(img);
            stk1.Children.Add(lb1);


            listView1.Items.Add(stk1);
        
        }

        private void NewComponent(object sender, RoutedEventArgs e)
        {
            NewComponentWindow newcomp = new NewComponentWindow();
            newcomp.Show();
        }
      

	}
}