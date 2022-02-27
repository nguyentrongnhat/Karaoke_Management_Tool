using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CĐCNPM;
using CĐCNPM.DAO;

namespace CĐCNPM.Pattern.Strategy
{
    public abstract class BindingData
    {
        public abstract void Binding();
    }
    public class BindingNhanVien : BindingData
    {
        public static BindingNhanVien instance = new BindingNhanVien();
        public static BindingNhanVien getInstance()
        {
            return instance;
        }
        private BindingNhanVien() { }
        public override void Binding()
        {
            AdminForm form = AdminForm.getInstance();
            if (form.isRemoveBidingNhanvien == false)
            {
                form.Remove_NhanVien_Binding();
                form.isRemoveBidingNhanvien = true;
            }
            form.AddbindingNhanVien();
            form.isRemoveBidingNhanvien = false;
        }
    }
    public class BindingTaiKhoan : BindingData
    {
        public static BindingTaiKhoan instance = new BindingTaiKhoan();
        public static BindingTaiKhoan getInstance()
        {
            return instance;
        }
        private BindingTaiKhoan() { }
        public override void Binding()
        {
            AdminForm form = AdminForm.getInstance();
            if (form.isRemoveBidingAccount == false)
            {
                form.Remove_TaiKhoan_Binding();
                form.isRemoveBidingAccount = true;
            }
            form.AddbindingTaiKhoan();
            form.isRemoveBidingAccount = false;
        }
    }
    public class BindingDichVu : BindingData
    {
        public static BindingDichVu instance = new BindingDichVu();
        public static BindingDichVu getInstance()
        {
            return instance;
        }
        private BindingDichVu() { }
        public override void Binding()
        {
            AdminForm form = AdminForm.getInstance();
            if (form.isRemoveBidingService == false)
            {
                form.Remove_YCDichvu_Binding();
                form.isRemoveBidingService = true;
            }
            form.AddbindingYC_Dichvu();
            form.isRemoveBidingService = false;
        }
    }
    public class BindingPhong : BindingData
    {
        public static BindingPhong instance = new BindingPhong();
        public static BindingPhong getInstance()
        {
            return instance;
        }
        private BindingPhong() { }
        public override void Binding()
        {
            AdminForm form = AdminForm.getInstance();
            if (form.isRemoveBidingPhong == false)
            {
                form.Remove_Phong_Binding();
                form.isRemoveBidingPhong = true;
            }
            form.AddbindingPhong();
            form.isRemoveBidingPhong = false;
        }
    }
    public class StrategyBinding
    {
        public static StrategyBinding instance = new StrategyBinding();
        public static StrategyBinding getInstance()
        {
            return instance;
        }
        private StrategyBinding() { }
        
        public BindingData binding;
        public void setBinding (BindingData binding)
        {
            this.binding = binding;
        }
        public void Binding()
        {
            this.binding.Binding();
        }
    }
}
