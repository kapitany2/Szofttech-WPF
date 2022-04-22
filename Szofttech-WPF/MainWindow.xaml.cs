﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Szofttech_WPF.Interfaces;
using Szofttech_WPF.Network;
using Szofttech_WPF.Utils;
using Szofttech_WPF.View;
using Szofttech_WPF.ViewModel;

namespace Szofttech_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MenuGUI menuGUI;
        private JoinGUI joinGUI;
        private SettingsGUI settingsGUI;
        private ChatGUI chatGUI;
        private GameGUI gameGUI;
        public MainWindow()
        {
            InitializeComponent();

            backButton.Click += (send, args) =>
            {
                var c = windowGrid.Children.Cast<UIElement>().Where(a => Grid.GetRow(a) == 1 && a.Visibility == Visibility.Visible).OfType<IExitableGUI>().FirstOrDefault();

                if (c != null)
                    c.CloseGUI();
                else
                    Console.WriteLine("nem tudtam bezárni, mivel null-t kaptam.");
            };

            #region joinGUI init
            joinGUI = new JoinGUI();
            joinGUI.Visibility = Visibility.Hidden;
            joinGUI.IsVisibleChanged += (send, args) =>
            {
                setHeaderBarButtons(joinGUI.IsVisible ? Visibility.Hidden : Visibility.Visible);
                menuGUI.Visibility = joinGUI.IsVisible ? Visibility.Hidden : Visibility.Visible;
            };
            (joinGUI.DataContext as JoinGameGUIViewModel).OnConnect += (sender, args) =>
            {
                CreateGameGUI(args.ServerAddress);
            };
            windowGrid.Children.Add(joinGUI);
            Grid.SetRow(joinGUI, 1);
            #endregion

            #region settingsGUI init
            settingsGUI = new SettingsGUI();
            settingsGUI.Visibility = Visibility.Hidden;
            settingsGUI.IsVisibleChanged += (send, args) =>
            {
                setHeaderBarButtons(settingsGUI.IsVisible ? Visibility.Hidden : Visibility.Visible);
                menuGUI.Visibility = settingsGUI.IsVisible ? Visibility.Hidden : Visibility.Visible;
            };
            windowGrid.Children.Add(settingsGUI);
            Grid.SetRow(settingsGUI, 1);
            #endregion

            chatGUI = new ChatGUI();
            chatGUI.Visibility = Visibility.Hidden;
            chatGUI.IsVisibleChanged += (send, args) =>
            {
                setHeaderBarButtons(chatGUI.IsVisible ? Visibility.Hidden : Visibility.Visible);
                menuGUI.Visibility = chatGUI.IsVisible ? Visibility.Hidden : Visibility.Visible;
            };
            windowGrid.Children.Add(chatGUI);
            Grid.SetRow(chatGUI, 1);


            #region Menu init
            menuGUI = new MenuGUI();
            menuGUI.IsVisibleChanged += (send, args) =>
            {
                setHeaderBarButtons(menuGUI.IsVisible ? Visibility.Hidden : Visibility.Visible);
            };
            menuGUI.bttnNewGame.Click += (send, args) =>
            {
                menuGUI.Visibility = Visibility.Hidden;
                CreateGameGUI(null);
            };
            menuGUI.bttnJoinGame.Click += (send, args) =>
            {
                joinGUI.Visibility = Visibility.Visible;
                menuGUI.Visibility = Visibility.Hidden;
            };
            menuGUI.bttnSettings.Click += (send, args) =>
            {
                settingsGUI.Visibility = Visibility.Visible;
                menuGUI.Visibility = Visibility.Hidden;
            };
            menuGUI.bttnExit.Click += (send, args) =>
            {
                Environment.Exit(0);
            };
            windowGrid.Children.Add(menuGUI);
            Grid.SetRow(menuGUI, 1);
            #endregion

            CheckDefaultFiles();
        }

        private void CheckDefaultFiles()
        {
            string[] files = { "/explosion.wav", "/splash.m4a", "/backgroundwind.mp3" };
            foreach (var file in files)
                if (!File.Exists(Settings.GetPath() + file))
                    CreateDefaultFiles(file);
        }
        private void CreateDefaultFiles(string filename)
        {
            try
            {
                if (!Directory.Exists(Settings.GetPath()))
                    Directory.CreateDirectory(Settings.GetPath());
            }
            catch { }
            try
            {
                using (FileStream fileStream = File.Create(Settings.GetPath() + filename))
                {
                    var a = Assembly.GetExecutingAssembly();
                    var y = "Szofttech_WPF.View.Resources." + filename.Substring(1);
                    var b = a.GetManifestResourceStream(y);
                    b.CopyTo(fileStream);
                }
            }
            catch { }
        }

        private void CreateGameGUI(ServerAddress sa)
        {
            if (sa != null)
            {
                joinGUI.Visibility = Visibility.Hidden;
                gameGUI = new GameGUI(sa.IP, sa.Port);
            }
            else
            {
                gameGUI = new GameGUI(Settings.getPort());
            }
            gameGUI.IsVisibleChanged += (send, args) =>
            {
                if (gameGUI.IsVisible)
                {
                    menuGUI.Visibility = Visibility.Hidden;
                }
                else
                {
                    menuGUI.Visibility = Visibility.Visible;
                    windowGrid.Children.Remove(gameGUI);
                    gameGUI = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            };
            windowGrid.Children.Add(gameGUI);
            Grid.SetRow(gameGUI, 1);
        }

        private void setHeaderBarButtons(Visibility visibility)
        {
            exitButton.Visibility = visibility;
            backButton.Visibility = visibility;
        }

        private void drawWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            var c = windowGrid.Children.Cast<UIElement>().Where(a => Grid.GetRow(a) == 1 && a.Visibility == Visibility.Visible).OfType<IExitableGUI>().FirstOrDefault();

            if (c != null)
            {
                if (c.ExitApplication())
                    Environment.Exit(0);
            }
            else
                Console.WriteLine("nem tudtam bezárni, mivel null-t kaptam.");
        }
    }
}
