using CĐCNPM.DAO;
using CĐCNPM.Pattern.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CĐCNPM
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            if (MessageBox.Show ("Bạn có chắc chắn muốn đóng chương trình ?"
                , "Thông báo",  MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(tbUsername.Text.Equals("") || tbPassword.Text.Equals(""))
            {
                MessageBox.Show("Chưa nhập Tài khoản hoặc Mật khẩu!");
            }
            else if(tbUsername.Text.Equals("") == false|| tbPassword.Text.Equals("") == false)
            {
                Account acclogin = AccountDAO.getInstance().Get(tbUsername.Text, tbPassword.Text);
                if(acclogin == null)
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
                }
                else
                {
                    this.Hide();
                    bool isadmin = acclogin.isAdmin.Value;
                    SimpleFactory f = SimpleFactory.getInstance();
                    RoomManagerForm.setFactory(f);
                    RoomManagerForm rm = RoomManagerForm.getInstance(isadmin);
                    rm.ShowDialog();
                    this.Show();
                }
            }
        }
    }
}
