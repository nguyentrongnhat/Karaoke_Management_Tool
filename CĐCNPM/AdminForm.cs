using CĐCNPM.DAO;
using CĐCNPM.Pattern.Strategy;
using CĐCNPM.Model;
using CĐCNPM.Properties;
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
    public partial class AdminForm : Form
    {
        #region varBindingObject
        public Binding bd_MSNV;
        public Binding bd_Hoten;
        public Binding bd_Gioitinh;
        public Binding bd_Ngaysinh;
        public Binding bd_CMND;
        public Binding bd_DienThoai;
        public Binding bd_Diachi;

        public Binding bd_MSPhong;
        public Binding bd_Loaiphong;
        public Binding bd_Giaphong;
        public Binding bd_Trangthai;

        public Binding bd_MsDV;
        public Binding bd_TenDV;
        public Binding bd_GiaDV;
        public Binding bd_LoaiDV;

        public Binding bd_Usename;
        public Binding bd_Password;
        public Binding bd_isAdmin;
        #endregion varBindingObject

        #region varBindingObject
        public Boolean isRemoveBidingAccount = false;
        public Boolean isRemoveBidingService = false;
        public Boolean isRemoveBidingNhanvien = false;
        public Boolean isRemoveBidingPhong = false;
        #endregion varBindingObject
        
        public static AdminForm instance = new AdminForm();
        public static AdminForm getInstance()
        {
            return instance;
        }
        private AdminForm()
        {
            InitializeComponent();
            dtgrvNhanVien.DataSource = NhanVienDAO.getInstance().GetAll();
            dtgrvServiceOder.DataSource = DichVuDAO.getInstance().GetList_();
            dtgrvServiceRoom.DataSource = PhongDAO.getInstance().GetList();
            dtgrvThongke.DataSource = PhienDungPhongDAO.getInstance().GetList();
            dtgrvTaiKhoan.DataSource = AccountDAO.getInstance().GetAll();
            tbTong.Text = "00.0";

            setCombobox();

            AddbindingNhanVien();
            AddbindingPhong();
            AddbindingYC_Dichvu();
            AddbindingTaiKhoan();
            
        }

        private void setCombobox()
        {
            List<string> loaiphong = new List<string>();
            loaiphong.Add("Vip");
            loaiphong.Add("Thường");
            cbbLoaiPhong.DataSource = loaiphong;

            List<string> gioitinh = new List<string>();
            gioitinh.Add("NAM");
            gioitinh.Add("NỮ");
            cbbGioitinh.DataSource = gioitinh;

            cbbLoaiDV.DataSource = DichVuDAO.getInstance().GetCategory();
            cbbLoaiDV.DisplayMember = "Loai";

            List<string> isAdmin = new List<string>();
            isAdmin.Add("True");
            isAdmin.Add("False");
            cbbIsAdmin.DataSource = isAdmin;
        }

        #region binding_&_remove_nhanvien

        public void AddbindingNhanVien()
        {
            bd_MSNV = new Binding("Text", dtgrvNhanVien.DataSource, "MSNV");
            tbMSNV.DataBindings.Add(bd_MSNV);

            bd_Hoten = new Binding("Text", dtgrvNhanVien.DataSource, "Hoten");
            tbTenNV.DataBindings.Add(bd_Hoten);

            bd_DienThoai = new Binding("Text", dtgrvNhanVien.DataSource, "DienThoai");
            tbDienthoai.DataBindings.Add(bd_DienThoai);

            bd_Gioitinh = new Binding("Text", dtgrvNhanVien.DataSource, "GioiTinh");
            cbbGioitinh.DataBindings.Add(bd_Gioitinh);

            bd_Ngaysinh = new Binding("Text", dtgrvNhanVien.DataSource, "NgaySinh");
            dtpkNgaysinh.DataBindings.Add(bd_Ngaysinh);

            bd_Diachi = new Binding("Text", dtgrvNhanVien.DataSource, "DiaChi");
            tbDiachi.DataBindings.Add(bd_Diachi);

            bd_CMND = new Binding("Text", dtgrvNhanVien.DataSource, "CMND");
            tbCMND.DataBindings.Add(bd_CMND);
        }

        public void Remove_NhanVien_Binding()
        {
            tbMSNV.DataBindings.Remove(bd_MSNV);
            tbDienthoai.DataBindings.Remove(bd_DienThoai);
            tbTenNV.DataBindings.Remove(bd_Hoten);
            cbbGioitinh.DataBindings.Remove(bd_Gioitinh);
            tbDiachi.DataBindings.Remove(bd_Diachi);
            tbCMND.DataBindings.Remove(bd_CMND);
            dtpkNgaysinh.DataBindings.Remove(bd_Ngaysinh);
        }

        #endregion binding_&_remove_nhanvien

        #region binding_&_remove_phong
        public  void AddbindingPhong()
        {
            bd_MSPhong = new Binding("Text", dtgrvServiceRoom.DataSource, "MSPhong");
            tbMaPhong.DataBindings.Add(bd_MSPhong);

            bd_Giaphong = new Binding("Text", dtgrvServiceRoom.DataSource, "GiaPhong");
            tbGiaPhong.DataBindings.Add(bd_Giaphong);

            bd_Loaiphong = new Binding("Text", dtgrvServiceRoom.DataSource, "LoaiPhong");
            cbbLoaiPhong.DataBindings.Add(bd_Loaiphong);

            bd_Trangthai = new Binding("Text", dtgrvServiceRoom.DataSource, "TrangThai");
            tbTrangThai.DataBindings.Add(bd_Trangthai);
        }

        public void Remove_Phong_Binding()
        {
            tbMaPhong.DataBindings.Remove(bd_MSPhong);
            tbGiaPhong.DataBindings.Remove(bd_Giaphong);
            cbbLoaiPhong.DataBindings.Remove(bd_Loaiphong);
            tbTrangThai.DataBindings.Remove(bd_Trangthai);
        }

        #endregion binding_&_remove_phong

        #region binding_&_remove_ycdichvu

        public void AddbindingYC_Dichvu()
        {
            bd_MsDV = new Binding("Text", dtgrvServiceOder.DataSource, "MaDV");
            tbMaDV.DataBindings.Add(bd_MsDV);

            bd_TenDV = new Binding("Text", dtgrvServiceOder.DataSource, "TenDV");
            tbTenDV.DataBindings.Add(bd_TenDV);

            bd_LoaiDV = new Binding("Text", dtgrvServiceOder.DataSource, "LoaiDV");
            cbbLoaiDV.DataBindings.Add(bd_LoaiDV);

            bd_GiaDV = new Binding("Text", dtgrvServiceOder.DataSource, "GiaDV");
            tbGiaDV.DataBindings.Add(bd_GiaDV);
        }

        public void Remove_YCDichvu_Binding()
        {
            tbMaDV.DataBindings.Remove(bd_MsDV);
            tbTenDV.DataBindings.Remove(bd_TenDV);
            cbbLoaiDV.DataBindings.Remove(bd_LoaiDV);
            tbGiaDV.DataBindings.Remove(bd_GiaDV);
        }

        #endregion binding_&_remove_ycdichvu

        #region binding_&_remove_taikhoan

        public void AddbindingTaiKhoan()
        {
            bd_Usename = new Binding("Text", dtgrvTaiKhoan.DataSource, "Username");
            tbUsername.DataBindings.Add(bd_Usename);

            bd_Password = new Binding("Text", dtgrvTaiKhoan.DataSource, "Password");
            tbPassword.DataBindings.Add(bd_Password);

            bd_isAdmin = new Binding("Text", dtgrvTaiKhoan.DataSource, "isAdmin");
            cbbIsAdmin.DataBindings.Add(bd_isAdmin);
        }

        public void Remove_TaiKhoan_Binding()
        {
            tbUsername.DataBindings.Remove(bd_Usename);
            tbPassword.DataBindings.Remove(bd_Password);
            cbbIsAdmin.DataBindings.Remove(bd_isAdmin);
        }

        #endregion binding_&_remove_taikhoan

        #region thongke

        private void btXemthongke_Click(object sender, EventArgs e)
        {
            DateTime DataFrom = dateTimePickerFrom.Value.Date;
            DateTime DataTo = dateTimePickerTo.Value.Date;
            List<PhienDungPhong> list = PhienDungPhongDAO.getInstance().Fillter(DataFrom, DataTo);
            dtgrvThongke.DataSource = list;
            int tong = 0;
            for(int i = 0; i < list.Count; i++)
            {
                tong += list[i].Dongia.Value;
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            tbTong.Text = tong.ToString("c", culture);
        }
        #endregion thongke

        #region CRUED_nhanvien

        private void btAddNV_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn thêm nhân viên?", "Xác nhận thêm nhân viên!", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK) 
            {
                if (tbTenNV.Text.Equals(""))
                {
                    MessageBox.Show("Chưa nhập tên nhân viên!");
                }
                else if (tbCMND.Text.Equals(""))
                {
                    MessageBox.Show("Chưa nhập CMND!");
                }
                else if (NhanVienDAO.getInstance().GetByCMND(tbCMND.Text) != null)
                {
                    MessageBox.Show("Chứng minh nhân dân bị trùng!");
                }
                else
                {
                    NhanVien add_nhanvien = new NhanVien();
                    add_nhanvien.HoTen = tbTenNV.Text;
                    add_nhanvien.NgaySinh = Convert.ToDateTime(dtpkNgaysinh.Value).Date;
                    add_nhanvien.GioiTinh = cbbGioitinh.Text;
                    add_nhanvien.CMND = tbCMND.Text;
                    add_nhanvien.DiaChi = tbDiachi.Text;
                    add_nhanvien.DienThoai = tbDienthoai.Text;

                    NhanVienDAO.getInstance().Add(add_nhanvien);
                    dtgrvNhanVien.DataSource = NhanVienDAO.getInstance().GetAll();

                    StrategyBinding sb = StrategyBinding.getInstance();
                    sb.setBinding(BindingNhanVien.getInstance());
                    sb.Binding();

                    MessageBox.Show("Thêm thành công nhân viên mới.");
                }
                
            }
        }

        private void btDelNV_Click(object sender, EventArgs e)
        {
            String id_nhanvien = tbMSNV.Text;
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn xóa nhân viên " + id_nhanvien + "?", "Xác nhận xóa nhân viên!", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {              
                NhanVienDAO.getInstance().Delete(id_nhanvien);
                dtgrvNhanVien.DataSource = NhanVienDAO.getInstance().GetAll();

                StrategyBinding sb = StrategyBinding.getInstance();
                sb.setBinding(BindingNhanVien.getInstance());
                sb.Binding();

                MessageBox.Show("Xóa thành công.");
            }
        }

        private void btEditNV_Click(object sender, EventArgs e)
        {
            String id_nhanvien = tbMSNV.Text;
            string dateOFbirth = dtpkNgaysinh.Text;
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn sửa thông tin nhân viên " + id_nhanvien + "?", "Xác nhận sửa thông tin nhân viên!", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                NhanVien add_nhanvien = new NhanVien();
                add_nhanvien.MSNV = tbMSNV.Text;
                add_nhanvien.HoTen = tbTenNV.Text;
                //add_nhanvien.NgaySinh = Convert.ToDateTime(tbNgaysinh.Text);
                add_nhanvien.NgaySinh = Convert.ToDateTime(dateOFbirth);
                add_nhanvien.GioiTinh = cbbGioitinh.Text;
                add_nhanvien.CMND = tbCMND.Text;
                add_nhanvien.DiaChi = tbDiachi.Text;
                add_nhanvien.DienThoai = tbDienthoai.Text;
                NhanVienDAO.getInstance().Update(add_nhanvien);
                dtgrvNhanVien.DataSource = NhanVienDAO.getInstance().GetAll();

                StrategyBinding sb = StrategyBinding.getInstance();
                StrategyBinding.getInstance().setBinding(BindingNhanVien.getInstance());
                sb.Binding();

                MessageBox.Show("Sửa thông tin cho nhân viên " + id_nhanvien + " thành công.");
            }
        }

        #endregion CRUED_nhanvien

        #region CRUED_phong
        private void btAddRoom_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn thêm phòng mới vào danh sách?", "Xác nhận thêm phòng", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                if (cbbLoaiPhong.Text.Equals("Vip") == false && cbbLoaiPhong.Text.Equals("Thường") == false)
                {
                    MessageBox.Show("Loại phòng chỉ nhận giá trị Thường hoặc Vip!");
                }
                else if (cbbLoaiPhong.Text.Equals(""))
                {
                    MessageBox.Show("Chưa nhập Loại phòng!");
                }
                else if (tbGiaPhong.Text.Equals(""))
                {
                    MessageBox.Show("Chưa nhập Giá phòng!");
                }
                else
                {
                    Phong add_phong = new Phong();
                    add_phong.LoaiPhong = cbbLoaiPhong.Text;
                    add_phong.GiaPhong = Int32.Parse(tbGiaPhong.Text);
                    PhongDAO.getInstance().Add(add_phong);
                    dtgrvServiceRoom.DataSource = PhongDAO.getInstance().GetList();

                    StrategyBinding sb = StrategyBinding.getInstance();
                    sb.setBinding(BindingPhong.getInstance());
                    sb.Binding();

                    //RoomManagerForm update = new RoomManagerForm(true);
                    //update.LoadPhongThuong();
                    //update.LoadPhongVip();

                    MessageBox.Show("Thêm thành công phòng mới vào danh sách.");
                }
            }
        }

        private void btDelRoom_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn xóa phòng " + tbMaPhong.Text + " khỏi danh sách?", "Xác nhận xóa phòng", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                String id_phong = tbMaPhong.Text;
                PhongDAO.getInstance().Delete(id_phong);
                dtgrvServiceRoom.DataSource = PhongDAO.getInstance().GetList();

                StrategyBinding sb = StrategyBinding.getInstance();
                sb.setBinding(BindingPhong.getInstance());
                sb.Binding();

                MessageBox.Show("Xóa thành công.");
            }
        }

        private void btEditRoom_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn sửa thông tin phòng " + tbMaPhong.Text + " ?", "Xác nhận sửa phòng", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                Phong add_phong = new Phong();
                add_phong.LoaiPhong = cbbLoaiPhong.Text;
                add_phong.GiaPhong = Int32.Parse(tbGiaPhong.Text);
                add_phong.MSPhong = tbMaPhong.Text;
                add_phong.Trangthai = tbTrangThai.Text;
                PhongDAO.getInstance().Update(add_phong);
                dtgrvServiceRoom.DataSource = PhongDAO.getInstance().GetList();

                StrategyBinding sb = StrategyBinding.getInstance();
                sb.setBinding(BindingPhong.getInstance());
                sb.Binding();

                MessageBox.Show("Sửa thông tin phòng thành công.");
            }
        }

        #endregion CRUED_phong

        #region CRUED_dichvu
        private void btAddService_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn thêm dịch vụ vào danh sách?", "Xác nhận thêm dịch vụ", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                if (tbTenDV.Text.Equals(""))
                {
                    MessageBox.Show("Bạn chưa nhập tên dịch vụ!");
                }
                else if (tbGiaDV.Text.Equals(""))
                {
                    MessageBox.Show("Bạn chưa nhập giá dịch vụ!");
                }
                else if (cbbLoaiDV.Text.Equals(""))
                {
                    MessageBox.Show("Bạn chưa nhập loại dịch vụ!");
                }
                else
                {
                    Dichvu add_dichvu = new Dichvu();
                    add_dichvu.GiaDV = Int32.Parse(tbGiaDV.Text);
                    add_dichvu.LoaiDV = cbbLoaiDV.Text;
                    add_dichvu.TenDV = tbTenDV.Text;

                    DichVuDAO.getInstance().Add(add_dichvu);
                    dtgrvServiceOder.DataSource = DichVuDAO.getInstance().GetList_();

                    StrategyBinding sb = StrategyBinding.getInstance();
                    sb.setBinding(BindingDichVu.getInstance());
                    sb.Binding();

                    MessageBox.Show("Thêm thành công.");
                }
            }
        }
        private void btDelService_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn xóa dịch vụ " + tbMaDV.Text + " khỏi danh sách?", "Xác nhận xóa dịch vụ", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                String id_dichvu = tbMaDV.Text;
                DichVuDAO.getInstance().Delete(id_dichvu);
                dtgrvServiceOder.DataSource = DichVuDAO.getInstance().GetList_();

                StrategyBinding sb = StrategyBinding.getInstance();
                sb.setBinding(BindingDichVu.getInstance());
                sb.Binding();

                MessageBox.Show("Xóa thành công.");
            }      
        }
        private void btEditService_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn sửa thông tin dịch vụ " + tbMaDV.Text + " ?", "Xác nhận sửa dịch vụ", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                Dichvu add_dichvu = new Dichvu();
                add_dichvu.GiaDV = Int32.Parse(tbGiaDV.Text);
                add_dichvu.LoaiDV = cbbLoaiDV.Text;
                add_dichvu.TenDV = tbTenDV.Text;
                add_dichvu.MaDV = tbMaDV.Text;

                DichVuDAO.getInstance().Update(add_dichvu);
                dtgrvServiceOder.DataSource = DichVuDAO.getInstance().GetList_();

                StrategyBinding sb = StrategyBinding.getInstance();
                sb.setBinding(BindingDichVu.getInstance());
                sb.Binding();

                MessageBox.Show("Sửa thành công.");
            }  
        }
        #endregion CRUED_dichvu

        #region CRUED_Taikhoan
        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn tạo tài khoản mới?", "Xác nhận tạo tài khoản", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                if (AccountDAO.getInstance().GetbyUsername(username) != null)
                {
                    MessageBox.Show("Username đã tồn tại!");
                }
                else if(AccountDAO.getInstance().Get(username, tbPassword.Text)!=null)
                {
                    MessageBox.Show("Tài khoản đã tồn tại!");
                }
                else if(username.Equals("") || tbPassword.Text.Equals(""))
                {
                    MessageBox.Show("Chưa nhập username hoặc password!");
                }
                else
                {
                    Account add_taikhoan = new Account();
                    add_taikhoan.Username = tbUsername.Text;
                    add_taikhoan.Password = tbPassword.Text;
                    add_taikhoan.isAdmin = bool.Parse(cbbIsAdmin.Text);

                    AccountDAO.getInstance().Add(add_taikhoan);
                    dtgrvTaiKhoan.DataSource = AccountDAO.getInstance().GetAll();

                    StrategyBinding sb = StrategyBinding.getInstance();
                    sb.setBinding(BindingTaiKhoan.getInstance());
                    sb.Binding();

                    MessageBox.Show("Thêm thành công.");
                }
            } 
        }

        private void btnDelAcc_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn xóa tài khoản?", "Xác nhận xóa tài khoản", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                String Usename = tbUsername.Text;
                String Password = tbPassword.Text;
                AccountDAO.getInstance().Delete(Usename, Password);
                dtgrvTaiKhoan.DataSource = AccountDAO.getInstance().GetAll();

                StrategyBinding sb = StrategyBinding.getInstance();
                sb.setBinding(BindingTaiKhoan.getInstance());
                sb.Binding();

                MessageBox.Show("Xóa thành công.");
            }
        }

        #endregion CRUED_Taikhoan

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
            //RoomManagerForm managerRoom = RoomManagerForm.getInstance(true);
            //managerRoom.ShowDialog();
            //this.Show();
            //this.Close();
        }
    }
}
