using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CĐCNPM.DAO;
using CĐCNPM.Model;

using System.Windows.Forms;

namespace CĐCNPM.Pattern.Command
{
    public interface ICommand
    {
        void Execute(object sender);
    }

    public class Service_Start_Command : ICommand
    {
        public Control_RoomManager cr;
        public Service_Start_Command(Boolean isAdmin)
        {
            cr = Control_RoomManager.getInstance();
            cr.setRM(isAdmin);
        }
        public void Execute(object sender)
        {
            cr.Start_Service(sender);
        }
    }

    public class Service_End_Command : ICommand
    {
        public Control_RoomManager cr;
        public Service_End_Command(Boolean isAdmin)
        {
            cr = Control_RoomManager.getInstance();
            cr.setRM(isAdmin);
        }
        public void Execute(object sender)
        {
            cr.End_Service(sender);
        }
    }

    public class ServiceVip_Start_Command : ICommand
    {
        public Control_RoomManager cr;
        public ServiceVip_Start_Command(Boolean isAdmin)
        {
            cr = Control_RoomManager.getInstance();
            cr.setRM(isAdmin);
        }
        public void Execute(object sender)
        {
            //RoomManagerForm rm = RoomManagerForm.getInstance(this.rm.isAdmin);
            cr.Start_Vip_Service(sender);
        }
    }

    public class ServiceVip_End_Command : ICommand
    {
        public Control_RoomManager cr;
        public ServiceVip_End_Command (Boolean isAdmin)
        {
            cr = Control_RoomManager.getInstance();
            cr.setRM(isAdmin);
        }
        public void Execute(object sender)
        {
            cr.End_Vip_Service(sender);
        }
    }

    public class Add_Service_Command : ICommand
    {
        public Control_RoomManager cr;
        public Add_Service_Command (Boolean isAdmin)
        {
            cr = Control_RoomManager.getInstance();
            cr.setRM(isAdmin);
        }
        public void Execute(object sender)
        {
            cr.Add_Serive(sender);
        }
    }

    public class Add_Service_Vip_Command : ICommand
    {
        public Control_RoomManager cr;
        public Add_Service_Vip_Command(Boolean isAdmin)
        {
            cr = Control_RoomManager.getInstance();
            cr.setRM(isAdmin);
        }
        public void Execute(object sender)
        {
            cr.Add_Vip_Serive(sender);
        }
    }

    public class Click_Room_Command : ICommand
    {
        public Control_RoomManager cr;
        public Click_Room_Command(Boolean isAdmin)
        {
            cr = Control_RoomManager.getInstance();
            cr.setRM(isAdmin);
        }
        public void Execute(Object sender)
        {
            cr.Click_Room(sender);
        }
    }

    public class Click_Room_Vip_Command : ICommand
    {
        public Control_RoomManager cr;
        public Click_Room_Vip_Command(Boolean isAdmin)
        {
            cr = Control_RoomManager.getInstance();
            cr.setRM(isAdmin);
        }
        public void Execute(Object sender)
        {
            cr.Click_Vip_Room(sender);
        }
    }

    public class Invoker_Command
    {
        public ICommand Start;
        public ICommand End;
        public ICommand Add;
        public ICommand Click;
        public Invoker_Command (ICommand start, ICommand end, ICommand add, ICommand click)
        {
            this.Start = start;
            this.End = end;
            this.Add = add;
            this.Click = click;
        }
        public void Start_Service(object sender)
        {
            Start.Execute(sender);
        }
        public void End_Service(object sender)
        {
            End.Execute(sender);
        }
        public void Add_Service(object sender)
        {
            Add.Execute(sender);
        }
        public void Click_Room(object sender)
        {
            Click.Execute(sender);
        }
    }

