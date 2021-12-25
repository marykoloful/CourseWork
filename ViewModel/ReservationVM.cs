using CourseWork.Model;
using CourseWork.Model.Data;
using CourseWork.Model.Data.Service;
using CourseWork.Utils;
using System;
using System.Windows;

namespace CourseWork.ViewModel
{
    public class ReservationVM : BaseVM
    {
        private string nickname = AuthVM.Nickname;
        #region PROPERTIES
        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }

        private TimeSpan timeFrom = ReservationService
                                                    .GetReservationInfo(FillUtil.ROW, FillUtil.COLUMN, FillUtil.PAGE, AuthVM.Nickname)
                                                    .TimeFrom;
        public TimeSpan TimeFrom
        {
            get { return timeFrom; }
            set { timeFrom = value; NotifyPropertyChanged(nameof(TimeFrom)); }
        }

        private TimeSpan timeTo = ReservationService
                                                .GetReservationInfo(FillUtil.ROW, FillUtil.COLUMN, FillUtil.PAGE, AuthVM.Nickname)
                                                .TimeTo;
        public TimeSpan TimeTo
        {
            get { return timeTo; }
            set { timeTo = value; NotifyPropertyChanged(nameof(TimeTo)); }
        }

        private string members = ReservationService.GetReservationInfo(FillUtil.ROW, FillUtil.COLUMN, FillUtil.PAGE, AuthVM.Nickname).Members;

        public string Members
        {
            get { return members; }
            set { members = value; NotifyPropertyChanged(nameof(Members)); }
        }

        #region ADD WINDOW PROPS
        private string addWndTitle;
        public string AddWndTitle
        {
            get
            {
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        addWndTitle = "Новая бронь";
                        break;
                    case "en-US":
                        addWndTitle = "New reservation";
                        break;
                }
                return addWndTitle;
            }
        }

        private string addBtnTitle;
        public string AddBtnTitle
        {
            get
            {
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        addBtnTitle = "Добавить";
                        break;
                    case "en-US":
                        addBtnTitle = "Add";
                        break;
                }
                return addBtnTitle;
            }
        }
        #endregion


        #region EDIT WINDOW PROPS
        private string editWndTitle;
        public string EditWndTitle
        {
            get
            {
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        editWndTitle = "Изменить данные брони";
                        break;
                    case "en-US":
                        editWndTitle = "Edit reservation";
                        break;
                }
                return editWndTitle;
            }
        }
        private string saveBtnTitle;
        public string SaveBtnTitle
        {
            get
            {
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        saveBtnTitle = "Сохранить изменения";
                        break;
                    case "en-US":
                        saveBtnTitle = "Save changes";
                        break;
                }
                return saveBtnTitle;
            }
        }

        private string deleteBtnTitle;
        public string DeleteBtnTitle
        {
            get
            {
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        deleteBtnTitle = "Удалить";
                        break;
                    case "en-US":
                        deleteBtnTitle = "Delete";
                        break;
                }
                return deleteBtnTitle;
            }
        }

        #endregion


        private string timeFromTitle;
        public string TimeFromTitle
        {
            get
            {
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        timeFromTitle = "Время начала";
                        break;
                    case "en-US":
                        timeFromTitle = "Time from";
                        break;
                }
                return timeFromTitle;
            }
        }

        private string timeToTitle;
        public string TimeToTitle
        {
            get
            {
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        timeToTitle = "Время окончания";
                        break;
                    case "en-US":
                        timeToTitle = "Time to";
                        break;
                }
                return timeToTitle;
            }
        }

        private string membersTitle;
        public string MembersTitle
        {
            get
            {
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        membersTitle = "Участники";
                        break;
                    case "en-US":
                        membersTitle = "Members";
                        break;
                }
                return membersTitle;
            }
        }
        #endregion

        #region METHODS
        private void _AddNewReservation()
        {
            bool isIntersect = IsIntersect();
            if (!isIntersect)
            {
                ReservationService.CreateReservation(FillUtil.ROW,
                                                     FillUtil.COLUMN,
                                                     FillUtil.PAGE,
                                                     AuthVM.Nickname,
                                                     Members ?? "No members",
                                                     TimeFrom,
                                                     TimeTo);
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        MessageBox.Show("Бронь успешно создана. Вы можете закрыть это окно", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        break;
                    case "en-US":
                        MessageBox.Show("Reservation has been successfully created. You can close this window", "Success!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        break;
                }
            }
            else switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        MessageBox.Show("Это время занято!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case "en-US":
                        MessageBox.Show("This time already taken!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
        }

        private void _EditReservation()
        {
            bool isIntersect = IsIntersect();
            if (!isIntersect)
            {
                ReservationService.EditReservation(FillUtil.ROW,
                                         FillUtil.COLUMN,
                                         FillUtil.PAGE,
                                         AuthVM.Nickname,
                                         Members,
                                         TimeFrom,
                                         TimeTo);
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        MessageBox.Show("Бронь успешно изменена. Вы можете закрыть это окно", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        break;
                    case "en-US":
                        MessageBox.Show("Reservation has been successfully edit. You can close this window", "Success!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        break;
                }
            }
            else switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        MessageBox.Show("Это время занято!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case "en-US":
                        MessageBox.Show("This time already taken!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
        }
        public void _DeleteReservation()
        {

            MessageBoxResult result = new();
            switch (AuthVM.Lang)
            {
                case "ru-RU":
                    result = MessageBox.Show("Вы уверены?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    break;
                case "en-US":
                    result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    break;
            }
            if (result == MessageBoxResult.Yes)
            {
                ReservationService.DeleteReservation(FillUtil.ROW,
                                       FillUtil.COLUMN,
                                       FillUtil.PAGE,
                                       AuthVM.Nickname);
                switch (AuthVM.Lang)
                {
                    case "ru-RU":
                        MessageBox.Show("Бронь успешно удалена. Вы можете закрыть это окно", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        break;
                    case "en-US":
                        MessageBox.Show("Reservation has been successfully delete. You can close this window", "Success!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        break;
                }
            }
        }
        private void _OnClosed()
        {
            FillUtil.FillContentGrid(FillUtil.CONTENT_GRID, FillUtil.PAGE);
        }
        private bool IsIntersect()
        {
            bool isIntersect = false;
            foreach (Reservation reservation in ReservationService.GetAllReservations())
            {
                if ((TimeFrom >= reservation.TimeFrom && TimeFrom <= reservation.TimeTo

                    || reservation.TimeFrom >= TimeFrom && reservation.TimeFrom <= TimeTo)

                    && FillUtil.PAGE == reservation.Page && FillUtil.COLUMN == reservation.GridColumn)
                {
                    isIntersect = true;
                    break;
                }
            }

            return isIntersect;
        }
        #endregion

        #region COMMANDS

        private readonly RelayCommand addNewReservation;
        public RelayCommand AddNewReservation { get => addNewReservation ?? new(o => _AddNewReservation()); }


        private readonly RelayCommand onClosed;
        public RelayCommand OnClosed { get => onClosed ?? new(o => _OnClosed()); }


        private readonly RelayCommand editReservation;
        public RelayCommand EditReservation { get => editReservation ?? new(o => _EditReservation()); }

        private readonly RelayCommand deleteReservation;
        public RelayCommand DeleteReservation { get => deleteReservation ?? new(o => _DeleteReservation()); }
        #endregion
    }
}