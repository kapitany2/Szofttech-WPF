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
using Szofttech_WPF.Interfaces;
using Szofttech_WPF.Network;
using Szofttech_WPF.Utils;
using Szofttech_WPF.View;

namespace Szofttech_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MenuGUI menu;
        public MainWindow()
        {
            InitializeComponent();
            Settings settings = Settings.getInstance();
            TESZTVILI();

            backButton.Click += (send, args) =>
            {
                Console.WriteLine("gomb megnyomva");
                var c = windowGrid.Children.Cast<UIElement>().Where(a => Grid.GetRow(a) == 1 && a.Visibility == Visibility.Visible).OfType<IExitableGUI>().FirstOrDefault();
                Console.WriteLine(c);
                c.CloseGUI();
            };

            menu = new MenuGUI();
            menu.IsVisibleChanged += (send, args) =>
            {
                exitButton.Visibility = menu.IsVisible ? Visibility.Hidden : Visibility.Visible;
                backButton.Visibility = menu.IsVisible ? Visibility.Hidden : Visibility.Visible;
            };
            menu.bttnNewGame.Click += (send, args) =>
            {
                menu.Visibility = Visibility.Hidden;
                CreateGameGUI(null);
            };
            menu.bttnJoinGame.Click += (send, args) =>
            {

                menu.Visibility = Visibility.Hidden;
            };
            menu.bttnSettings.Click += (send, args) =>
            {

                menu.Visibility = Visibility.Hidden;
            };
            menu.bttnExit.Click += (send, args) =>
            {
                Environment.Exit(0);
            };
            windowGrid.Children.Add(menu);
            Grid.SetRow(menu, 1);
        }

        private void CreateGameGUI(ServerAddress sa)
        {
            GameGUI gameGUI;
            if (sa != null)
            {
                //gameGUI = new GameGUI(sa.IP, sa.Port);
                gameGUI = new GameGUI();
            }
            else
            {
                gameGUI = new GameGUI();
            }
            gameGUI.IsVisibleChanged += (send, args) =>
            {
                if (gameGUI.IsVisible)
                {
                    menu.Visibility = Visibility.Hidden;
                }
                else
                {
                    menu.Visibility = Visibility.Visible;
                    windowGrid.Children.Remove(gameGUI);
                }
            };
            windowGrid.Children.Add(gameGUI);
            Grid.SetRow(gameGUI, 1);
        }

        private void drawWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TESZTVILI()
        {
            
            Console.WriteLine(Settings.port);
            Server server = new Server(25564);
        }
    }
}
