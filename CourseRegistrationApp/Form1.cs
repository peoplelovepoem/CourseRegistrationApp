using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CourseRegistrationApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo danh sách Chuyên ngành
            cboNganh.Items.Clear();
            cboNganh.Items.AddRange(new string[] {
                "Công nghệ thông tin",
                "Kỹ thuật phần mềm",
                "Hệ thống thông tin",
                "Khoa học dữ liệu",
                "An toàn thông tin"
            });
            cboNganh.SelectedIndex = 0;

            // Khởi tạo danh sách môn học kèm số tín chỉ
            clbMonHoc.Items.Clear();
            clbMonHoc.Items.Add("Lập trình Windows (3 tín chỉ)");
            clbMonHoc.Items.Add("Cơ sở dữ liệu (3 tín chỉ)");
            clbMonHoc.Items.Add("Cấu trúc dữ liệu & Giải thuật (4 tín chỉ)");
            clbMonHoc.Items.Add("Mạng máy tính (3 tín chỉ)");
            clbMonHoc.Items.Add("Thực hành Lập trình Windows (2 tín chỉ)");

            // Thiết lập cột cho DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvDanhSach.Columns.Clear();
            dgvDanhSach.Columns.Add("MSSV", "Mã SV");
            dgvDanhSach.Columns.Add("HoTen", "Họ và Tên");
            dgvDanhSach.Columns.Add("Nganh", "Chuyên Ngành");
            dgvDanhSach.Columns.Add("GioiTinh", "Giới Tính");
            dgvDanhSach.Columns.Add("MonHoc", "Các Môn Đăng Ký");
            dgvDanhSach.Columns.Add("TongTC", "Tổng Tín Chỉ");
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // 1. Nút Đăng ký: Kiểm tra dữ liệu và hiển thị kết quả
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra rỗng MSSV
            if (string.IsNullOrWhiteSpace(txtMSSV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã số sinh viên!", "Thông báo lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMSSV.Focus();
                return;
            }

            // Kiểm tra rỗng Họ tên
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ và tên sinh viên!", "Thông báo lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // Kiểm tra xem đã chọn môn học chưa
            if (clbMonHoc.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một môn học để đăng ký!", "Thông báo lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy giới tính
            string gioiTinh = rdbNam.Checked ? "Nam" : "Nữ";

            // Lấy danh sách các môn đã chọn và tính tổng số tín chỉ
            List<string> tenMonDaChon = new List<string>();
            int tongTinChi = 0;

            foreach (var item in clbMonHoc.CheckedItems)
            {
                string tenMon = item.ToString();
                tenMonDaChon.Add(tenMon);

                if (tenMon.Contains("4 tín chỉ")) tongTinChi += 4;
                else if (tenMon.Contains("2 tín chỉ")) tongTinChi += 2;
                else tongTinChi += 3;
            }

            string chuoiMonHoc = string.Join("; ", tenMonDaChon);

            // Thêm dòng mới vào DataGridView
            dgvDanhSach.Rows.Add(
                txtMSSV.Text.Trim(),
                txtHoTen.Text.Trim(),
                cboNganh.SelectedItem.ToString(),
                gioiTinh,
                chuoiMonHoc,
                tongTinChi
            );

            MessageBox.Show("Đăng ký môn học thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 2. Nút Làm mới: Đưa Form về trạng thái ban đầu
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMSSV.Clear();
            txtHoTen.Clear();

            if (cboNganh.Items.Count > 0)
                cboNganh.SelectedIndex = 0;

            rdbNam.Checked = true;

            // Bỏ chọn toàn bộ các môn học
            for (int i = 0; i < clbMonHoc.Items.Count; i++)
            {
                clbMonHoc.SetItemChecked(i, false);
            }

            txtMSSV.Focus();
        }

        // 3. Nút Thoát: Xác nhận đóng ứng dụng
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng không?",
                                                "Xác nhận thoát",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}