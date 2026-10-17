using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
public sealed class EmployeeService
    {
        private readonly List<StaffMember> employees;
        private readonly StudioWorkspace workspace;
        public EmployeeService(StudioWorkspace data) : this(data == null ? null : data.Employees) { workspace = data; }
        public EmployeeService(List<StaffMember> data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Any(x => x == null || string.IsNullOrWhiteSpace(x.Ma)) || data.Select(x => x.Ma).Distinct().Count() != data.Count)
                throw new ArgumentException("Dữ liệu phải có mã không rỗng, không trùng và không chứa phần tử null.", "data");
            employees = data;
            if (ReferenceEquals(data, AppSession.Data.Employees)) workspace = AppSession.Data;
        }

        private string NextCode()
        {
            int number = 1;
            while (employees.Any(x => x.Ma == "NV" + number.ToString("0000"))) number++;
            return "NV" + number.ToString("0000");
        }


        public List<StaffMember> Search(string query)
        {
            AppSession.Require(Feature.Employees);
            return employees.Where(x=>(x.Ma+" "+x.HoTen).IndexOf(query ?? "",StringComparison.CurrentCultureIgnoreCase)>=0).Select(x=>new StaffMember { Ma=x.Ma,HoTen=x.HoTen,DienThoai=x.DienThoai,VaiTro=x.VaiTro,DangLam=x.DangLam }).ToList();
        }
        public void Save(string code, string name, string phone, string role, bool active)
        {
            AppSession.Require(Feature.Employees, true);
            if (workspace != null && workspace.IsPersistent) { workspace.Commit(current => new EmployeeService(current).Save(code, name, phone, role, active)); return; }
            bool add=code==null;

            name=InputRules.Required(name, "Họ tên"); phone=InputRules.Phone(phone);
            if(name.Length<2 || phone.Length<9 || phone.Length>11 || !phone.All(c=>c>='0' && c<='9')) throw new InvalidOperationException("Kiểm tra họ tên và số điện thoại.");
            if(!new[] { "Quản lý","Nhân viên","Thủ kho" }.Contains(role)) throw new InvalidOperationException("Vai trò không hợp lệ.");
            if(employees.Any(x=>x.DienThoai==phone && x.Ma!=code)) throw new InvalidOperationException("Số điện thoại đã được sử dụng.");
            var employee=add ? new StaffMember { Ma=NextCode() } : employees.SingleOrDefault(x=>x.Ma==code);
            if(employee==null) throw new InvalidOperationException("Không tìm thấy nhân viên.");

            employee.HoTen=name;employee.DienThoai=phone;employee.VaiTro=role;employee.DangLam=active;
            if(add) employees.Add(employee);
        }
        public void Deactivate(string code)
        {
            AppSession.Require(Feature.Employees, true);
            if (workspace != null && workspace.IsPersistent) { workspace.Commit(current => new EmployeeService(current).Deactivate(code)); return; }
            var employee=employees.SingleOrDefault(x=>x.Ma==code);
            if(employee==null) throw new InvalidOperationException("Chọn nhân viên.");
            employee.DangLam=false;
        }
    }
}
