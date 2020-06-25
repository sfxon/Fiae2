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

namespace Fiae2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int questionMode = 1;
        //int possibleAnswers = 8;
        //int[] answersActivated = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            int num = 0;
            
            //Frage-Modus: Multiple Choice
            if(questionMode == 1)
            {
                //Zahleneingabe über das Keypad behandeln.
                if(e.Key >= Key.NumPad1 && e.Key <= Key.NumPad8)
                {
                    num = (int)e.Key;
                    num = num - (int)Key.NumPad0;
                }

                //Zahleneingabe über die "normalen" Zahlen behandeln.
                if(e.Key >= Key.D1 && e.Key <= Key.D8)
                {
                    num = (int)e.Key;
                    num = num - (int)Key.D0;
                }

                if(num != 0)
                {
                    UpdateCheckboxByInteger(num);
                }
                
                //Einabe über Enter-Taste behandeln
                if(e.Key == Key.Enter)
                {
                    MessageBox.Show("Enter");
                }
            }
        }

        private void Window_Loaded(object Sender, RoutedEventArgs e)
        {
            List<Question> questions = SqliteDataAccess.loadQuestion(1);

            if (questions.Count == 0)
            {
                questionText.Text = "Es wurden keine Fragen gefunden";
            }
            else
            {
                Question firstQuestion = questions.First();
                questionText.Text = firstQuestion.question_text;
            }

        }

        private void UpdateCheckboxByInteger(int num)
        {
            System.Windows.Controls.CheckBox answer = this.FindName("answer" + num) as CheckBox;
            answer.IsChecked = !answer.IsChecked;   //Set from checked to unchecked
        }
    }
}
