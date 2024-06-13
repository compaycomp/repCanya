using System;
using System.IO;
using System.Windows.Forms;

namespace Cursach
{
    public static class UserManager
    {
        private const string UsersFilePath = "C:\\Users\\CompayComp\\source\\repos\\Cursach — копия\\Cursach\\Resources\\users.txt";

        public static void RegisterUser(string login, string password, int easyTime = 0, int mediumTime = 0, int hardTime = 0)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(UsersFilePath, true))
                {
                    writer.WriteLine($"{login},{password}, {easyTime}, {mediumTime}, {hardTime}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании пользователя: " + ex.Message);
            }
        }
    }
}
