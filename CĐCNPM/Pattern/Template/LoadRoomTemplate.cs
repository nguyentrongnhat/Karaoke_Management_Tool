using System;
using CĐCNPM.DAO;
using CĐCNPM.Pattern.Factory;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CĐCNPM.Pattern.Template
{
    public abstract class LoadRoomTemplate
    {
        public abstract List<Phong> GetRoomList();
        public void HandleRoomList(List<Phong> DanhsachPhong, Boolean isAdmin, String type)
        {
            RoomManagerForm rm = RoomManagerForm.getInstance(isAdmin);
            foreach (Phong phong in DanhsachPhong)
            {
                Button btn = new Button()
                {
                    Width = 150,
                    Height = 150
                };
                btn.Text = "Phòng: " + phong.MSPhong + Environment.NewLine + "Trạng thái: " + phong.Trangthai;

                btn.Tag = phong;

                if (phong.Trangthai.Equals("Trống"))
                {
                    btn.BackColor = Color.Wheat;
                }
                else if (phong.Trangthai.Equals("Đang dùng"))
                {
                    btn.BackColor = Color.LightPink;
                }
                if(type == "vip")
                {
                    btn.Click += rm.btnRoom_Click_Vip;
                    rm.flowLayoutPanelRoomVip.Controls.Add(btn);
                }
                else
                {
                    btn.Click += rm.btnRoom_Click;
                    rm.flowLayoutPanelRoom.Controls.Add(btn);
                }
            }
        }
        public void LoadPhong(Boolean isAdmin, String type)
        {
            HandleRoomList(GetRoomList(), isAdmin, type);
        }
    }

    public class LoadNormalRoom : LoadRoomTemplate
    {
        public override List<Phong> GetRoomList()
        { 
            List<Phong> DanhsachPhong = new List<Phong>();
            DanhsachPhong = PhongDAO.getInstance().GetPhongThuong();
            return DanhsachPhong;
        }
    }

    public class LoadVipRoom : LoadRoomTemplate
    {
        public override List<Phong> GetRoomList()
        {
            List<Phong> DanhsachPhong = new List<Phong>();
            DanhsachPhong = PhongDAO.getInstance().GetPhongVip();
            return DanhsachPhong;
        }
    }
}
