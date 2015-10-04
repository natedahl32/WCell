using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WCell.CreateBotAccountsSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sw = new StreamWriter(@"c:\temp\CreateAccountsSQL.sql"))
            {
                for (var i = 1; i < 2000; i++)
                {
                    var accountName = "BOT" + i.ToString();
                    sw.WriteLine(string.Format("INSERT wcellauthserver.account (Created, Name, Password, ClientId, RoleGroupName, IsActive, HighestCharLevel, Locale) SELECT CURDATE(), '{0}', Password, 2, 'Player', IsActive, 0, 'English' FROM account WHERE AccountId = 1;", accountName.ToUpper()));
                }
            }
        }
    }
}
