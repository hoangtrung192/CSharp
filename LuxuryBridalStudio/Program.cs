using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                var setting = ConfigurationManager.ConnectionStrings["LuxuryBridalStudio"];
                if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                    throw new InvalidOperationException("Thiếu kết nối LuxuryBridalStudio trong App.config.");
                var accounts = new SqlAccountService(setting.ConnectionString);
                if (!accounts.HasAccounts)
                    using (var setup = new frmInitialSetup(accounts))
                        if (setup.ShowDialog() != DialogResult.OK) return;
                AppSession.Accounts = accounts;
                AppSession.Data = new SqlStudioStore(setting.ConnectionString).Load();
                Application.Run(new frmDangNhap());
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + "\n\nKiểm tra SQL Server, App.config và hướng dẫn Database/README.md.",
                    "Không thể khởi động", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
