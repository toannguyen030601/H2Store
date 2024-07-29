using System.Net.Mail;
using System.Net;
using System.Text;

namespace asm1.Models
{
    public class SendMail
    {
        public void Send(string email, string matkhau, bool isMatkhau = false)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential cred = new NetworkCredential("toannguyen0251@gmail.com", "olik tgqv sjiv yzcr");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("toannguyen0251@gmail.com");
            mailMessage.To.Add(email);
            if (isMatkhau)
            {
                mailMessage.Subject = "Bạn đã sử dụng tính năng quên mật khẩu";
                mailMessage.Body = "Mật khẩu mới là " + matkhau;
            }
            else
            {
                mailMessage.Subject = "Thông tin đăng nhập!";
                mailMessage.Body = "Mật khẩu của bạn là " + matkhau;
            }

            smtp.Credentials = cred;

            smtp.EnableSsl = true;

            smtp.Send(mailMessage);
        }

        //mk ramdom
        public string getPassword()
        {
            Random r = new Random();
            StringBuilder builder = new StringBuilder();
            builder.Append(randomString(4, true));
            builder.Append(r.Next(1000, 9999));
            builder.Append(randomString(2, false));
            return builder.ToString();

        }
        private string randomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random r = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * r.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
            {
                return builder.ToString().ToUpper();

            }
            else return builder.ToString().ToLower();
        }
    }
}
