using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Entities.Identity
{
    public class RefreshToken : BaseEntity<long>
    {
        public long UserId { get; set; }
        //chuỗi refresh token
        public string Token { get; set; } = string.Empty;
        //Id cuar JWT token gốc
        public string JwtId { get; set; }
        //Thời hạn token
        public DateTime ExpiryDate { get; set; }
        //Đã bị thu hồi hay chưa
        public bool IsRevoked { get; set; }
        //Đã hết hạn chưa
        public bool IsExpired => DateTime.Now >= ExpiryDate;
        //Trạng thái hoạt động
        public bool IsActive => !IsRevoked && !IsExpired;
        //IP tạo token
        public string CreatedByIp { get; set; }
        //Ip thu hồi
        public string RevokedByIp { get; set; }
    }
}
