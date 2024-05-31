using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingSample.Models;

public partial class Application
{
    public Guid PartitionKey { get; set; }

    [RegularExpression(@"^[\u4e00-\u9fa5]{2,}$", ErrorMessage = "名字必須包兩個中文字")]
    public string Name { get; set; }

    [RegularExpression(@"^[\u4e00-\u9fa5]{6,}$", ErrorMessage = "請輸入正確地址")]
    public string Address { get; set; }

    [RegularExpression(@"^09\d{8}$", ErrorMessage = "請輸入正確手機")]
    public string Phone { get; set; }

    public int Count_Person { get; set; }

    public int Count_Veg { get; set; }

    public int Count_Child { get; set; }

    public string Message { get; set; }
}
