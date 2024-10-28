using System;
using System.Collections.Generic;
using System.Text;

namespace chdTour.DataAccess.Contracts.Domain.Base
{
    public class BaseAttachmentEntity<T> : BaseEntity<T> where T : struct
    {
        public byte[] Data { get; set; }
        public string Type { get; set; }

        public string Url => string.Format("data:{0};base64,{1}", this.Type, Convert.ToBase64String(this.Data));

    }
}
