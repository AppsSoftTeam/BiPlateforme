using System.ComponentModel;


namespace TestApp.ViewModels
{
    public class UserSelectionVM
    {
        public int UserId { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public bool Selected { get; set; }
    }
}