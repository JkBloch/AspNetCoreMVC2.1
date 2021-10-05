using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Helpers
{
    public class CustomEmailIdTagHelper : TagHelper
    {
        public string sEmailId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{sEmailId}");
            output.Attributes.Add("id", "my-email-id");
            output.Content.SetContent("my-email");
        }

    }
}
