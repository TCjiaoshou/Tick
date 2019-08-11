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

namespace Tick
{
    /// <summary>
    /// DigitalTubeControl.xaml 的交互逻辑
    /// </summary>
    public partial class DigitalTubeControl : UserControl
    {
        private List<Rectangle> rectanglesDigits = new List<Rectangle>(7);
        private List<Rectangle> rectanglesTenDigits = new List<Rectangle>(7);

        private void digitsShow(List<Rectangle> rectangles, int number)
        {
            //Rect01.Visibility = Visibility.Hidden;
            //Rect02.Visibility = Visibility.Hidden;
            //Rect03.Visibility = Visibility.Hidden;
            //Rect04.Visibility = Visibility.Hidden;
            //Rect05.Visibility = Visibility.Hidden;
            //Rect06.Visibility = Visibility.Hidden;
            foreach (Rectangle rect in rectangles)
            {
                rect.Visibility = Visibility.Hidden;
            }
            if(!showTenDigiteZero && rectangles.Equals(rectanglesTenDigits) && number == 0)
            {
                return;
            }
            switch (number)
            {
                case 1:
                    rectangles[1].Visibility = Visibility.Visible;
                    rectangles[3].Visibility = Visibility.Visible;
                    break;
                case 2:
                    rectangles[0].Visibility = Visibility.Visible;
                    rectangles[1].Visibility = Visibility.Visible;
                    rectangles[2].Visibility = Visibility.Visible;
                    rectangles[5].Visibility = Visibility.Visible;
                    rectangles[4].Visibility = Visibility.Visible;
                    break;
                case 3:
                    rectangles[0].Visibility = Visibility.Visible;
                    rectangles[1].Visibility = Visibility.Visible;
                    rectangles[2].Visibility = Visibility.Visible;
                    rectangles[3].Visibility = Visibility.Visible;
                    rectangles[4].Visibility = Visibility.Visible;
                    break;
                case 4:
                    rectangles[6].Visibility = Visibility.Visible;
                    rectangles[2].Visibility = Visibility.Visible;
                    rectangles[1].Visibility = Visibility.Visible;
                    rectangles[3].Visibility = Visibility.Visible;
                    break;
                case 5:
                    rectangles[0].Visibility = Visibility.Visible;
                    rectangles[6].Visibility = Visibility.Visible;
                    rectangles[2].Visibility = Visibility.Visible;
                    rectangles[3].Visibility = Visibility.Visible;
                    rectangles[4].Visibility = Visibility.Visible;
                    break;
                case 6:
                    rectangles[0].Visibility = Visibility.Visible;
                    rectangles[6].Visibility = Visibility.Visible;
                    rectangles[2].Visibility = Visibility.Visible;
                    rectangles[5].Visibility = Visibility.Visible;
                    rectangles[3].Visibility = Visibility.Visible;
                    rectangles[4].Visibility = Visibility.Visible;
                    break;
                case 7:
                    rectangles[0].Visibility = Visibility.Visible;
                    rectangles[1].Visibility = Visibility.Visible;
                    rectangles[3].Visibility = Visibility.Visible;
                    break;
                case 8:
                    rectangles[0].Visibility = Visibility.Visible;
                    rectangles[1].Visibility = Visibility.Visible;
                    rectangles[2].Visibility = Visibility.Visible;
                    rectangles[3].Visibility = Visibility.Visible;
                    rectangles[4].Visibility = Visibility.Visible;
                    rectangles[5].Visibility = Visibility.Visible;
                    rectangles[6].Visibility = Visibility.Visible;
                    break;
                case 9:
                    rectangles[0].Visibility = Visibility.Visible;
                    rectangles[6].Visibility = Visibility.Visible;
                    rectangles[1].Visibility = Visibility.Visible;
                    rectangles[2].Visibility = Visibility.Visible;
                    rectangles[3].Visibility = Visibility.Visible;
                    rectangles[4].Visibility = Visibility.Visible;
                    break;
                case 0:
                    rectangles[0].Visibility = Visibility.Visible;
                    rectangles[6].Visibility = Visibility.Visible;
                    rectangles[1].Visibility = Visibility.Visible;
                    rectangles[5].Visibility = Visibility.Visible;
                    rectangles[3].Visibility = Visibility.Visible;
                    rectangles[4].Visibility = Visibility.Visible;
                    break;
                default:
                    throw new Exception("Errer:Digite Show Exception");
            }

        }
        
        public DigitalTubeControl()
        {
            InitializeComponent();

            rectanglesDigits.Add(Rect10);
            rectanglesDigits.Add(Rect11);
            rectanglesDigits.Add(Rect12);
            rectanglesDigits.Add(Rect13);
            rectanglesDigits.Add(Rect14);
            rectanglesDigits.Add(Rect15);
            rectanglesDigits.Add(Rect16);

            rectanglesTenDigits.Add(Rect00);
            rectanglesTenDigits.Add(Rect01);
            rectanglesTenDigits.Add(Rect02);
            rectanglesTenDigits.Add(Rect03);
            rectanglesTenDigits.Add(Rect04);
            rectanglesTenDigits.Add(Rect05);
            rectanglesTenDigits.Add(Rect06);
        }

        private int number; 
        public int Value
        {
            get => number;
            set
            {
                if (value >= 0 && value < 100)
                {
                    number = value;
                    digitsShow(rectanglesTenDigits, value / 10);
                    digitsShow(rectanglesDigits, value % 10);
                }
                else
                {
                    throw new Exception("Errer:Value Assignment Exception, Range: 0 ~ 100");
                }
            }
        }

        new public Color Foreground
        {
            set
            {
                fun(rectanglesDigits);
                fun(rectanglesTenDigits);
                void fun (List<Rectangle> rects)
                {
                    foreach(Rectangle rect in rects)
                    {
                        rect.Fill = new SolidColorBrush(value);
                    }
                }
            }
        }
        new public Color Background
        {
            set => canvasBack.Background = new SolidColorBrush(value);
        }

        private bool showTenDigiteZero = true;
        public bool isShowTenDigiteZero
        {
            get => showTenDigiteZero;
            set
            {
                showTenDigiteZero = value;
            }
        }
    }
}
