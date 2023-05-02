using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.EmailSender
{
    public class MailSender
    {

        [DisplayName("تایید کد")]
        [Required(ErrorMessage ="تایید کد نمیتواند خالی باشد")]
        public string SendCode { get; set; }

    }
}
