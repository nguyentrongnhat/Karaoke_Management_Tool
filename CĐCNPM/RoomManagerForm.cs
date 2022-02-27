using CĐCNPM.DAO;
using CĐCNPM.Pattern.Factory;
using CĐCNPM.Pattern.Template;
using CĐCNPM.Pattern.Builder;
using CĐCNPM.Pattern.Command;
using CĐCNPM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CĐCNPM
{
    public partial class RoomManagerForm : Form
    {
        public float TienDichVu;
        public string TextClock = "00:00:00";
        public int gioTimer = 0;
        public int phutTimer = 0;
        public int giayTimer = 0;
        public bool isAdmin;

        public static SimpleFactory factory;
        public static RoomManagerForm instanceAdmin = new RoomManagerForm(true);
        public static RoomManagerForm instanceNV = new RoomManagerForm(false);
        public static void setFactory(SimpleFactory f)
        {
            factory = f;
        }
        public static RoomManagerForm getInstance(Boolean m)
        {
            return factory.createRoomManager(m);
        }
        public static RoomManagerForm getInstanceAdmin()
        {   
            return instanceAdmin;
        }
        public static RoomManagerForm getInstanceNV()
        {
            return instanceNV;
        }
        public RoomManagerForm(Boolean mess)
        {
            InitializeComponent();
            isAdmin = mess;
        }

        private void RoomManagerForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanelRoom.Controls.Clear();
            flowLayoutPanelRoomVip.Controls.Clear();
            LoadPhongThuong();
            LoadPhongVip();
            LoadCategory();

            if (isAdmin == false)
            {
                tsmManager.Enabled = false;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminForm admin = AdminForm.getInstance();
            admin.ShowDialog();
            this.Show();
        }

        #region ShowBill
        public void ShowBill(String MSPhong, String LoaiPhong)
        {
            TienDichVu = 0;
            List<Hoadon> listHD = new List<Hoadon>();
            listHD = HoaDonDAO.getInstance().get_ListBill(MSPhong);

            lvBillThuong.Items.Clear();
            lvBillVip.Items.Clear();

            float tongtien = 0;

            foreach (Hoadon hd in listHD)
            {
                hd.dongia = hd.GiaDV * hd.SoLuong;
                tongtien += hd.dongia;
                TienDichVu = tongtien;
                ListViewItem cl1 = new ListViewItem(hd.LoaiDV.ToString());
                cl1.SubItems.Add(hd.TenDV.ToString());
                cl1.SubItems.Add(hd.GiaDV.ToString());
                cl1.SubItems.Add(hd.SoLuong.ToString());
                cl1.SubItems.Add(hd.dongia.ToString());
                if (LoaiPhong.Equals("Thường"))
                {
                    lvBillThuong.Items.Add(cl1);
                }
                else
                {
                    lvBillVip.Items.Add(cl1);
                }             
            }
            if (tongtien >= 0)
            {
                if (LoaiPhong.Equals("Thường"))
                {
                    CultureInfo culture = new CultureInfo("vi-VN");
                    tbDichvu.Text = tongtien.ToString("c", culture);
                }
                else
                {
                    CultureInfo culture = new CultureInfo("vi-VN");
                    tbDichvuVip.Text = tongtien.ToString("c", culture);
                }
                
            }
        }
        #endregion ShowBill

        #region loadroom
        public void LoadPhongThuong()
        {
            LoadRoomTemplate template = new LoadNormalRoom();
            template.LoadPhong(isAdmin, "normal");
        }

        public void LoadPhongVip()
        {
            LoadRoomTemplate template = new LoadVipRoom();
            template.LoadPhong(isAdmin, "vip");
        }


        #endregion loadroom 

        #region loadcategory and service by category

        #region Thuong
        void LoadCategory()
        {
            List<LoaiDichVu> list = new List<LoaiDichVu>();
            list = DichVuDAO.getInstance().GetCategory();

            cbbCategoryService.DataSource = list;
            cbbCategoryService.DisplayMember = "Loai";

            cbbCategoryServiceVip.DataSource = list;
            cbbCategoryServiceVip.DisplayMember = "Loai";

            btnThanhtoan.Enabled = false;
            btnStart.Enabled = false;
            btnAddService.Enabled = false;

            btnThanhtoanVip.Enabled = false;
            btnStartVip.Enabled = false;         
            btnAddServiceVip.Enabled = false;
        }

        public void LoadDetailService(string loai)
        {
            List<DichVutheoTheLoai> list = new List<DichVutheoTheLoai>();
            list = DichVuDAO.getInstance().GetServicebyCategory(loai);
            cbbDichVutheoTheLoai.DataSource = list;
            cbbDichVutheoTheLoai.DisplayMember = "Ten";

            cbbDichVutheoTheLoaiVip.DataSource = list;
            cbbDichVutheoTheLoaiVip.DisplayMember = "Ten";
        }

        private void cbbCategoryService_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loai = "";
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
            {
                return;
            }
            LoaiDichVu ldv = cb.SelectedItem as LoaiDichVu;
            loai = ldv.Loai;
            LoadDetailService(loai);
        }
        #endregion Thuong

        #region Vip

        //void LoadCategoryVip()
        //{
        //    List<LoaiDichVu> list = new List<LoaiDichVu>();
        //    list = DichVuDAO.GetCategory();
        //    cbbCategoryServiceVip.DataSource = list;
        //    cbbCategoryServiceVip.DisplayMember = "Loai";
        //    btnThanhtoanVip.Enabled = false;
        //    btnStartVip.Enabled = false;
        //    btnAddServiceVip.Enabled = false;
        //}

        //public void LoadDetailServiceVip(string loai)
        //{
        //    List<DichVutheoTheLoai> list = new List<DichVutheoTheLoai>();
        //    list = DichVuDAO.GetServicebyCategory(loai);
        //    cbbDichVutheoTheLoaiVip.DataSource = list;
        //    cbbDichVutheoTheLoaiVip.DisplayMember = "Ten";
        //}

        //private void cbbCategoryService_SelectedIndexChangedVip(object sender, EventArgs e)
        //{
        //    string loai = "";
        //    ComboBox cb = sender as ComboBox;

        //    if (cb.SelectedItem == null)
        //    {
        //        return;
        //    }
        //    LoaiDichVu ldv = cb.SelectedItem as LoaiDichVu;
        //    loai = ldv.Loai;
        //    LoadDetailService(loai);
        //}

        #endregion Vip

        #endregion loadcategory and service by category

        #region event

        #region ClickRoom

        #region Thuong
        public void btnRoom_Click(object sender, EventArgs e)
        {
            ICommand Click_Room = new Click_Room_Command(isAdmin);
            Invoker_Command control_start_end = new Invoker_Command_Builder().Click_Builder(Click_Room).build();
            control_start_end.Click_Room(sender);
        }
        #endregion Thuong

        #region Vip
        public void btnRoom_Click_Vip(object sender, EventArgs e)
        {
            ICommand Click_Room = new Click_Room_Vip_Command (isAdmin);
            Invoker_Command control_start_end = new Invoker_Command_Builder().Click_Builder(Click_Room).build();
            control_start_end.Click_Room(sender);
        }
        #endregion Vip

        #endregion ClickRoom

        #region StartButton
        private void btnStart_Click(object sender, EventArgs e)
        {
            ICommand Start_Service = new Service_Start_Command(isAdmin);
            Invoker_Command control_start_end = new Invoker_Command_Builder().Start_Builder(Start_Service).build();
            control_start_end.Start_Service(sender);
        }

        private void btnStartVip_Click(object sender, EventArgs e)
        {
            ICommand Start_Service = new ServiceVip_Start_Command(isAdmin);
            Invoker_Command control_start_end = new Invoker_Command_Builder().Start_Builder(Start_Service).build();
            control_start_end.Start_Service(sender);
        }
        #endregion StartButton

        #region AddService
        private void btnAddService_Click(object sender, EventArgs e)
        {
            ICommand Add_Service = new Add_Service_Command(isAdmin);
            Invoker_Command control_add = new Invoker_Command_Builder().Add_Builder(Add_Service).build();
            control_add.Add_Service(sender);
        }

        private void btnAddServiceVip_Click(object sender, EventArgs e)
        {
            ICommand Add_Service = new Add_Service_Vip_Command(isAdmin);
            Invoker_Command control_add = new Invoker_Command_Builder().Add_Builder(Add_Service).build();
            control_add.Add_Service(sender);
        }

        public bool CheckExitsService(string TenDV, List<Hoadon> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if (TenDV.Equals(list[i].TenDV))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion AddService

        #region ThanhToan

        #region Thuong
        public void btnThanhtoan_Click(object sender, EventArgs e)
        {
            ICommand End_Service = new Service_End_Command(isAdmin);
            Invoker_Command control_start_end = new Invoker_Command_Builder().End_Builder(End_Service).build();
            control_start_end.End_Service(sender);
            //ICommand_Start_End_Add Start_Service = new Service_Start_Command(isAdmin);
            //ICommand_Start_End_Add End_Service = new Service_End_Command(isAdmin);
            //Invoker_Start_End_Service control_start_end = new Invoker_Start_End_Service(Start_Service, End_Service);
            //control_start_end.End_Service();
        }
        #endregion Thuong

        #region Vip
        private void btnThanhtoanVip_Click(object sender, EventArgs e)
        {
            ICommand End_Service = new ServiceVip_End_Command(isAdmin);
            Invoker_Command control_start_end = new Invoker_Command_Builder().End_Builder(End_Service).build();
            control_start_end.End_Service(sender);

            //ICommand_Start_End_Add Start_Service = new ServiceVip_Start_Command(isAdmin);
            //ICommand_Start_End_Add End_Service = new ServiceVip_End_Command(isAdmin);
            //Invoker_Start_End_Service control_start_end = new Invoker_Start_End_Service(Start_Service, End_Service);
            //control_start_end.End_Service();
        }
        #endregion Vip

        #endregion ThanhToan

        #region runclock

        #region second to Time
        public string clockText(int second)
        {

            String h = "";
            String min = "";
            String sec = "";

            int gio = second / 3600;
            int temp = second % 3600;
            int phut = temp / 60;
            int giay = temp % 60;

            h += gio.ToString();
            min += phut.ToString();
            sec += giay.ToString();

            if (gio < 10)
            {
                h = "0" + h;
            }
            if (phut < 10)
            {
                min = "0" + min;
            }
            if (giay < 10)
            {
                sec = "0" + sec;
            }
            String clock = h + ":" + min + ":" + sec;
            return clock;
        }
        #endregion second to Time

        #region Su ly nhay
        private void timer1_Tick(object sender, EventArgs e)
        {

            giayTimer++;
            if (giayTimer == 60)
            {
                giayTimer = 0;
                phutTimer++;
            }

            if (phutTimer == 60)
            {
                gioTimer++;
                phutTimer = 0;
            }

            string hour, min, sec;

            if (giayTimer <= 9)
            {
                sec = '0' + giayTimer.ToString();
            }
            else
            {
                sec = giayTimer.ToString();
            }
            if (phutTimer <= 9)
            {
                min = '0' + phutTimer.ToString();
            }
            else
            {
                min = phutTimer.ToString();
            }
            if (gioTimer <= 9)
            {
                hour = '0' + gioTimer.ToString();
            }
            else
            {
                hour = gioTimer.ToString();
            }
            clock.Text = hour + ":" + min + ":" + sec;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            giayTimer++;
            if (giayTimer == 60)
            {
                giayTimer = 0;
                phutTimer++;
            }

            if (phutTimer == 60)
            {
                gioTimer++;
                phutTimer = 0;
            }

            string hour, min, sec;

            if (giayTimer <= 9)
            {
                sec = '0' + giayTimer.ToString();
            }
            else
            {
                sec = giayTimer.ToString();
            }
            if (phutTimer <= 9)
            {
                min = '0' + phutTimer.ToString();
            }
            else
            {
                min = phutTimer.ToString();
            }
            if (gioTimer <= 9)
            {
                hour = '0' + gioTimer.ToString();
            }
            else
            {
                hour = gioTimer.ToString();
            }
            clockVip.Text = hour + ":" + min + ":" + sec;
        }




        #endregion Su ly nhay

        #endregion runclock

        #endregion event

        private void tsmLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
