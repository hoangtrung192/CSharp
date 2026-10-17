using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LuxuryBridalStudio
{
    public static class InputRules
    {
        public static readonly CultureInfo Vietnamese = CultureInfo.GetCultureInfo("vi-VN");
        public static string Money(decimal value) { return value.ToString("N0", Vietnamese) + " đ"; }

        // Whole VND only. Accept 3500000, 3.500.000 or 3,500,000; reject ambiguous decimals.
        public static decimal ParseMoney(string text)
        {
            text = (text ?? "").Trim();
            if (text.EndsWith("đ", StringComparison.OrdinalIgnoreCase)) text = text.Substring(0, text.Length - 1).Trim();
            if (!Regex.IsMatch(text, @"^(?:[0-9]+|[0-9]{1,3}(?:\.[0-9]{3})+|[0-9]{1,3}(?:,[0-9]{3})+)$"))
                throw new InvalidOperationException("Số tiền phải là số nguyên VNĐ, ví dụ 3500000 hoặc 3.500.000.");
            decimal value;
            if (!decimal.TryParse(text.Replace(".", "").Replace(",", ""), NumberStyles.None, CultureInfo.InvariantCulture, out value) || value > 1000000000m)
                throw new InvalidOperationException("Số tiền tối đa là 1.000.000.000 đ.");
            return value;
        }

        public static string Phone(string text)
        {
            string phone = (text ?? "").Trim().Replace(" ", "").Replace("-", "");
            if (!Regex.IsMatch(phone, @"^[0-9]{9,11}$")) throw new InvalidOperationException("Số điện thoại cần 9–11 chữ số.");
            return phone;
        }

        public static string Required(string text, string label, int max = 150)
        {
            text = (text ?? "").Trim();
            if (text.Length < 2 || text.Length > max) throw new InvalidOperationException(label + " cần từ 2 đến " + max + " ký tự.");
            return text;
        }

        public static string EscapeLike(string text)
        {
            var result = new StringBuilder();
            foreach (char c in text ?? "")
            {
                if (c == '\'') result.Append("''");
                else if ("[]%*".IndexOf(c) >= 0) result.Append('[').Append(c).Append(']');
                else result.Append(c);
            }
            return result.ToString();
        }

        public static string SearchFilter(string text, params string[] columns)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            return string.Join(" OR ", columns.Select(c => "[" + c + "] LIKE '%" + EscapeLike(text.Trim()) + "%'"));
        }

        public static string NextCode(DataTable table, string prefix)
        {
            int max = 0;
            foreach (DataRow row in table.Rows)
            {
                string code = Convert.ToString(row[0]);
                int n;
                if (code.StartsWith(prefix, StringComparison.Ordinal) && int.TryParse(code.Substring(prefix.Length), out n)) max = Math.Max(max, n);
            }
            return prefix + checked(max + 1).ToString("D3");
        }
    }
}