    public class Control_RoomManager
    {
        private static Control_RoomManager instance = new Control_RoomManager();
        private Control_RoomManager() { }
        public static Control_RoomManager getInstance()
        {
            return instance;
        }
        public RoomManagerForm rm;
        public void setRM(Boolean isAdmin)
        {
            rm = RoomManagerForm.getInstance(isAdmin);
        }
        public void Start_Service(object sender)
        {
            //RoomManagerForm rm = RoomManagerForm.getInstance(this.rm.isAdmin);
            rm.btnStart.Enabled = false;
            rm.btnAddService.Enabled = true;
            rm.btnThanhtoan.Enabled = true;
            Phong phong = rm.btnStart.Tag as Phong;
            PhienDungPhongDAO.getInstance().Create(phong.MSPhong);
            phong.Trangthai = "Đang dùng";
            PhongDAO.getInstance().Update(phong);
            rm.flowLayoutPanelRoom.Controls.Clear();
            rm.LoadPhongThuong();

            rm.gioTimer = 0;
            rm.phutTimer = 0;
            rm.giayTimer = 0;
            rm.timer1.Start();
        }
        public void Start_Vip_Service(object sender)
        {
            //RoomManagerForm rm = RoomManagerForm.getInstance(this.rm.isAdmin);
            rm.btnStartVip.Enabled = false;
            rm.btnAddServiceVip.Enabled = true;
            rm.btnThanhtoanVip.Enabled = true;
            Phong phong = rm.btnStartVip.Tag as Phong;
            PhienDungPhongDAO.getInstance().Create(phong.MSPhong);
            phong.Trangthai = "Đang dùng";
            PhongDAO.getInstance().Update(phong);
            rm.flowLayoutPanelRoomVip.Controls.Clear();
            rm.LoadPhongVip();

            rm.gioTimer = 0;
            rm.phutTimer = 0;
            rm.giayTimer = 0;
            rm.timer2.Start();
        }
        public void End_Service(object sender)
        {
            Phong phong = rm.btnThanhtoan.Tag as Phong;
            phong.Trangthai = "Trống";
            PhongDAO.getInstance().Update(phong);

            List<PhienDungPhong> phien = PhienDungPhongDAO.getInstance().Get_Session_NotEnd(phong.MSPhong, false);

            TimeSpan Batdau = (TimeSpan)phien[0].Batdau;
            TimeSpan Ketthuc = DateTime.Now.TimeOfDay;

            float TongGiay = PhienDungPhongDAO.getInstance().Diff(Batdau, Ketthuc);

            if (TongGiay < 0)
            {
                float before = PhienDungPhongDAO.getInstance().Diff(Batdau, new TimeSpan(23, 59, 59));
                before++;
                float after = PhienDungPhongDAO.getInstance().Diff(new TimeSpan(00, 00, 00), Ketthuc);

                TongGiay = before + after;
            }

            double donvitinh = (float)phong.GiaPhong / 3600;
            double TienPhong = (donvitinh * TongGiay);
            double Tongtien = TienPhong + rm.TienDichVu;

            phien[0].Thanhtoan = true;
            phien[0].Ketthuc = Ketthuc;
            phien[0].Dongia = (int)Tongtien;

            PhienDungPhongDAO.getInstance().Update(phien[0]);
            rm.flowLayoutPanelRoom.Controls.Clear();
            rm.LoadPhongThuong();

            CultureInfo culture = new CultureInfo("vi-VN");
            rm.tbPhong.Text = TienPhong.ToString("c", culture);
            rm.tbTong.Text = Tongtien.ToString("c", culture);


            rm.btnStart.Enabled = true;
            rm.btnAddService.Enabled = false;
            rm.btnThanhtoan.Enabled = false;

            rm.timer1.Stop();
        }
        public void End_Vip_Service(object sender)
        {
            Phong phong = rm.btnThanhtoanVip.Tag as Phong;
            phong.Trangthai = "Trống";
            PhongDAO.getInstance().Update(phong);

            List<PhienDungPhong> phien = PhienDungPhongDAO.getInstance().Get_Session_NotEnd(phong.MSPhong, false);

            TimeSpan Batdau = (TimeSpan)phien[0].Batdau;
            TimeSpan Ketthuc = DateTime.Now.TimeOfDay;

            float TongGiay = PhienDungPhongDAO.getInstance().Diff(Batdau, Ketthuc);

            if (TongGiay < 0)
            {
                float before = PhienDungPhongDAO.getInstance().Diff(Batdau, new TimeSpan(23, 59, 59));
                before++;
                float after = PhienDungPhongDAO.getInstance().Diff(new TimeSpan(00, 00, 00), Ketthuc);

                TongGiay = before + after;
            }

            float donvitinh = (float)phong.GiaPhong / 3600;
            float TienPhong = (donvitinh * TongGiay);
            float Tongtien = TienPhong + rm.TienDichVu;

            phien[0].Thanhtoan = true;
            phien[0].Ketthuc = Ketthuc;
            phien[0].Dongia = (int)Tongtien;

            PhienDungPhongDAO.getInstance().Update(phien[0]);
            rm.flowLayoutPanelRoomVip.Controls.Clear();
            rm.LoadPhongVip();

            CultureInfo culture = new CultureInfo("vi-VN");
            rm.tbPhongVip.Text = TienPhong.ToString("c", culture);
            rm.tbTongVip.Text = Tongtien.ToString("c", culture);

            rm.btnStartVip.Enabled = true;
            rm.btnAddServiceVip.Enabled = false;
            rm.btnThanhtoanVip.Enabled = false;

            rm.timer2.Stop();
        }
        public void Add_Serive(object sender)
        {
            Phong phong = rm.btnAddService.Tag as Phong;
            List<PhienDungPhong> pdp = PhienDungPhongDAO.getInstance().Get_Session_NotEnd(phong.MSPhong, false);
            string TenDV = rm.cbbDichVutheoTheLoai.Text;
            int numOfService = (int)rm.nmrService.Value;
            int ID_Phien = pdp[0].ID;
            Dichvu dv = DichVuDAO.getInstance().GetbyName(TenDV);
            YC_dichvu ycdv = new YC_dichvu();
            ycdv.ID_Phien = ID_Phien;
            ycdv.MaDV = dv.MaDV;
            ycdv.Soluong = numOfService;
            //MessageBox.Show(TenDV + " " + numOfService + " " + dv.MaDV + " " + ID_Phien);
            List<Hoadon> listHD = new List<Hoadon>();
            listHD = HoaDonDAO.getInstance().get_ListBill(phong.MSPhong);
            if (rm.CheckExitsService(TenDV, listHD))
            {
                YC_dichvu ycdv_update = new YC_dichvu();
                ycdv_update = YC_DichVuDAO.getInstance().Get(ID_Phien, dv.MaDV);
                ycdv_update.Soluong += numOfService;
                if (ycdv_update.Soluong <= 0)
                {
                    YC_DichVuDAO.getInstance().Delete(ycdv_update);
                }
                else
                {
                    YC_DichVuDAO.getInstance().Update(ycdv_update);
                }

            }
            else
            {
                if (ycdv.Soluong > 0)
                {
                    YC_DichVuDAO.getInstance().Add(ycdv);
                }
            }
            rm.ShowBill(phong.MSPhong, phong.LoaiPhong);
        }
        public void Add_Vip_Serive(object sender)
        {
            Phong phong = rm.btnAddServiceVip.Tag as Phong;
            List<PhienDungPhong> pdp = PhienDungPhongDAO.getInstance().Get_Session_NotEnd(phong.MSPhong, false);
            string TenDV = rm.cbbDichVutheoTheLoaiVip.Text;
            int numOfService = (int)rm.nmrServiceVip.Value;
            int ID_Phien = pdp[0].ID;
            Dichvu dv = DichVuDAO.getInstance().GetbyName(TenDV);
            YC_dichvu ycdv = new YC_dichvu();
            ycdv.ID_Phien = ID_Phien;
            ycdv.MaDV = dv.MaDV;
            ycdv.Soluong = numOfService;
            //MessageBox.Show(TenDV + " " + numOfService + " " + dv.MaDV + " " + ID_Phien);
            List<Hoadon> listHD = new List<Hoadon>();
            listHD = HoaDonDAO.getInstance().get_ListBill(phong.MSPhong);
            if (rm.CheckExitsService(TenDV, listHD))
            {
                YC_dichvu ycdv_update = new YC_dichvu();
                ycdv_update = YC_DichVuDAO.getInstance().Get(ID_Phien, dv.MaDV);
                ycdv_update.Soluong += numOfService;
                if (ycdv_update.Soluong <= 0)
                {
                    YC_DichVuDAO.getInstance().Delete(ycdv_update);
                }
                else
                {
                    YC_DichVuDAO.getInstance().Update(ycdv_update);
                }
            }
            else
            {
                if (ycdv.Soluong > 0)
                {
                    YC_DichVuDAO.getInstance().Add(ycdv);
                }
            }
            rm.ShowBill(phong.MSPhong, phong.LoaiPhong);
        }
        public void Click_Room(Object sender)
        {
            Phong p = ((sender as Button).Tag as Phong);
            String MSPhong = ((sender as Button).Tag as Phong).MSPhong;
            String LoaiPhong = ((sender as Button).Tag as Phong).LoaiPhong;

            rm.cbbRoomLabel.Text = MSPhong;

            rm.lvBillThuong.Tag = (sender as Button).Tag;
            rm.btnStart.Tag = (sender as Button).Tag;
            rm.btnAddService.Tag = (sender as Button).Tag;
            rm.btnThanhtoan.Tag = (sender as Button).Tag;

            rm.ShowBill(MSPhong, LoaiPhong);

            int chuathanhtoan = 0;
            CultureInfo culture = new CultureInfo("vi-VN");
            rm.tbPhong.Text = chuathanhtoan.ToString("c", culture);
            rm.tbTong.Text = chuathanhtoan.ToString("c", culture);

            List<PhienDungPhong> pdp = PhienDungPhongDAO.getInstance().Get_Session_NotEnd(MSPhong, false);
            if (pdp.Count != 0)
            {
                rm.btnStart.Enabled = false;
                rm.btnThanhtoan.Enabled = true;
                rm.btnAddService.Enabled = true;

                TimeSpan Batdau = (TimeSpan)pdp[0].Batdau;
                TimeSpan Hientai = DateTime.Now.TimeOfDay;

                float TongGiay = PhienDungPhongDAO.getInstance().Diff(Batdau, Hientai);
                //MessageBox.Show("Tổng số giây cũ: " + TongGiay);

                if (TongGiay < 0)
                {
                    float before = PhienDungPhongDAO.getInstance().Diff(Batdau, new TimeSpan(23, 59, 59));
                    before++;
                    float after = PhienDungPhongDAO.getInstance().Diff(new TimeSpan(00, 00, 00), Hientai);

                    TongGiay = before + after;
                    //MessageBox.Show("Tổng số giây mới: " + TongGiay);
                }

                rm.TextClock = rm.clockText((int)TongGiay);
                rm.gioTimer = int.Parse(rm.TextClock.Substring(0, 2));
                rm.phutTimer = int.Parse(rm.TextClock.Substring(3, 2));
                rm.giayTimer = int.Parse(rm.TextClock.Substring(6, 2));
                rm.timer1.Start();
                //MessageBox.Show("TongDiff: " + TongGiay + " - TextClock: " + TextClock);

            }
            else
            {
                rm.timer1.Stop();
                rm.TextClock = "00:00:00";
                rm.clock.Text = rm.TextClock;

                rm.btnStart.Enabled = true;
                rm.btnThanhtoan.Enabled = false;
                rm.btnAddService.Enabled = false;
            }
        }
        public void Click_Vip_Room(Object sender)
        {
            String MSPhong = ((sender as Button).Tag as Phong).MSPhong;
            String LoaiPhong = ((sender as Button).Tag as Phong).LoaiPhong;

            rm.ccbRoomLabelVip.Text = MSPhong;

            rm.lvBillVip.Tag = (sender as Button).Tag;
            rm.btnStartVip.Tag = (sender as Button).Tag;
            rm.btnAddServiceVip.Tag = (sender as Button).Tag;
            rm.btnThanhtoanVip.Tag = (sender as Button).Tag;

            rm.ShowBill(MSPhong, LoaiPhong);

            int chuathanhtoan = 0;
            CultureInfo culture = new CultureInfo("vi-VN");
            rm.tbPhong.Text = chuathanhtoan.ToString("c", culture);
            rm.tbTong.Text = chuathanhtoan.ToString("c", culture);

            List<PhienDungPhong> pdp = PhienDungPhongDAO.getInstance().Get_Session_NotEnd(MSPhong, false);
            if (pdp.Count != 0)
            {
                rm.btnStartVip.Enabled = false;
                rm.btnThanhtoanVip.Enabled = true;
                rm.btnAddServiceVip.Enabled = true;

                TimeSpan Batdau = (TimeSpan)pdp[0].Batdau;
                TimeSpan Hientai = DateTime.Now.TimeOfDay;

                float TongGiay = PhienDungPhongDAO.getInstance().Diff(Batdau, Hientai);
                //MessageBox.Show("Tổng số giây cũ: " + TongGiay);

                if (TongGiay < 0)
                {
                    float before = PhienDungPhongDAO.getInstance().Diff(Batdau, new TimeSpan(23, 59, 59));
                    before++;
                    float after = PhienDungPhongDAO.getInstance().Diff(new TimeSpan(00, 00, 00), Hientai);

                    TongGiay = before + after;
                    //MessageBox.Show("Tổng số giây mới: " + TongGiay);
                }

                rm.TextClock = rm.clockText((int)TongGiay);
                rm.gioTimer = int.Parse(rm.TextClock.Substring(0, 2));
                rm.phutTimer = int.Parse(rm.TextClock.Substring(3, 2));
                rm.giayTimer = int.Parse(rm.TextClock.Substring(6, 2));
                rm.timer2.Start();
                //MessageBox.Show("TongDiff: " + TongGiay + " - TextClock: " + TextClock);

            }
            else
            {
                rm.timer2.Stop();
                rm.TextClock = "00:00:00";
                rm.clockVip.Text = rm.TextClock;

                rm.btnStartVip.Enabled = true;
                rm.btnThanhtoanVip.Enabled = false;
                rm.btnAddServiceVip.Enabled = false;
            }
        }
    }
}
