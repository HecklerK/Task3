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
using System.IO;

namespace Task3
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


        public class info
        {
            public double index { get; set; }
            public string ru_text { get; set; }
            public string en_text { get; set; }
        }

        public string[] readText(string NameFile)
        {
            using (FileStream fstream = new FileStream(NameFile, FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textF = Encoding.Default.GetString(array);
                int c = 0;
                string[] text = new string[1];
                foreach (var item in textF)
                {
                    if (item == '\n') c++;
                    Array.Resize(ref text, c + 1);
                    text[c] += item;
                }
                return text;
            }
        }


        public double GetIndex(string text)
        {
            bool comment = false;
            double index = 0;
            double Lindex = 0.5;
            int c = 0;
            double indexComment = 0;
            double LindexComment = 0.5;
            int cComment = 0;
            foreach (var item in text)
            {
                if (char.IsLetterOrDigit(item) && !comment)
                {
                    index += Lindex;
                    Lindex += 1;
                    c++;
                }
                else if (char.IsLetterOrDigit(item) && comment)
                {
                    indexComment += LindexComment;
                    LindexComment += 1;
                    cComment++;
                }
                if (item == '|')
                {
                    comment = true;
                }
            }
            if (cComment == 0) return index * c;
            else return index * c + indexComment * cComment;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] ru_text = readText("russian.txt");
            string[] en_text = readText("english.txt");
            foreach (var item in ru_text)
            {
                double i = GetIndex(item);
                string en_t = null;
                foreach (var en in en_text)
                {
                    if (i == GetIndex(en)) en_t = en;
                }
                if (en_t == null)
                {
                    info t = new info
                    {
                        index = i,
                        ru_text = item
                    };
                    list.Items.Add(t);
                }
                else
                {
                    info t = new info
                    {
                        index = i,
                        ru_text = item,
                        en_text = en_t
                    };
                    list.Items.Add(t);
                }
            }
            foreach (var item in en_text)
            {
                info t = new info
                {
                    index = GetIndex(item),
                    en_text = item
                };
                list2.Items.Add(t);
            }
        }
    }
}
