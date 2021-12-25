using CourseWork.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CourseWork.View;
using CourseWork.Model.Data.Service;
using System.Globalization;
using System.Threading;

namespace CourseWork.Utils
{
    public class FillUtil
    {
        private static AuthVM authVM = new();

        private readonly static DateTime TODAY = DateTime.Now;

        /// <summary>
        /// Pointer to current content grid
        /// </summary>
        public static Grid CONTENT_GRID;
        /// <summary>
        /// Pointer to current page
        /// </summary>
        public static string PAGE;

        public static int COLUMN, ROW;
        public static void FillContentGrid(Grid contentGrid, string page)
        {
            CONTENT_GRID = contentGrid;
            UIElement ui = new Button();
            for (int row = 0; row < 24; row++)
            {
                PAGE = page;
                for (int column = 0; column < 7; column++)
                {
                    var dayOfWeek = TODAY.AddDays(column).DayOfWeek;

                    if (ReservationService.isReservationExist(row, column, page, AuthVM.Nickname))
                    {
                        ui = new Button() { Content = ReservationService.GetReservationInfo(row, column, page, AuthVM.Nickname).Members, FontSize = 7 };
                        ((Button)ui).Click += OpenEditReservationWnd;
                    }
                    else
                    {
                        ui = new Button() { Content = "+" };
                        ((Button)ui).Click += OpenNewReservationWnd;
                    }
                    contentGrid.Children.Add(ui);
                    Grid.SetColumn(ui, column);
                    Grid.SetRow(ui, row);

                }
            }
        }

        public static void FillDaysOfWeek(UniformGrid contentHeaderGrid, int from, int to)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(AuthVM.Lang);
            for (int i = from; i < to; i++)
            {
                StackPanel stackPanel = new() { Orientation = Orientation.Vertical, Margin = new Thickness(10, 0, 0, 0) };
                stackPanel.Children.Add(new TextBlock() { Text = TODAY.AddDays(i).ToString("dddd", CultureInfo.GetCultureInfo(AuthVM.Lang)) });
                stackPanel.Children.Add(new Label() { Content = DateTime.Today.AddDays(i).ToString("d"), FontWeight = FontWeights.Bold, FontSize = 7 });
                contentHeaderGrid.Children.Add(stackPanel);
            }
        }
        public static void FillHoursStackPanel(StackPanel stackPanel)
        {
            for (int i = 0; i < 25; i++)
            {
                TextBlock tb = new()
                {
                    FontSize = 9,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 6)
                };
                switch (i)
                {
                    case < 10:
                        tb.Text = $"00:0{i}";
                        break;
                    case >= 10:
                        tb.Text = $"00:{i}";
                        break;
                    default:
                }
                stackPanel.Children.Add(tb);
            }

        }
        private static void OpenNewReservationWnd(object sender, RoutedEventArgs e)
        {
            COLUMN = Grid.GetColumn(sender as Button);
            ROW = Grid.GetRow(sender as Button);

            AddNewReservationWindow newReservationWindow = new();
            newReservationWindow.ShowDialog();
        }
        private static void OpenEditReservationWnd(object sender, RoutedEventArgs e)
        {
            COLUMN = Grid.GetColumn(sender as Button);
            ROW = Grid.GetRow(sender as Button);

            EditReservationWindow editReservationWindow = new();
            editReservationWindow.ShowDialog();
        }
    }
}
