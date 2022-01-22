using System.ComponentModel.DataAnnotations;

namespace KidsVaccineReminder.Model
{
    public class UserModel
    {
        private static UserModel _instance;

        public static UserModel Instance => _instance ?? (_instance = new UserModel());

        [Key]
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
