using System;
using System.Windows;
using System.Windows.Controls;
using Szofttech_WPF.DataPackage;
using Szofttech_WPF.EventArguments.Board;
using Szofttech_WPF.EventArguments.ShipSelecter;
using Szofttech_WPF.Interfaces;
using Szofttech_WPF.Logic;
using Szofttech_WPF.Network;
using Szofttech_WPF.Utils;
using Szofttech_WPF.View.Game;
using Szofttech_WPF.ViewModel;

namespace Szofttech_WPF.View
{
    /// <summary>
    /// Interaction logic for GameGUI.xaml
    /// </summary>
    public partial class GameGUI : UserControl, IExitableGUI
    {
        PlayerBoardGUI playerBoardGUI;
        EnemyBoardGUI enemyBoardGUI;
        private ShipSelecterGUI selecter;
        private ChatGUI chatGUI;
        private InfoPanelGUI infoPanel;
        private Client Client;
        private Server Server;
        private ChatViewModel ChatViewModel;

        public GameGUI(int port) : this(Settings.getIP(), port)
        {
            Server = new Server(Settings.getPort());
        }

        public GameGUI(string ip, int port)
        {
            InitializeComponent();

            playerBoardGUI = new PlayerBoardGUI();
            enemyBoardGUI = new EnemyBoardGUI();
            selecter = new ShipSelecterGUI();
            ChatViewModel = new ChatViewModel();
            chatGUI = new ChatGUI();

            Client = new Client(ip, port);
            Client.OnMessageReceived += Client_OnMessageReceived;
            Client.OnYourTurn += Client_OnYourTurn;
            Client.OnGameEnded += Client_OnGameEnded;
            Client.OnEnemyHitMe += Client_OnEnemyHitMe;
            Client.OnMyHit += Client_OnMyHit;
            Client.OnJoinedEnemy += Client_OnJoinedEnemy;

            playerBoardGUI.OnPlace += PlayerBoardGUI_OnPlace;
            playerBoardGUI.OnPickUp += PlayerBoardGUI_OnPickUp;
            grid.Children.Add(playerBoardGUI);
            Grid.SetRow(playerBoardGUI, 3);
            Grid.SetColumn(playerBoardGUI, 1);
            grid.Children.Add(enemyBoardGUI);
            Grid.SetRow(enemyBoardGUI, 3);
            Grid.SetColumn(enemyBoardGUI, 5);

            selecter.OnSelectShip += Selecter_OnSelectShip;
            selecter.OnSelectDirection += Selecter_OnSelectDirection;
            selecter.OnClearBoard += Selecter_OnClearBoard;
            selecter.OnPlaceRandomShips += Selecter_OnPlaceRandomShips;
            selecter.OnRanOutOfShips += Selecter_OnRanOutOfShips;
            selecter.OnDone += Selecter_OnDone;
            grid.Children.Add(selecter);
            Grid.SetRow(selecter, 1);
            Grid.SetRowSpan(selecter, 3);
            Grid.SetColumn(selecter, 1);
            Grid.SetColumnSpan(selecter, 5);

            chatGUI.DataContext = ChatViewModel;
            chatGUI.Visibility = Visibility.Hidden;
            grid.Children.Add(chatGUI);
            Grid.SetRow(chatGUI, 1);
            Grid.SetRowSpan(chatGUI, 3);
            Grid.SetColumn(chatGUI, 1);
            Grid.SetColumnSpan(chatGUI, 5);
        }

        private void Client_OnJoinedEnemy(object sender, EventArgs e)
        {
            //waitingTitle.Visibility = Visibility.Hidden;
            playerBoardGUI.Visibility = Visibility.Visible;
            enemyBoardGUI.Visibility = Visibility.Visible;
            selecter.Visibility = Visibility.Visible;
        }

        private void Client_OnMyHit(object sender, EventArguments.Chat.MyHitArgs e)
        {
            enemyBoardGUI.Hit(e.I, e.J, e.Status);
        }

        private void Client_OnEnemyHitMe(object sender, EventArguments.Chat.EnemyHitMeArgs e)
        {
            playerBoardGUI.Hit(e.I, e.J);
        }

        private void Client_OnGameEnded(object sender, EventArguments.Chat.GameEndedArgs e)
        {
            enemyBoardGUI.setTurnEnabled(false);
            string endMessage = "";
            switch (e.GameEndedStatus)
            {
                case GameEndedStatus.Unknown:
                    endMessage = "Unknown game ended status.";
                    break;
                case GameEndedStatus.Defeat:
                    endMessage = "Defeat!";
                    break;
                case GameEndedStatus.Win:
                    endMessage = "You win!";
                    break;
                default:
                    break;
            }
            chatGUI.AddMessage("System", endMessage);
        }

        private void Client_OnYourTurn(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Client_OnMessageReceived(object sender, EventArguments.Chat.MessageReveivedArgs e)
        {
            throw new NotImplementedException();
        }

        private void PlayerBoardGUI_OnPickUp(object sender, ShipSizeArgs e)
        {
            selecter.PickupFromTable(e.ShipSize);
            playerBoardGUI.canPlace = true;
            selecter.CanDone(false);
        }

        private void PlayerBoardGUI_OnPlace(object sender, ShipSizeArgs e)
        {
            selecter.PlaceToTable(e.ShipSize);
        }

        private void Selecter_OnDone(object sender, EventArgs e)
        {
            playerBoardGUI.IsEnabled = false;
            chatGUI.Visibility = Visibility.Visible;
            Client.sendMessage(new PlaceShipsData(Client.ID, playerBoardGUI.board));
        }

        private void Selecter_OnRanOutOfShips(object sender, EventArgs e)
        {
            playerBoardGUI.canPlace = false;
            selecter.CanDone(true);
        }

        private void Selecter_OnPlaceRandomShips(object sender, EventArgs e)
        {
            playerBoardGUI.canPlace = false;
            playerBoardGUI.RandomPlace();
            selecter.CanDone(true);
        }

        private void Selecter_OnClearBoard(object sender, EventArgs e)
        {
            playerBoardGUI.ClearBoard();
            playerBoardGUI.canPlace = true;
            selecter.CanDone(false);
        }

        private void Selecter_OnSelectDirection(object sender, SelectShipDirectionArgs e)
        {
            playerBoardGUI.shipPlaceHorizontal = e.ShipPlaceHorizontal;
        }

        private void Selecter_OnSelectShip(object sender, SelectShipArgs e)
        {
            playerBoardGUI.selectedShipSize = e.ShipSize;
        }

        public void CloseGUI()
        {
            this.Visibility = Visibility.Hidden;
        }

        public void ExitApplication()
        {
            //Check if exitable
        }
    }
}
