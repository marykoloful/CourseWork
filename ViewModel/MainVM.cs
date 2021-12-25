namespace CourseWork.ViewModel
{
    public class MainVM : BaseVM
    {
        private string wndTitle;

        public string WndTitle
        {
            get
            {
                wndTitle = AuthVM.Lang switch
                {
                    "ru-RU" => "Бронирование",
                    _ => "Reservation",
                };
                return wndTitle;
            }
        }

    }
}
